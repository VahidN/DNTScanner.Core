namespace DNTScanner.Core
{
    /// <summary>
    /// WiaImage Format
    /// </summary>
    public sealed class WiaImageFormat
    {
        private WiaImageFormat(string value) { Value = value; }

        /// <summary>
        /// Gets or sets a WiaImage Format
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// BMP format
        /// </summary>
        public static WiaImageFormat Bmp => new WiaImageFormat("{B96B3CAB-0728-11D3-9D7B-0000F81EF32E}");

        /// <summary>
        /// PNG format
        /// </summary>
        public static WiaImageFormat Png => new WiaImageFormat("{B96B3CAF-0728-11D3-9D7B-0000F81EF32E}");

        /// <summary>
        /// GIF format
        /// </summary>
        public static WiaImageFormat Gif => new WiaImageFormat("{B96B3CB0-0728-11D3-9D7B-0000F81EF32E}");

        /// <summary>
        /// JPG format
        /// </summary>
        public static WiaImageFormat Jpeg => new WiaImageFormat("{B96B3CAE-0728-11D3-9D7B-0000F81EF32E}");

        /// <summary>
        /// TIFF format
        /// </summary>
        public static WiaImageFormat Tiff => new WiaImageFormat("{B96B3CB1-0728-11D3-9D7B-0000F81EF32E}");
    }
}