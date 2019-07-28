using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace DNTScanner.ASPNETCoreApp.Models
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

    public class NewScanViewModel
    {
        public string Name { set; get; }

        [EnumDataType(typeof(SourceType))]
        public SourceType Source { get; set; }

        [DisplayName("Color Format")]
        [EnumDataType(typeof(ColorFormatType))]
        public ColorFormatType ColorFormat { get; set; }

        [DisplayName("File Type")]
        [EnumDataType(typeof(FileType))]
        public FileType FileType { get; set; }

        public int Resolution { get; set; }

        [JsonIgnore]
        public List<SelectListItem> Resolutions { get; set; }

        public int Brightness { get; set; }

        public int Contrast { get; set; }
    }
}