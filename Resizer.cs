using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizer
{
    public static class Resizer
    {
        public static void Resize(string filePath, IEnumerable<int> widths)
        {
            using (var bmp = new Bitmap(filePath))
            {
                foreach (var width in widths)
                {
                    var aspectRatio = bmp.Size.AspectRatio();
                    var size = new Size(width, (int)(width / aspectRatio));
                    Resize(filePath, bmp, size);
                }
            }
        }

        private static void Resize(string filePath, Image bmp, Size newSize)
        {
            var dir = Path.GetDirectoryName(filePath);
            if (dir == null)
                throw new DirectoryNotFoundException();

            var file = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);
            var newFileName = $"{file}-{newSize.Width:D4}{extension}";
            var newPath = Path.Combine(dir, newFileName);
            if (File.Exists(newPath))
                return;
            var imageFormat = getImageFormat(extension);
            Console.WriteLine(newFileName);

            using (var newImage = new Bitmap(newSize.Width, newSize.Height))
            {
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(bmp, 0, 0, newSize.Width, newSize.Height);
                }

                newImage.Save(newPath, imageFormat);
            }
        }

        private static ImageFormat getImageFormat(string extension)
        {
            switch (extension.ToLower())
            {
                case ".jpeg":
                case ".jpg":
                    return ImageFormat.Jpeg;
                case ".png":
                    return ImageFormat.Png;
                case ".bmp":
                    return ImageFormat.Bmp;
                case ".gif":
                    return ImageFormat.Gif;
                case ".tif":
                case ".tiff":
                    return ImageFormat.Tiff;
                default:
                    throw new BadImageFormatException();
            }
        }
    }
}