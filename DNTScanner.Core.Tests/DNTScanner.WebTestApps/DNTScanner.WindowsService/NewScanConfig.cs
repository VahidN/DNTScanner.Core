namespace DNTScanner.WindowsService
{
    public enum ColorFormatType
    {
        Color,
        Greyscale,
        Text
    }

    public enum FileType
    {
        Jpeg,
        Bmp,
        Png,
        Gif,
        Tiff
    }

    public enum SourceType
    {
        Flatbed,
        AutomaticDocumentFeeder,
        Duplex
    }

    public class NewScanConfig
    {
        public string Name { set; get; }

        public SourceType Source { get; set; }

        public ColorFormatType ColorFormat { get; set; }

        public FileType FileType { get; set; }

        public int Resolution { get; set; }

        public int Brightness { get; set; }

        public int Contrast { get; set; }
    }
}