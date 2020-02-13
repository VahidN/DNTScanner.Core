using System.Collections.Generic;
using WIA;
using System.Runtime.InteropServices;
using System;

namespace DNTScanner.Core
{
    /// <summary>
    /// Lists the connected devices to the system.
    /// </summary>
    public static class SystemDevices
    {
        /// <summary>
        /// Lists the connected scanners to the system.
        /// </summary>
        public static IList<ScannerSettings> GetScannerDevices()
        {
            var scanners = new List<ScannerSettings>();
            var deviceInfos = new DeviceManager().DeviceInfos;
            for (var i = 1; i <= deviceInfos.Count; i++) // Using a regular for loop to avoid `System.IO.FileNotFoundException: Could not load file or assembly CustomMarshalers` in .NET Core 2x apps.
            {
                var info = deviceInfos[i];
                if (info.Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                var device = info.Connect();
                var scanner = device.Items[1];

                var wiaSettings = new WiaSettings(device.Properties);
                var defaultSettings = new WiaSettings(scanner.Properties);

                var caps = wiaSettings.GetPropertyValue<int>("Document Handling Capabilities");
                scanners.Add(new ScannerSettings
                {
                    Name = info.Properties["Description"].get_Value().ToString(),
                    Id = info.DeviceID,
                    SupportedResolutions = getAvailableResolutions(defaultSettings),
                    ScannerDeviceSettings = wiaSettings.ExtendedProperties,
                    ScannerPictureSettings = defaultSettings.ExtendedProperties,
                    IsAutomaticDocumentFeeder = (caps & (int)WiaDpsDocumentHandlingSelect.FEEDER) != 0,
                    IsDuplex = (caps & (int)WiaDpsDocumentHandlingSelect.DUPLEX) != 0,
                    IsFlatbed = (caps & (int)WiaDpsDocumentHandlingSelect.FLATBED) != 0,
                    SupportedEvents = getDeviceEvents(device.Events),
                    SupportedTransferFormats = getSupportedTransferFormats(scanner)
                });

                Marshal.ReleaseComObject(device);
                Marshal.ReleaseComObject(scanner);
            }
            return scanners;
        }

        /// <summary>
        /// Lists the static properties of scanners connected to the system.
        /// </summary>
        public static List<IDictionary<string, object>> GetScannerDeviceProperties()
        {
            List<IDictionary<string, object>> properties = new List<IDictionary<string, object>>();
            var deviceInfos = new DeviceManager().DeviceInfos;
            foreach(DeviceInfo device in deviceInfos)
            {
                if (device.Type != WiaDeviceType.ScannerDeviceType)
                {
                    continue;
                }

                IDictionary<string, object> props = new Dictionary<string, object>();
                foreach(Property item in device.Properties)
                {
                    props.Add(item.Name, item.get_Value());
                }
                properties.Add(props);

                Marshal.FinalReleaseComObject(device);
            }
            Marshal.FinalReleaseComObject(deviceInfos);

            return properties;
        }

        private static IDictionary<string, string> getSupportedTransferFormats(Item scanner)
        {
            var results = new Dictionary<string, string>();
            var formats = scanner.Formats;
            for (var i = 1; i <= formats.Count; i++)
            {
                var format = formats[i];
                switch (format)
                {
                    case "{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}":
                        results.Add(format, "Bmp");
                        break;
                    case "{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}":
                        results.Add(format, "Png");
                        break;
                    case "{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}":
                        results.Add(format, "Gif");
                        break;
                    case "{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}":
                        results.Add(format, "Jpg");
                        break;
                    case "{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}":
                        results.Add(format, "Tiff");
                        break;
                    default:
                        results.Add(format, "Unknown");
                        break;
                }
            }
            return results;
        }

        private static IDictionary<string, string> getDeviceEvents(DeviceEvents events)
        {
            var results = new Dictionary<string, string>();
            for (var j = 1; j <= events.Count; j++)
            {
                results.Add(events[j].Name, events[j].EventID);
            }
            return results;
        }

        private static IList<int> getAvailableResolutions(WiaSettings wiaSettings)
        {
            var availableResolutions = new List<int>();

            var horizontalResolution = wiaSettings.GetProperty("Horizontal Resolution");
            switch (horizontalResolution.SubType)
            {
                case WiaSubType.RangeSubType:
                    {
                        var stp = 100;
                        var min = 100;
                        var max = 2000;

                        if (horizontalResolution.SubTypeMin <= 75)
                        {
                            availableResolutions.Add(75);
                        }

                        if (horizontalResolution.SubTypeMin > min)
                        {
                            min = horizontalResolution.SubTypeMin;
                        }

                        if (horizontalResolution.SubTypeStep > stp)
                        {
                            stp = horizontalResolution.SubTypeStep;
                        }

                        if (horizontalResolution.SubTypeMax < max)
                        {
                            max = horizontalResolution.SubTypeMax;
                        }

                        for (var i = min; i <= max; i += stp)
                        {
                            availableResolutions.Add(i);
                        }

                        break;
                    }
                case WiaSubType.ListSubType:
                    {
                        var subTypeValues = horizontalResolution.SubTypeValues;
                        for (var i = 1; i <= subTypeValues.Count; i++)
                        {
                            availableResolutions.Add(Convert.ToInt32(subTypeValues.get_Item(i)));
                        }

                        break;
                    }
            }
            return availableResolutions;
        }
    }
}