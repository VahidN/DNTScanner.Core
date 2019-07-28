using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using DNTScanner.Core;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DNTScanner.WindowsService
{
    public interface IScannerService
    {
        Task DetectScanner(string message);
        Task DoScan(NewScanConfig model);
    }

    public class ScannerService : IScannerService
    {
        private readonly IScannerHubClient _hubClient;
        private ScannerSettings _firstScanner;
        private readonly AppConfig _appConfig;
        private readonly UploadApiClient _uploadApiClient;
        private readonly ILogger<ScannerService> _logger;

        public ScannerService(
            IScannerHubClient hubClient,
            UploadApiClient uploadApiClient,
            ILogger<ScannerService> logger,
            IOptions<AppConfig> appConfig)
        {
            _hubClient = hubClient;
            _appConfig = appConfig.Value;
            _uploadApiClient = uploadApiClient;
            _logger = logger;
        }

        public async Task DetectScanner(string message)
        {
            try
            {
                _logger.LogInformation(message);

                var scanners = SystemDevices.GetScannerDevices();
                _firstScanner = scanners.FirstOrDefault();
                if (_firstScanner == null)
                {
                    await _hubClient.CallScannerIsNotConnectedError();
                    return;
                }

                _logger.LogInformation($"{_firstScanner}");
                await _hubClient.CallGetScannerSettings(_firstScanner);
            }
            catch (COMException ex)
            {
                var friendlyErrorMessage = ex.GetComErrorMessage(); // How to show a better error message to users
                _logger.LogError(ex, friendlyErrorMessage);

                await _hubClient.CallGetErrors(friendlyErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "DetectScanner error.");
                await _hubClient.CallGetErrors(ex.Message);
            }
        }

        public async Task DoScan(NewScanConfig newScanConfig)
        {
            try
            {
                if (_firstScanner == null || newScanConfig == null)
                {
                    await _hubClient.CallScannerIsNotConnectedError();
                    return;
                }

                _logger.LogInformation($"Using {_firstScanner}");
                _logger.LogInformation($"{newScanConfig.Source}, {newScanConfig.ColorFormat}, DPI:{newScanConfig.Resolution}, {newScanConfig.FileType}");

                using (var scannerDevice = new ScannerDevice(_firstScanner))
                {
                    ColorType colorType;
                    switch (newScanConfig.ColorFormat)
                    {
                        case ColorFormatType.Color:
                            colorType = ColorType.Color;
                            break;
                        case ColorFormatType.Greyscale:
                            colorType = ColorType.Greyscale;
                            break;
                        case ColorFormatType.Text:
                            colorType = ColorType.BlackAndWhite;
                            break;
                        default:
                            colorType = ColorType.Color;
                            break;
                    }
                    scannerDevice.ScannerPictureSettings(config =>
                    {
                        config.ColorFormat(colorType)
                              .Resolution(newScanConfig.Resolution)
                              .Brightness(newScanConfig.Brightness)
                              .Contrast(newScanConfig.Contrast);
                    });

                    scannerDevice.ScannerDeviceSettings(config =>
                    {
                        if (newScanConfig.Source == SourceType.Duplex)
                        {
                            config.Source(DocumentSource.DoubleSided);
                        }
                        else if (newScanConfig.Source == SourceType.AutomaticDocumentFeeder)
                        {
                            config.Source(DocumentSource.SingleSided);
                        }
                    });

                    WiaImageFormat format;
                    switch (newScanConfig.FileType)
                    {
                        case FileType.Jpeg:
                            format = WiaImageFormat.Jpeg;
                            break;
                        case FileType.Bmp:
                            format = WiaImageFormat.Bmp;
                            break;
                        case FileType.Png:
                            format = WiaImageFormat.Png;
                            break;
                        case FileType.Gif:
                            format = WiaImageFormat.Gif;
                            break;
                        case FileType.Tiff:
                            format = WiaImageFormat.Tiff;
                            break;
                        default:
                            format = WiaImageFormat.Jpeg;
                            break;
                    }
                    scannerDevice.PerformScan(format);

                    scannerDevice.ProcessScannedImages(process =>
                    {
                        process.Compress(quality: 90);
                        // ...
                    });

                    var filesBytes = scannerDevice.ExtractScannedImageFiles().ToList();
                    await _uploadApiClient.PostImagesAsync(
                                    _appConfig.UploadImagesApiPath,
                                    filesBytes,
                                    _appConfig.UploadImagesApiParamName,
                                    $".{newScanConfig.FileType.ToString().ToLowerInvariant()}");
                }
            }
            catch (COMException ex)
            {
                var friendlyErrorMessage = ex.GetComErrorMessage(); // How to show a better error message to users
                _logger.LogError(ex, friendlyErrorMessage);

                await _hubClient.CallGetErrors(friendlyErrorMessage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Scan error.");
                await _hubClient.CallGetErrors(ex.Message);
            }
        }
    }
}