namespace DNTScanner.Core
{
    /// <summary>
    /// Document source status
    /// </summary>
    public static class WiaDpsDocumentHandlingStatus
    {
        /// <summary>
        /// FEED_READY = 0x01
        /// </summary>
        public const uint FEED_READY = 0x01;

        /// <summary>
        /// FLAT_READY = 0x02
        /// </summary>
        public const uint FLAT_READY = 0x02;


        /// <summary>
        /// DUP_READY = 0x04
        /// </summary>
        public const uint DUP_READY = 0x04;

        /// <summary>
        /// FLAT_COVER_UP = 0x08
        /// </summary>
        public const uint FLAT_COVER_UP = 0x08;

        /// <summary>
        /// PATH_COVER_UP = 0x10
        /// </summary>
        public const uint PATH_COVER_UP = 0x10;

        /// <summary>
        /// PAPER_JAM = 0x20
        /// </summary>
        public const uint PAPER_JAM = 0x20;

        /// <summary>
        /// FILM_TPA_READY = 0x40
        /// </summary>
        public const uint FILM_TPA_READY = 0x40;

        /// <summary>
        /// STORAGE_READY = 0x80
        /// </summary>
        public const uint STORAGE_READY = 0x80;

        /// <summary>
        /// STORAGE_FULL = 0x100
        /// </summary>
        public const uint STORAGE_FULL = 0x100;

        /// <summary>
        /// MULTIPLE_FEED = 0x200
        /// </summary>
        public const uint MULTIPLE_FEED = 0x200;

        /// <summary>
        /// DEVICE_ATTENTION = 0x400
        /// </summary>
        public const uint DEVICE_ATTENTION = 0x400;

        /// <summary>
        /// LAMP_ERR = 0x800
        /// </summary>
        public const uint LAMP_ERR = 0x800;
    }
}