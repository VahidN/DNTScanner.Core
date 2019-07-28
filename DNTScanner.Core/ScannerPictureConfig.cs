using System;

namespace DNTScanner.Core
{
    /// <summary>
    /// Scanner Picture Config. Setting properties like ColorFormat, Brightess, etc.
    /// </summary>
    public class ScannerPictureConfig
    {
        private readonly WiaSettings _wiaScannerPictureSettings;
        private readonly ScannerSettings _scannerSettings;

        /// <summary>
        /// Scanner Picture Config. Setting properties like ColorFormat, Brightess, etc.
        /// </summary>
        public ScannerPictureConfig(WiaSettings wiaScannerPictureSettings, ScannerSettings scannerSettings)
        {
            _wiaScannerPictureSettings = wiaScannerPictureSettings;
            _scannerSettings = scannerSettings;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerPictureConfig SetPropertyValue(ScannerPictureSetting setting, object value)
        {
            _wiaScannerPictureSettings.SetPropertyValue((int)setting, value);
            return this;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerPictureConfig SetPropertyValue(int propertyId, object value)
        {
            _wiaScannerPictureSettings.SetPropertyValue(propertyId, value);
            return this;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerPictureConfig SetPropertyValue(string propertyName, object value)
        {
            _wiaScannerPictureSettings.SetPropertyValue(propertyName, value);
            return this;
        }

        /// <summary>
        /// Sets the color format of the scanned images.
        /// </summary>
        public ScannerPictureConfig ColorFormat(ColorType colorType)
        {
            SetPropertyValue("Current Intent", (int)colorType.Format);
            SetPropertyValue("Bits Per Pixel", colorType.BitsPerPixel);
            return this;
        }

        /// <summary>
        /// Sets the Brightness of the scanned images.
        /// Its value must be between -100 and 100.
        /// </summary>
        public ScannerPictureConfig Brightness(int value)
        {
            if (value > 100 || value < -100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Brightness value must be between -100 and 100.");
            }
            _wiaScannerPictureSettings.SetRangeSubTypePropertyValue("Brightness", value);
            return this;
        }

        /// <summary>
        /// Sets the Contrast of the scanned images.
        /// Its value must be between -100 and 100.
        /// </summary>
        public ScannerPictureConfig Contrast(int value)
        {
            if (value > 100 || value < -100)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "Contrast value must be between -100 and 100.");
            }
            _wiaScannerPictureSettings.SetRangeSubTypePropertyValue("Contrast", value);
            return this;
        }

        /// <summary>
        /// Sets the Horizontal and Vertical Resolution of the scanned images in pixels per inch.
        /// </summary>
        public ScannerPictureConfig Resolution(int value)
        {
            if (!_scannerSettings.SupportedResolutions.Contains(value))
            {
                throw new ArgumentOutOfRangeException(nameof(value),
                     $"`{value}` is not a valid resolution value. Supported resolution values are {string.Join(", ", _scannerSettings.SupportedResolutions)}.");
            }
            SetPropertyValue(ScannerPictureSetting.HorizontalResolution, value);
            SetPropertyValue(ScannerPictureSetting.VerticalResolution, value);
            return this;
        }

        /// <summary>
        /// Sets the Horizontal and Vertical StartPosition of the scanned images in pixels of the upper-left corner of a selected image.
        /// </summary>
        public ScannerPictureConfig StartPosition(int left, int top)
        {
            SetPropertyValue(ScannerPictureSetting.HorizontalStartPosition, left);
            SetPropertyValue(ScannerPictureSetting.VerticalStartPosition, top);
            return this;
        }

        /// <summary>
        /// Sets the Horizontal and Vertical Extent of the scanned images in pixels of a selected image.
        /// </summary>
        public ScannerPictureConfig Extent(int width, int height)
        {
            SetPropertyValue(ScannerPictureSetting.HorizontalExtent, width);
            SetPropertyValue(ScannerPictureSetting.VerticalExtent, height);
            return this;
        }
    }
}