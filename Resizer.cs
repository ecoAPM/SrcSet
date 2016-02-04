using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizer
{
    public class Resizer
    {
        private readonly string dir;
        private readonly string filename;
        private readonly IEnumerable<int> widths;

        public Resizer(string dir, string filename, IEnumerable<int> widths)
        {
            this.dir = dir;
            this.filename = filename;
            this.widths = widths;
        }
        public void Resize()
        {
            using (var bmp = new Bitmap(filename))
            {
                foreach (var width in widths)
                {
                    var aspectRatio = bmp.Size.AspectRatio();
                    var size = new Size(width, (int)(width / aspectRatio));
                    resize(bmp, size);
                }
            }
        }

        private void resize(Image bmp, Size newSize)
        {
            var file = Path.GetFileNameWithoutExtension(filename);
            var newFileName = file + "-" + (newSize.Width < 1000 ? "0" : "") + newSize.Width + ".jpg";
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

                newImage.Save(newPath, ImageFormat.Jpeg);
            }
        }
    }
}