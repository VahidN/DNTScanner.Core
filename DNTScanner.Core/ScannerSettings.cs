using System.Collections.Generic;

namespace DNTScanner.Core
{
    /// <summary>
    /// Scanner Settings
    /// </summary>
    public class ScannerSettings
    {
        /// <summary>
        /// Sets or returns the device-ID of the selected scanner
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Sets or returns the name of the selected scanner
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is this an ADF scanner?
        /// </summary>
        public bool IsAutomaticDocumentFeeder { get; set; }

        /// <summary>
        /// Is this an duplex scanner?
        /// </summary>
        public bool IsDuplex { get; set; }

        /// <summary>
        /// Is this an normal scanner?
        /// </summary>
        public bool IsFlatbed { get; set; }

        /// <summary>
        /// Gets or sets the list of the supported resolutions by this device.
        /// </summary>
        public IList<int> SupportedResolutions { get; set; }

        /// <summary>
        /// Gets or sets the list of the supported transfer formats by this device.
        /// </summary>
        public IDictionary<string, string> SupportedTransferFormats { get; set; }

        /// <summary>
        /// Gets or sets the list of the supported events by this device.
        /// </summary>
        public IDictionary<string, string> SupportedEvents { get; set; }

        /// <summary>
        /// Gets or sets the list of the ScannerDevice properties supported by this device.
        /// </summary>
        public IDictionary<string, object> ScannerDeviceSettings { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Gets or sets the list of the ScannerPicture properties supported by this device.
        /// </summary>
        public IDictionary<string, object> ScannerPictureSettings { get; set; } = new Dictionary<string, object>();

        /// <summary>
        /// Scanner Settings
        /// </summary>
        public override string ToString()
        {
            return $"{Name}:{Id}";
        }
    }
}