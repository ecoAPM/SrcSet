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

     
        private static void  Resize( string filePath, Image bmp, Size newSize)
        {
            var dir = Path.GetDirectoryName(filePath);
            var file = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath).ToLower();
            var imgFormat = ImageFormat.Jpeg;
            switch (extension)
            {
                case ".jpg":
                    break;
                case ".bmp":
                    {
                        imgFormat = ImageFormat.Bmp;
                        break;
                    }
                case ".png":
                    {
                        imgFormat = ImageFormat.Png;
                        break;
                    }
                case ".tif":
                case ".tiff":
                    {
                        imgFormat = ImageFormat.Tiff;
                        break;
                    }
                default:
                    {
                        throw new BadImageFormatException();
                    }
            }
            var newFileName =string.Format("{0}-{1:D4}{2}", file,(int) newSize.Width , extension);
            var newPath = Path.Combine(dir, newFileName);
            if (File.Exists(newPath)) return;
            Console.WriteLine(newFileName);

            using (var newImage = new Bitmap(newSize.Width, newSize.Height))
            {
                using (var graphics = Graphics.FromImage(newImage))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(bmp, 0, 0, newSize.Width, newSize.Height);
                }

                newImage.Save(newPath, imgFormat);
            }
        }
    }
}