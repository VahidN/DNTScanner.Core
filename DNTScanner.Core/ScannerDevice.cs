using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using WIA;

namespace DNTScanner.Core
{
    /// <summary>
    /// ScannerDevice gets an active scanner and then configures its proprties and finally starts scanning the images.
    /// </summary>
    public class ScannerDevice : IDisposable
    {
        private bool _disposed;
        private Device _device;
        private Item _scannerItem;
        private ImageFile _imageFile;
        private WiaSettings _wiaScannerDeviceSettings;
        private WiaSettings _wiaScannerPictureSettings;
        private DeviceManager _manager;
        private WiaImageFormat _imageFormat;
        private ScannerSettings _scannerSettings;

        /// <summary>
        /// Using the first available scanner device as the primary scanner.
        /// </summary>
        public ScannerDevice()
        {
            setFirstAvailableScanner();
            setupDevice();
        }

        /// <summary>
        /// Using the selected scanner device as the primary scanner.
        /// </summary>
        /// <param name="scannerSettings">the provided scanner device</param>
        public ScannerDevice(ScannerSettings scannerSettings)
        {
            _scannerSettings = scannerSettings;
            setupDevice();
        }

        private void setFirstAvailableScanner()
        {
            var scanners = SystemDevices.GetScannerDevices();
            _scannerSettings = scanners.FirstOrDefault();
            if (_scannerSettings == null)
            {
                throw new InvalidOperationException("Please connect your scanner to the system and also make sure its driver is installed.");
            }
        }

        private void setupDevice()
        {
            _manager = new DeviceManager();
            var deviceInfos = _manager.DeviceInfos;
            var availableDeviceIds = new List<string>();
            for (var i = 1; i <= deviceInfos.Count; i++) // Using a regular for loop to avoid `System.IO.FileNotFoundException: Could not load file or assembly CustomMarshalers` in .NET Core 2x apps.
            {
                var info = deviceInfos[i];
                if (info.Type == WiaDeviceType.ScannerDeviceType)
                {
                    availableDeviceIds.Add(info.DeviceID);
                    if (info.DeviceID == _scannerSettings.Id)
                    {
                        _device = info.Connect();
                        _scannerItem = _device.Items[1];
                        _wiaScannerDeviceSettings = new WiaSettings(_device.Properties);
                        _wiaScannerPictureSettings = new WiaSettings(_scannerItem.Properties);
                        return;
                    }
                }
            }
            throw new InvalidOperationException(
                $"Scanner with DeviceID:`{_scannerSettings.Id}` not found. Available Device ID's: {string.Join(", ", availableDeviceIds)}.");
        }

        /// <summary>
        /// Setting properties like ColorFormat, Brightens, etc.
        /// </summary>
        public void ScannerPictureSettings(Action<ScannerPictureConfig> config)
        {
            var scannerPictureConfig = new ScannerPictureConfig(_wiaScannerPictureSettings, _scannerSettings);
            config(scannerPictureConfig);
        }

        /// <summary>
        /// Setting properties like DocumentSource.
        /// If your scanner is duplex or automatic document feeder, set these options.
        /// </summary>
        public void ScannerDeviceSettings(Action<ScannerDeviceConfig> config)
        {
            var scannerPictureConfig = new ScannerDeviceConfig(_wiaScannerDeviceSettings);
            config(scannerPictureConfig);
        }

        /// <summary>
        /// Starts scanning the images.
        /// </summary>
        public void PerformScan(WiaImageFormat format)
        {
            _imageFormat = format;
            _imageFile = (ImageFile)_scannerItem.Transfer(format.Value);
        }

        /// <summary>
        /// An optional images post processing.
        /// </summary>
        /// <param name="process">post processing config</param>
        public void ProcessScannedImages(Action<ImageProcessor> process)
        {
            if (_imageFile == null)
            {
                throw new InvalidOperationException($"Please call the `{nameof(PerformScan)}` method first.");
            }

            var imageProcess = new ImageProcessor(_imageFile, _wiaScannerDeviceSettings, _imageFormat);
            process(imageProcess);
            _imageFile = imageProcess.ApplyFilters();
        }

        /// <summary>
        /// Saves the scanned ImageFile to multiple files. An ImageFile can have multiple frames, based on the type of your scanner.
        /// This method returns the filenames of the final saved image files.
        /// </summary>
        public IEnumerable<string> SaveScannedImageFiles(string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
            {
                throw new ArgumentNullException(nameof(fileName));
            }

            if (_imageFile == null)
            {
                throw new InvalidOperationException($"Please call the `{nameof(PerformScan)}` method first.");
            }

            if (_imageFile.FrameCount == 1)
            {
                if (File.Exists(fileName))
                {
                    File.Delete(fileName);
                }
                _imageFile.SaveFile(fileName);
                yield return fileName;
            }
            else
            {
                var extension = Path.GetExtension(fileName);
                for (int frame = 1; frame <= _imageFile.FrameCount; frame++)
                {
                    var path = Path.ChangeExtension(fileName, $".{frame}{extension}");
                    if (File.Exists(path))
                    {
                        File.Delete(path);
                    }

                    _imageFile.ActiveFrame = frame;
                    _imageFile.SaveFile(path);
                    yield return path;
                }
            }
        }

        /// <summary>
        /// Saves the scanned ImageFile to multiple byte arrays. An ImageFile can have multiple frames, based on the type of your scanner.
        /// </summary>
        public IEnumerable<byte[]> ExtractScannedImageFiles()
        {
            if (_imageFile == null)
            {
                throw new InvalidOperationException($"Please call the `{nameof(PerformScan)}` method first.");
            }

            for (int frame = 1; frame <= _imageFile.FrameCount; frame++)
            {
                _imageFile.ActiveFrame = frame;
                yield return (byte[])_imageFile.FileData.get_BinaryData();
            }
        }

        /// <summary>
        /// Are there any images left? Are you using a Duplex or AutomaticDocumentFeeder scanner?
        /// </summary>
        public bool HasMorePages()
        {
            bool hasMorePages = false;
            if (_scannerSettings.IsDuplex || _scannerSettings.IsAutomaticDocumentFeeder)
            {
                var status = _wiaScannerDeviceSettings.GetPropertyValue<int>("Document Handling Status");
                hasMorePages = (status & WiaDpsDocumentHandlingStatus.FEED_READY) != 0;
            }
            return hasMorePages;
        }

        /// <summary>
        /// Clean up
        /// </summary>
        ~ScannerDevice()
        {
            Dispose(false);
        }

        /// <summary>
        /// Clean up
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Clean up
        /// </summary>
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            if (disposing)
            {
                // free other managed objects that implement
                // IDisposable only
            }

            // release any unmanaged objects
            // set the object references to null

            if (_manager != null)
            {
                Marshal.ReleaseComObject(_manager);
                _manager = null;
            }

            if (_imageFile != null)
            {
                Marshal.ReleaseComObject(_imageFile);
                _imageFile = null;
            }

            if (_scannerItem != null)
            {
                Marshal.ReleaseComObject(_scannerItem);
                _scannerItem = null;
            }

            if (_device != null)
            {
                Marshal.ReleaseComObject(_device);
                _device = null;
            }

            _disposed = true;
        }
    }
}