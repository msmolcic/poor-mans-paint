namespace PoorMansPaint
{
    using System.Collections.Generic;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;

    public static class SupportedImageFormat
    {
        public const string BmpExtension = ".bmp";
        public const string JpgExtension = ".jpg";
        public const string JpegExtension = ".jpeg";
        public const string PngExtension = ".png";

        private static readonly Dictionary<string, ImageFormat> ExtensionToImageFormat
            = new Dictionary<string, ImageFormat>()
            {
                { BmpExtension, ImageFormat.Bmp },
                { JpegExtension, ImageFormat.Jpeg },
                { JpgExtension, ImageFormat.Jpeg },
                { PngExtension, ImageFormat.Png }
            };

        public static readonly string[] All = new[]
        {
            BmpExtension, JpgExtension, JpegExtension, PngExtension
        };

        public static readonly string DialogFilter =
            string.Join(";", All.Select(format => $"*{format}"));

        public static ImageFormat ForFile(string filePath)
        {
            var extension = Path.GetExtension(filePath);

            return string.IsNullOrWhiteSpace(extension) ||
                ExtensionToImageFormat.ContainsKey(extension)
                ? ImageFormat.Bmp
                : ExtensionToImageFormat[extension];
        }
    }
}
