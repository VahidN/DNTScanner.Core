namespace DNTScanner.Core
{
    /// <summary>
	/// Scanner Picture Settings
	/// </summary>
	public enum ScannerPictureSetting
    {
        /// <summary>
        /// Current Intent
        /// The CurrentIntent property contains the current settings for an application's intended use of an image. The WIA minidriver creates and maintains this property.
        /// A driver uses the intent settings to pre-set item properties based on an application's intended use of an image. These properties might include, for example, maximum quality and minimum size.
        /// The driver chooses the bit depth, in dots per inch, and other settings that it determines are appropriate for the selected intent. The application must read the current settings to determine which properties were changed.
        /// An application sets the WIA_IPS_CUR_INTENT(0x1802) property to auto-set the WIA properties for specific acquisition intent. Note that flags can be combined with a bitwise OR operator, but an image cannot be both grayscale and color.
        /// WIA_IPS_CUR_INTENT is required for all image acquisition enabled items; it is not available for storage items or stored image items.
        /// </summary>
        CurrentIntent = 6146,

        /// <summary>
        /// Horizontal Resolution
        /// The HorizontalResolution property contains the current horizontal resolution, in pixels per inch, for a device.
        /// An application sets the HorizontalResolution property to set the horizontal resolution. The WIA minidriver creates and maintains this property.
        /// If a device can be set to only a single value, create a WIA_PROP_LIST type and place the valid value in it. This situation also applies when one resolution setting depends on another resolution. (For example, the vertical resolution can depend on the horizontal resolution.)
        /// HorizontalResolution is required for all image acquisition-enabled items and stored image items; it is not available for storage items.
        /// WIA_IPS_XRES = 0x1803
        /// </summary>
        HorizontalResolution = 6147,

        /// <summary>
        /// Vertical Resolution
        /// The VerticalResolution property contains the current vertical resolution setting, in pixels per inch, for a device.
        /// An application sets the VerticalResolution property to set the vertical resolution. The WIA minidriver creates and maintains this property.
        /// If a device can be set to only a single value, create a WIA_PROP_LIST type and place the valid value in it. This situation also applies when one resolution setting depends on another resolution. (For example, the vertical resolution can depend on the horizontal resolution.)
        /// VerticalResolution is required for all image acquisition-enabled items and stored image items; it is not available for storage items.
        /// WIA_IPS_YRES = 0x1804
        /// </summary>
        VerticalResolution = 6148,

        /// <summary>
        /// Horizontal Start Position
        /// The HorizontalStartPosition property contains the x-coordinate, in pixels, of the upper-left corner of a selected image. The WIA minidriver creates and maintains this property.
        /// An application sets the HorizontalStartPosition property to mark the upper-left corner of a selection area.
        /// HorizontalStartPosition is required for all image acquisition-enabled items and child items of these items; this property is not available for storage items or stored image items.
        /// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_YEXTENT,  and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set HorizontalStartPosition to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
        /// When the origin or one extent is changed, the driver has to update WIA_IPS_PAGE_SIZE to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
        /// A driver must also update the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
        /// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, HorizontalStartPosition, WIA_IPS_XRES, WIA_IPS_YEXTENT, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
        /// WIA_IPS_XPOS = 0x1805
        /// </summary>
        HorizontalStartPosition = 6149,

        /// <summary>
        /// Vertical Start Position
        /// The VerticalStartPosition property contains the current y-coordinate, in pixels, of the upper-left corner of a selected image. The WIA minidriver creates and maintains this property.
        /// An application sets the VerticalStartPosition property to mark the upper-left corner of a selection area.
        /// VerticalStartPosition is required for all image acquisition-enabled items and child items of these items; this property is not available for storage items or stored image items.
        /// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_YEXTENT,  and VerticalStartPosition properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and VerticalStartPosition to ((scan area height - document height) / 2) * resolution [DPI]).
        /// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE property to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
        /// A driver must also update WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and VerticalStartPosition properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
        /// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_XRES, WIA_IPS_YEXTENT, VerticalStartPosition, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
        /// WIA_IPS_YPOS = 0x1806
        /// </summary>
        VerticalStartPosition = 6150,

        /// <summary>
        /// Horizontal Extent
        /// The HorizontalExtent property contains the current width, in pixels, of a selected image to acquire.
        /// An application sets the HorizontalExtent property to mark the upper-left corner (that is, the width) of the selection area to acquire. HorizontalExtent must agree with the WIA_IPA_PIXELS_PER_LINE property. The minidriver creates and maintains this property.
        /// HorizontalExtent is required for all image acquisition enabled items and child items of these items; this property is not available for storage items or stored image items.
        /// When a fixed page size is set, the driver has to set the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
        /// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE property to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
        /// A driver must also to update the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_YEXTENT, and WIA_IPS_YPOS properties when WIA_IPS_XRES and WIA_IPS_YRES are changed.
        /// Note: Flatbed and Film child items must support only the HorizontalExtent, WIA_IPS_XPOS, WIA_IPS_XRES, WIA_IPS_YEXTENT, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
        /// WIA_IPS_XEXTENT = 0x1807
        /// </summary>
        HorizontalExtent = 6151,

        /// <summary>
        /// Vertical Extent
        /// The VerticalExtent property contains the current height, in pixels, of a selected image to acquire.
        /// An application sets the VerticalExtent property to mark the upper-left corner (that is, the height) of a selection area to acquire. VerticalExtent must agree with the value of the WIA_IPA_NUMBER_OF_LINES property. The WIA minidriver creates and maintains this property.
        /// VerticalExtent is required for all image acquisition enabled items and child items of these items; this property is not available for storage items or stored image items.
        /// When a fixed page size is set, the driver has to set the WIA_IPS_XEXTENT, WIA_IPS_XPOS, VerticalExtent, and WIA_IPS_YPOS properties to match the page size dimensions and a "0" origin. For center document alignment, the driver has to set WIA_IPS_XPOS to ((scan area width - document width) / 2) * resolution [DPI]) and WIA_IPS_YPOS to ((scan area height - document height) / 2) * resolution [DPI]).
        /// When the origin or one extent is changed, the driver has to update the WIA_IPS_PAGE_SIZE to CUSTOM_SIZE and the WIA_IPS_PAGE_WIDTH and WIA_IPS_PAGE_HEIGHT properties to match the scan area extents. Orientation and rotation should not affect these properties, unless an orientation change (not a rotation change) renders the origin or one extent outside of the available document scan area.
        /// A driver must also update the WIA_IPS_XEXTENT, WIA_IPS_XPOS, VerticalExtent, and WIA_IPS_YPOS properties when the WIA_IPS_XRES and WIA_IPS_YRES properties are changed.
        /// Note: Flatbed and Film child items are required to support only the WIA_IPS_XEXTENT, WIA_IPS_XPOS, WIA_IPS_XRES, VerticalExtent, WIA_IPS_YPOS, and WIA_IPS_YRES properties. All other properties, required or optional for their parent (the base Flatbed or Film items), are only optional for these items. The only exceptions are the WIA_IPA_ITEM_Xxx properties, which are required for all items.
        /// WIA_IPS_YEXTENT = 0x1808
        /// </summary>
        VerticalExtent = 6152,

        /// <summary>
        /// Photometric Interpretation
        /// The PhotometricInterpretation property contains the current setting for white and black pixels. The WIA minidriver creates and maintains this property.
        /// An application reads the PhotometricInterpretation property to determine the value assigned to white or black pixels (depending on what the application is doing).
        /// If a device can be set to only a single value, create a WIA_PROP_LIST type, and place the valid value in it.
        /// The PhotometricInterpretation property is required for all image acquisition items and stored images.
        /// WIA_IPS_PHOTOMETRIC_INTERP = 0x1809
        /// </summary>
        PhotometricInterpretation = 6153,

        /// <summary>
        /// Brightness
        /// The Brightness property contains the current hardware brightness setting for a device.
        /// An application sets the Brightness property to the hardware's brightness value. The WIA minidriver creates and maintains this property.
        /// Values for Brightness should be mapped in a range from ˆ’1000 through 1000, where 1000 corresponds to the maximum brightness, 0 corresponds to normal brightness, and ˆ’1000 corresponds to the minimum brightness.
        /// Brightness is required for all image acquisition items.
        /// WIA_IPS_BRIGHTNESS = 0x180a
        /// </summary>
        Brightness = 6154,

        /// <summary>
        /// Contrast
        /// The Contrast property contains the current hardware contrast setting for a device.
        /// An application sets the Contrast property to the hardware's contrast value. The WIA minidriver creates and maintains this property.
        /// Values for Contrast should be mapped in a range from ˆ’1000 through 1000, where ˆ’1000 corresponds to the minimum contrast, 0 corresponds to normal contrast, and 1000 corresponds to the maximum contrast.
        /// Contrast is required for all image acquisition items.
        /// WIA_IPS_CONTRAST = 0x180b
        /// </summary>
        Contrast = 6155,

        /// <summary>
        /// Orientation
        /// The Orientation property describes the current orientation of the document to scan. The WIA minidriver creates and maintains this property.
        /// An application sets the Orientation property to define the original orientation of a page or image to be acquired. For more information about how to use Orientation, see WIA_DPS_PAGE_SIZE.
        /// The Orientation property describes the orientation of the document to scan. This property affects the current scan frame and available page sizes.
        /// Orientationis different from the WIA_IPS_ROTATION property, which refers to a rotation that is applied to an image after it is scanned. So, a ROT180 value for Orientation is different from a ROT180 value for WIA_IPS_ROTATION. For Orientation, ROT180 describes the orientation of the physical document to scan, relative to the scan direction, and for WIA_IPS_ROTATION, ROT180 describes the rotation to apply to an image after it is scanned.
        /// The Orientation property is required for ADF items and optional for all other image acquisition items.
        /// Note: The compatibility layer within the WIA service does not add support for Orientation to the ADF item that is translated from a Microsoft Windows XP WIA device if the property is not supported on the child item of the device. Applications should not expect that an ADF item will always support this property and should always check if Orientation is supported at run time.
        /// WIA_IPS_ORIENTATION = 0x180c
        /// </summary>
        Orientation = 6156,

        /// <summary>
        /// Rotation
        /// The Rotation property contains the current rotation setting for image rotation, if it is implemented. The WIA minidriver creates and maintains this property.
        /// An application sets the Rotation property to inform a driver how much (if at all) to rotate an image before the driver returns it to the application.
        /// The WIA minidriver is responsible for rotating image data before sending it back to the application. The application is responsible for checking the image headers to see the newly rotated values.
        /// It can be difficult to understand the effect of rotation on the current image's selection area (which is defined by the WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties).
        /// Selection area refers to the selected area on the physical scanner bed that an image is be acquired from. The Rotation property does not modify the selection area. The driver applies a counterclockwise rotation according to Rotation only after the driver has acquired the appropriate selection area. Rotation does affect the dimensions of the output image, so these dimensions must be reflected in the resulting image's data header.
        /// WIA_IPS_YEXTENT is not related to WIA_IPS_ORIENTATION. WIA_IPS_ORIENTATION describes the orientation of the document to be scanned relative to the direction of the scan; in contrast, Rotation describes the rotation that is to be applied to an image after it is scanned.
        /// WIA_IPS_ORIENTATION can impact the area to be scanned. Not all page sizes are available in both landscape and portrait, and the extents of the image from an change in WIA_IPS_ORIENTATION could crop the image. Rotation does not impact the image extents and is not related to the orientation of the document that is to be scanned.
        /// WIA_IPS_ROTATION = 0x180d
        /// </summary>
        Rotation = 6157,

        /// <summary>
        /// Threshold
        /// The Threshold property contains the current hardware threshold setting for a device. The WIA minidriver creates and maintains this property.
        /// You should map values for the Threshold property in a range from 0 through 255. The default value is 128.
        /// An application sets Threshold to change the hardware threshold value. This value is valid only if the WIA_IPA_DATATYPE property is equal to WIA_DATA_THRESHOLD. If a device does not allow WIA_DATA_THRESHOLD to be changed, it should report the default value of 128.
        /// WIA_IPS_THRESHOLD = 0x180f
        /// </summary>
        Threshold = 6159,

        /// <summary>
        /// DeskewX
        /// The DeskewX property, together with the WIA_IPS_DESKEW_Y property, describes the upper two corners of a skewed image. The WIA minidriver creates and maintains this property.
        /// The DeskewX and WIA_IPS_DESKEW_Y properties describe where the two upper corners of a skewed image are located within the bounding rectangle that WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties define.
        /// The valid values for DeskewX must be between 0 and (WIA_IPS_XEXTENT - 1). A value of 0 means that no skew correction should be performed.
        /// DeskewX contains the number of pixels in the x-direction from WIA_IPS_XPOS to the x-coordinate of the uppermost corner of the image to be corrected.
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_DESKEW_X = 0x1812
        /// </summary>
        DeskewX = 6162,

        /// <summary>
        /// DeskewY
        /// The DeskewY property, together with the WIA_IPS_DESKEW_X property, describes the upper two corners of a skewed image. The WIA minidriver creates and maintains this property.
        /// The WIA_IPS_DESKEW_X and DeskewY properties describe where the two upper corners of a skewed image are located within the bounding rectangle that the WIA_IPS_XPOS, WIA_IPS_YPOS, WIA_IPS_XEXTENT, and WIA_IPS_YEXTENT properties define.
        /// The valid values for DeskewY must be between 0 and (WIA_IPS_YEXTENT - 1). A value of 0 means that no deskew should be performed.
        /// DeskewY contains the number of pixels in the y-direction from WIA_IPS_YPOS to the y-coordinate of the leftmost corner of the image to be deskewed.
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_DESKEW_Y = 0x1813
        /// </summary>
        DeskewY = 6163,

        /// <summary>
        /// Segmentation
        /// You must implement Segmentation  for scanner flatbed and film items if they support the creation of child items with a segmentation filter or if the driver itself creates child items for fixed frames.
        /// You can package a driver with a segmentation filter and still have Segmentation set to WIA_DONT_USE_SEGMENTATION_FILTER for one of its items (for example, the film item). This situation could occur if the scanner uses fixed frames for film scanning, but not for scanning from the flatbed.
        /// WIA_IPS_SEGMENTATION = 0x1814
        /// </summary>
        Segmentation = 6164,

        /// <summary>
        /// Document Handling Select
        /// The DocumentHandlingSelect property contains the current scanner acquisition source and mode.
        /// An application reads the DocumentHandlingSelect property to determine the current acquisition source of a scanner, or the application writes this property to set the source and mode of the scanner. In addition, applications use this property to enable and disable duplexer functionality. The WIA minidriver creates and maintains this property.
        /// The values DUPLEX and FRONT_ONLY are mutually exclusive - set one or the other, but not both.
        /// A WIA 2.0 minidriver must set the initial value of this property to its default value, FRONT_ONLY. Failure to observe this requirement might make the minidriver incompatible with the WIA 1.0 common scan dialog and with some WIA 1.0 applications.
        /// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the identical WIA_DPS_DOCUMENT_HANDLING_SELECT property.
        /// WIA_IPS_DOCUMENT_HANDLING_SELECT = 0xc10
        /// </summary>
        DocumentHandlingSelect = 3088,

        /// <summary>
        /// Pages
        /// The Pages property contains the current number of pages to acquire from an automatic document feeder.
        /// An application reads Pages to determine a document feeder's page capacity. The application sets this property to the maximum number of pages it is willing to scan in the current WIA session. The WIA minidriver creates and maintains this property.
        /// Note: If duplex mode is enabled (that is, if WIA_IPS_DOCUMENT_HANDLING_SELECT is set to FEEDER | DUPLEX), Pages is still equal to the number of pages to scan.One sheet of paper will automatically contain two pages if DUPLEX is enabled, even if the back side of the page is blank.
        /// If you set Pages to 1, the scanner will process one of the sides of the page. We recommend that, if a scanner is unable to scan only one side of a page while in duplex mode, you should change the Pages value for the Inc member of the WIA_PROPERTY_INFO structure to 2. With this value, the application must request pages in multiples of two. A value of zero means that all pages that are currently loaded into the document feeder are to be scanned.
        /// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGES property instead.
        /// WIA_IPS_PAGES = 0xc18
        /// </summary>
        Pages = 3096,

        /// <summary>
        /// Page Size
        /// The PageSize property contains the size of the page that is currently selected to be scanned.
        /// To select the dimensions of the page to scan, an application sets the PageSize property. The WIA minidriver creates and maintains this property.
        /// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PAGE_SIZE property instead.
        /// WIA_IPS_PAGE_SIZE = 0xc19
        /// </summary>
        PageSize = 3097,

        /// <summary>
        /// Preview
        /// The Preview property indicates the preview mode for a device.
        /// An application sets Preview to place a device into a preview mode.
        /// Versions: Available in Windows Vista and later operating systems. For Windows XP, use the WIA_DPS_PREVIEW property instead.
        /// WIA_IPS_PREVIEW = 0xc1c
        /// </summary>
        Preview = 3100,

        /// <summary>
        /// Film Scan Mode
        /// The FilmScanMode property contains the current film scan configuration settings. The WIA minidriver creates and maintains this property.
        /// This property is required for the root item in the WIA item tree of film scanners and transparency adapters.
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_FILM_SCAN_MODE = 0xc20
        /// </summary>
        FilmScanMode = 3104,

        /// <summary>
        /// Lamp
        /// The Lamp property contains the current configuration setting for a scanner's lamp. The WIA minidriver creates and maintains this property.
        /// The Lamp property enables the programmatic control of the scanner lamp; this lamp could be a dedicated lamp (for a transparency adapter) or the main scanner lamp (for dedicated film scanners).
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_LAMP = 0xc21
        /// </summary>
        Lamp = 3105,

        /// <summary>
        /// Lamp Auto Off
        /// The AutoOff property contains the current configuration setting for automatically shutting off a scanner's lamp. The WIA minidriver creates and maintains this property.
        /// The AutoOff property enables the programmatic control of how long a lamp will be kept on when a scanner is not in use; this lamp could be a dedicated lamp (for a transparency adapter) or the main scanner lamp (for dedicated film scanners).
        /// You should implement AutoOff only if the device supports an automatic lamp-off feature.
        /// The valid values for AutoOff range from 0 through 4095 seconds.
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_LAMP_AUTO_OFF = 0xc22
        /// </summary>
        AutoOff = 3106,

        /// <summary>
        /// Automatic Deskew
        /// The AutoDeskew property indicates if a device should use automatic skew correction. The WIA minidriver creates and maintains this property.
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_AUTO_DESKEW = 0xc23
        /// </summary>
        AutoDeskew = 3107,

        /// <summary>
        /// Horizontal Scaling
        /// The HorizontalScaling property indicates if scaling along the x-axis should be applied to a scan. The WIA minidriver creates and maintains this property.
        /// Valid values for the HorizontalScaling property range from 1 through 65535.
        /// HorizontalScaling indicates only scaling along the x-axis. If you want to scale an image uniformly, you must set a similar value in HorizontalScaling and in the WIA_IPS_YSCALING property.
        /// Consider the following examples:
        /// 100, no scaling (1x, 100%). The image is not changed.
        /// 050, 1/2 scaling (1/2x, 50%). The image size is reduced along the x-axis by 50% (1/2 the original size).
        /// 200, 2x scaling (200%). The image size is enlarged along the x-axis by 200% (double).
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_XSCALING = 0xc25
        /// </summary>
        HorizontalScaling = 3109,

        /// <summary>
        /// Vertical Scaling
        /// The VerticalScaling property indicates if scaling along the y-axis should be applied to a scan. The WIA minidriver creates and maintains this property.
        /// Valid values for the VerticalScaling property range from 1 through 65535.
        /// VerticalScaling indicates only scaling along the y-axis. If you want to scale an image uniformly, you must set a similar value in VerticalScaling and in the WIA_IPS_XSCALING property.
        /// Consider the following examples:
        /// 100, no scaling (1x, 100%). The image is not changed.
        /// 050, 1/2 scaling (1/2x, 50%). The image size is reduced along the y-axis by 50% (1/2 the original size).
        /// 200, 2x scaling (200%). The image size is enlarged along the y-axis by 200% (double).
        /// Versions: Available in Windows Vista and later operating systems.
        /// WIA_IPS_YSCALING = 0xc26
        /// </summary>
        VerticalScaling = 3110,

        /// <summary>
        /// Film Node Name
        /// Enables specification of a particular film scanning attachment when there is more than one.
        /// This property is required for the WIA_CATEGORY_FILM items when there are multiple film scan items. If the device supports only one root scanner film item then this property is optional.
        /// Note: This property is supported only by Windows Vista and later.
        /// WIA_IPS_FILM_NODE_NAME = 0x1021
        /// </summary>
        FilmNodeName = 4129,

        /// <summary>
        /// Item Time Stamp
        /// The ItemTime property contains the time that an image was originally captured.
        /// WIA_IPA_ITEM_TIME = 0x1004
        /// </summary>
        ItemTime = 4100,

        /// <summary>
        /// Access Rights
        /// The AccessRights property contains the access rights for a WIA item.
        /// Access rights control the ability of an application to delete items in the WIA item tree. The WIA minidriver creates and maintains the AccessRights property.
        /// WIA_IPA_ACCESS_RIGHTS = 0x1006
        /// </summary>
        AccessRights = 4102,

        /// <summary>
        /// Data Type
        /// The DataType property contains the current data type setting for a device. A WIA minidriver creates and maintains this property.
        /// An application reads the DataType property to determine the data type of an image. The application writes this property to set the current data type of the image that is about to be transferred.
        /// The DataType property usually contains a single value for cameras.
        /// WIA_IPA_DATATYPE = 0x1007
        /// </summary>
        DataType = 4103,

        /// <summary>
        /// Bits Per Pixel
        /// The Depth property contains the bit depth setting of an image. The WIA minidriver creates and maintains this property.
        /// An application reads the Depth property to determine the bit depth setting of an image. The application might also set this value to the desired bit depth.
        /// If you can set the device to only a single value, create a WIA_PROP_LIST type and place the valid value in it.
        /// WIA_IPA_DEPTH = 0x1008
        /// </summary>
        Depth = 4104,

        /// <summary>
        /// Format
        /// The Format property contains the current format of the image that is about to be transferred. The WIA minidriver creates and maintains this property.
        /// If you can set the device to only a single value, create a WIA_PROP_LIST type, and place the valid value in it.
        /// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is WiaImgFmt_BMP.
        /// WIA_IPA_FORMAT = 0x100a
        /// </summary>
        Format = 4106,

        /// <summary>
        /// Compression
        /// The Compression property contains the current compression type that is used. The WIA minidriver creates and maintains this property.
        /// An application reads the Compression property to determine the image compression type, or the application sets this property to configure the compression setting.
        /// Note: When the file format is WiaImgFmt_XPS or WiaImgFmt_PDFA, WIA_COMPRESSION_NONE means €œnot defined€; the device cannot choose the internal compression (if any) for images that are stored in these two document formats.
        /// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is WIA_COMPRESSION_NONE.
        /// The access rights of the Compression property are read/write for all image acquisitions but read-only for stored image items.
        /// WIA_IPA_COMPRESSION = 0x100b
        /// </summary>
        Compression = 4107,

        /// <summary>
        /// Media Type
        /// The MediaType property contains the method setting for image transfer . The WIA minidriver creates and maintains this property.
        /// An application reads the MediaType property to determine the minidriver's method of data transfer.
        /// All WIA 2.0 minidrivers must set the initial value of this property to its default value, which is TYMED_FILE.
        /// WIA_IPA_TYMED = 0x100c
        /// </summary>
        MediaType = 4108,

        /// <summary>
        /// Planar
        /// The Planar property contains image data packing options. The WIA minidriver creates and maintains this property.
        /// An application reads Planar to determine the image packing options or sets the current image packing options.
        /// If a device can be set to only a single value, you can implement the Planar property as WIA_PROP_NONE and read-only.
        /// Versions: Obsolete in Windows Vista and later operating system.
        /// WIA_IPA_PLANAR = 0x100f
        /// </summary>
        Planar = 4111,

        /// <summary>
        /// Color Profile Name
        /// The IcmProfileName property contains the image color management (ICM) profile name that is needed to properly decode an image.
        /// An application reads the IcmProfileName property to determine the ICM profile to use when processing the image. The WIA service creates and maintains this property based on the ICMProfiles entry in the driver installation file.
        /// WIA_IPA_ICM_PROFILE_NAME = 0x1018
        /// </summary>
        IcmProfileName = 4120,

        /// <summary>
        /// Upload Item Size
        /// The UploadItemSize property is used by applications to specify the number of bytes to upload for an item. The application creates and maintains this property.
        /// Versions: Available on Windows Vista and later operating systems.
        /// WIA_IPA_UPLOAD_ITEM_SIZE = 0x101e
        /// </summary>
        UploadItemSize = 4126
    }
}