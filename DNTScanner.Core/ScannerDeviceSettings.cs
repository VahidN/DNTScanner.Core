namespace DNTScanner.Core
{
    /// <summary>
    /// Scanner Device Settings
    /// </summary>
    public enum ScannerDeviceSettings
    {
        /// <summary>
        /// Pad Color
        /// The PadColor property contains the current pad color that is used when the WIA minidriver pads unaligned data. The WIA minidriver creates and maintains this property.
        /// The PadColor property should be reported as a vector of four BYTE values in the form of an RGBQUAD structure (which is described in the Microsoft Windows SDK documentation).
        /// An application reads PadColor to get the padding color that is used.
        /// </summary>
        PadColor = 3082,

        /// <summary>
        /// Document Handling Select
        /// The DocumentHandlingSelect property contains the current scanner acquisition source and mode.
        /// An application reads the DocumentHandlingSelect property to determine the current acquisition source of a scanner, or an application write this property to set the source and mode of the scanner. In addition, applications use this property to enable and disable duplexer functionality. The WIA minidriver creates and maintains this property.
        /// The values DUPLEX and FRONT_ONLY are mutually exclusive set one or the other, but not both.
        /// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the WIA_IPS_DOCUMENT_HANDLING_SELECT property.
        /// </summary>
        DocumentHandlingSelect = 3088,

        /// <summary> Endorser String
        /// The EndorserString property contains a string that is to be endorsed (that is, printed) on each page that the minidriver scans.
        /// An application sets the EndorserString property by using the valid character set that is reported in the WIA_DPS_ENDORSER_CHARACTERS property. The WIA minidriver should endorse documents only if a string is set in EndorserString. An empty string means that the endorser functionality is disabled.
        /// Because the driver must interpret the endorser string, your driver can use special characters in EndorserString. However, only your applications will understand these characters.
        /// A driver that supports the EndorserString property must support the following list of tokens:
        /// $DATE$	The date in the form YYYY/MM/DD.
        /// $DAY$	The day, in the form DD.
        /// $MONTH$	The month of the year, in the form MM.
        /// $PAGE_COUNT$	The number of pages that are transferred.
        /// $TIME$	The time of day, in the form HH:MM:SS.
        /// $YEAR$	The year, in the form YYYY.
        /// </summary>
        EndorserString = 3093,

        /// <summary>
        /// Scan Ahead Pages
        /// The ScanAheadPages property contains a value that indicates whether a scanner will cache pages in a scanner buffer before sending them to an application.
        /// If the ScanAheadPages property is zero, scan ahead is disabled, and the scanner will not scan ahead any pages.
        /// If the scanner performs data transfers on the buffered scan-ahead item, the scanner will retrieve the buffered pages. WIA properties cannot be changed during a scan-ahead operation. ScanAheadPages is optional.
        /// </summary>
        ScanAheadPages = 3094,

        /// <summary>
        /// Pages
        /// The Pages property contains the current number of pages to acquire from an automatic document feeder.
        /// An application reads the Pages property to determine a document feeder's page capacity. The application also sets this property to the number of pages it is going to scan. The WIA minidriver creates and maintains Pages.
        /// If you set Pages to zero (0)the scanner will process continuously until no more documents are fed into the ADF.
        /// Note: If duplex mode is enabled (that is, the WIA_DPS_DOCUMENT_HANDLING_SELECT property is set to FEEDER | DUPLEX), Pages is still equal to the number of pages to scan.One sheet of paper will automatically contain two pages if DUPLEX is enabled, even if the back side of the page is blank.
        /// If you set Pages to 1, the scanner will process one of the sides of the page. If a scanner is unable to scan only one side of a page while in duplex mode, you should change the Pages value for the Inc member of the WIA_PROPERTY_INFO structure to 2. This value signals to the application that it must request pages in multiples of two. If Pages is zero, the scanner will scan all pages that are currently loaded into the document feeder.
        /// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGES property.
        /// </summary>
        Pages = 3096,

        /// <summary>
        /// Page Size
        /// The PageSize property contains the size of the page that is currently selected to be scanned.
        /// To select the dimensions of the page to scan, an application sets PageSize. The WIA minidriver creates and maintains this property.
        /// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PAGE_SIZE property.
        /// </summary>
        PageSize = 3097,

        /// <summary>
        /// Preview
        /// The Preview property indicates the preview mode for a device. An application sets this property to place the device into a preview mode.
        /// Versions: Available for Microsoft Windows XP. For Windows Vista and later, use the identical WIA_IPS_PREVIEW property.
        /// </summary>
        Preview = 3100,

        /// <summary>
        /// Device Time
        /// The DeviceTime property contains the current clock time that is stored on a device. The minidriver creates and maintains this property.
        /// When the DeviceTime property is read, the minidriver should check the device's current clock time and should always return the current time. This property is supported only by devices that have an internal clock. If the device clock can be set, this property is read/write; otherwise, it is read-only. WIA devices report time in a SYSTEMTIME structure (which is described in the Microsoft Windows SDK documentation).
        /// </summary>
        DeviceTime = 1028
    }
}