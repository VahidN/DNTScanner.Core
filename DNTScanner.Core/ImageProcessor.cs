using System;
using WIA;

namespace DNTScanner.Core
{
    /// <summary>
    /// Post processing of scanned images.
    /// </summary>
    public class ImageProcessor
    {
        private readonly ImageFile _imageFile;
        private readonly WiaSettings _wiaSettings;
        private readonly WiaImageFormat _imageFormat;
        private readonly ImageProcess _imageProcess = new ImageProcess();
        private readonly Filters _filters;

        /// <summary>
        /// Post processing of scanned images.
        /// </summary>
        public ImageProcessor(ImageFile imageFile, WiaSettings wiaSettings, WiaImageFormat imageFormat)
        {
            _imageFile = imageFile ?? throw new ArgumentNullException(nameof(imageFile));
            _wiaSettings = wiaSettings;
            _imageFormat = imageFormat;
            _filters = _imageProcess.Filters;
        }

        /// <summary>
        /// Compresses the scanned images.
        /// </summary>
        /// <param name="quality">The quality value must be between 1 and 100.</param>
        public ImageProcessor Compress(int quality)
        {
            if (quality > 100 || quality < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(quality), "The quality value must be between 1 and 100.");
            }

            _filters.Add(_imageProcess.FilterInfos["Convert"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["FormatID"], _imageFormat.Value);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Quality"], quality);

            return this;
        }

        internal ImageFile ApplyFilters()
        {
            return _imageProcess.Apply(_imageFile);
        }

        /// <summary>
        /// Creates a crop filter for the specified image.
        /// To specify pixels, enter a value greater than one (1).
        /// </summary>
        /// <param name="left">Specifies the how much to crop from the left side of the image.</param>
        /// <param name="top">Specifies the how much to crop from the top of the image.</param>
        /// <param name="right">Specifies the how much to crop from the right side of the image.</param>
        /// <param name="bottom">Specifies the how much to crop from the bottom of the image.</param>
        public ImageProcessor CropByPixels(int left, int top, int right, int bottom)
        {
            _filters.Add(_imageProcess.FilterInfos["Crop"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["Left"], left);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Top"], top);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Right"], right);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Bottom"], bottom);

            return this;
        }

        /// <summary>
        /// Creates a crop filter for the specified image.
        /// To specify a percentage, enter a value less than one (1), such as ".25".
        /// </summary>
        /// <param name="left">Specifies the how much to crop from the left side of the image.</param>
        /// <param name="top">Specifies the how much to crop from the top of the image.</param>
        /// <param name="right">Specifies the how much to crop from the right side of the image.</param>
        /// <param name="bottom">Specifies the how much to crop from the bottom of the image.</param>
        public ImageProcessor CropByPercentage(double left, double top, double right, double bottom)
        {
            _filters.Add(_imageProcess.FilterInfos["Crop"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["Left"], _imageFile.Width * left);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Top"], _imageFile.Height * top);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Right"], _imageFile.Width * right);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Bottom"], _imageFile.Height * bottom);

            return this;
        }

        /// <summary>
        /// Creates a stamp filter of the specified image.
        /// </summary>
        /// <param name="imageFilePath">The image that is used in the filter is placed over other images when you apply the filter.</param>
        /// <param name="left">The horizontal location within the image where the overlay is added. </param>
        /// <param name="top">The vertical location within the image where the overlay is added. </param>
        public ImageProcessor Stamp(string imageFilePath, int left, int top)
        {
            var imageThumb = new ImageFile();
            imageThumb.LoadFile(imageFilePath);

            _filters.Add(_imageProcess.FilterInfos["Stamp"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["ImageFile"], imageThumb);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Left"], left);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["Top"], top);

            return this;
        }

        /// <summary>
        ///  Creates a filter for rotating images.
        /// </summary>
        /// <param name="rotationAngle">Specifies the angle of rotation. Valid values are 0, 90, 180, 270, and 360.</param>
        /// <param name="flipHorizontal"> Flips the images horizontally.</param>
        /// <param name="flipVertical">Flips the images vertically.</param>
        public ImageProcessor RotateFlip(int rotationAngle, bool flipHorizontal, bool flipVertical)
        {
            _filters.Add(_imageProcess.FilterInfos["RotateFlip"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["FlipHorizontal"], flipHorizontal.ToString());
            _wiaSettings.SetPropertyValue(_filters[index].Properties["FlipVertical"], flipVertical.ToString());
            _wiaSettings.SetPropertyValue(_filters[index].Properties["RotationAngle"], rotationAngle);

            return this;
        }

        /// <summary>
        /// Creates a filter for resizing images.
        /// To specify pixels, enter a value greater than one (1).
        /// </summary>
        /// <param name="maximumWidth">Enter the desired width of the resized image.</param>
        /// <param name="maximumHeight">Enter the desired height of the resized image.</param>
        /// <param name="preserveAspectRatio">preserve the aspect ratio when resizing</param>
        public ImageProcessor ScaleByPixels(int maximumWidth, int maximumHeight, bool preserveAspectRatio)
        {
            _filters.Add(_imageProcess.FilterInfos["Scale"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["PreserveAspectRatio"], preserveAspectRatio.ToString());
            _wiaSettings.SetPropertyValue(_filters[index].Properties["MaximumWidth"], maximumWidth);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["MaximumHeight"], maximumHeight);

            return this;
        }

        /// <summary>
        /// Creates a filter for resizing images.
        /// To specify a percentage, enter a value less than one (1), such as ".25".
        /// </summary>
        /// <param name="maximumWidth">Enter the desired width of the resized image.</param>
        /// <param name="maximumHeight">Enter the desired height of the resized image.</param>
        /// <param name="preserveAspectRatio">preserve the aspect ratio when resizing</param>
        public ImageProcessor ScaleByPercentage(double maximumWidth, double maximumHeight, bool preserveAspectRatio)
        {
            _filters.Add(_imageProcess.FilterInfos["Scale"].FilterID);
            var index = _filters.Count;

            _wiaSettings.SetPropertyValue(_filters[index].Properties["PreserveAspectRatio"], preserveAspectRatio.ToString());
            _wiaSettings.SetPropertyValue(_filters[index].Properties["MaximumWidth"], _imageFile.Width * maximumWidth);
            _wiaSettings.SetPropertyValue(_filters[index].Properties["MaximumHeight"], _imageFile.Height * maximumHeight);

            return this;
        }
    }
}