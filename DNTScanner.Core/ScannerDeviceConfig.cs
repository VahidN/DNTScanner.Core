namespace DNTScanner.Core
{
    /// <summary>
    /// Scanner Device Config. Setting properties like DocumentSource.
    /// </summary>
    public class ScannerDeviceConfig
    {
        private readonly WiaSettings _wiaScannerDeviceSettings;

        /// <summary>
        /// Scanner Device Config. Setting properties like DocumentSource.
        /// </summary>
        public ScannerDeviceConfig(WiaSettings wiaScannerDeviceSettings)
        {
            _wiaScannerDeviceSettings = wiaScannerDeviceSettings;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerDeviceConfig SetPropertyValue(ScannerDeviceSettings setting, object value)
        {
            _wiaScannerDeviceSettings.SetPropertyValue((int)setting, value);
            return this;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerDeviceConfig SetPropertyValue(int propertyId, object value)
        {
            _wiaScannerDeviceSettings.SetPropertyValue(propertyId, value);
            return this;
        }

        /// <summary>
        /// Setting a custom property value
        /// </summary>
        public ScannerDeviceConfig SetPropertyValue(string propertyName, object value)
        {
            _wiaScannerDeviceSettings.SetPropertyValue(propertyName, value);
            return this;
        }

        /// <summary>
        /// Sets the different types of DocumentSources available to scanners.
        /// If your scanner is duplex or automatic document feeder, set these options.
        /// </summary>
        public ScannerDeviceConfig Source(DocumentSource source)
        {
            SetPropertyValue("Document Handling Select", (int)source);
            return this;
        }
    }
}