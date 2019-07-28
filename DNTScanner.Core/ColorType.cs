namespace DNTScanner.Core
{
    /// <summary>
    /// Color format of the scanned image.
    /// </summary>
    public enum ColorFormat
    {
        /// <summary>
        /// Color = 1
        /// </summary>
        Color = 1,

        /// <summary>
        /// Greyscale = 2
        /// </summary>
        Greyscale = 2,

        /// <summary>
        /// BlackAndWhite = 4
        /// </summary>
        BlackAndWhite = 4
    }

    /// <summary>
    /// Color type of the scanned image.
    /// </summary>
    public sealed class ColorType
    {
        /// <summary>
        /// Color type of the scanned image.
        /// </summary>
        private ColorType(ColorFormat format, int bitsPerPixel)
        {
            Format = format;
            BitsPerPixel = bitsPerPixel;
        }

        internal ColorFormat Format { get; }

        internal int BitsPerPixel { get; }

        /// <summary>
        /// BlackAndWhite, BitsPerPixel = 1
        /// </summary>
        public static ColorType BlackAndWhite => new ColorType(ColorFormat.BlackAndWhite, 1);

        /// <summary>
        /// Greyscale, BitsPerPixel = 8
        /// </summary>
        public static ColorType Greyscale => new ColorType(ColorFormat.Greyscale, 8);

        /// <summary>
        /// Color, BitsPerPixel = 24
        /// </summary>
        public static ColorType Color => new ColorType(ColorFormat.Color, 24);
    }
}