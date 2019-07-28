namespace DNTScanner.Core
{
    /// <summary>
    /// Document source properties
    /// </summary>
    public static class WiaDpsDocumentHandlingSelect
    {
        /// <summary>
        /// FEEDER = 0x001
        /// </summary>
        public const uint FEEDER = 0x001;

        /// <summary>
        /// FLATBED = 0x002
        /// </summary>
        public const uint FLATBED = 0x002;

        /// <summary>
        /// DUPLEX = 0x004
        /// </summary>
        public const uint DUPLEX = 0x004;

        /// <summary>
        /// FRONT_FIRST = 0x008
        /// </summary>
        public const uint FRONT_FIRST = 0x008;

        /// <summary>
        /// BACK_FIRST = 0x010
        /// </summary>
        public const uint BACK_FIRST = 0x010;

        /// <summary>
        /// FRONT_ONLY = 0x020
        /// </summary>
        public const uint FRONT_ONLY = 0x020;

        /// <summary>
        /// BACK_ONLY = 0x040
        /// </summary>
        public const uint BACK_ONLY = 0x040;

        /// <summary>
        /// NEXT_PAGE = 0x080
        /// </summary>
        public const uint NEXT_PAGE = 0x080;

        /// <summary>
        /// PREFEED = 0x100
        /// </summary>
        public const uint PREFEED = 0x100;

        /// <summary>
        /// AUTO_ADVANCE = 0x200
        /// </summary>
        public const uint AUTO_ADVANCE = 0x200;

        /// <summary>
        /// ADVANCED_DUPLEX = 0x400
        /// </summary>
        public const uint ADVANCED_DUPLEX = 0x400;
    }
}