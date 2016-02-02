using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace ImageResizer
{
    public class Resizer : IDisposable
    {
        private string dir;
        private string file;
        private Bitmap bmp;
        private IEnumerable<int> widths;

        public Resizer(string dir, string filename, IEnumerable<int> widths)
        {
            this.dir = dir;
            bmp = new Bitmap(filename);
            file = Path.GetFileNameWithoutExtension(filename);
            this.widths = widths;
        }
        public void Resize()
        {
            foreach (var width in widths)
            {
                var aspectRatio = bmp.Size.AspectRatio();
                var size = new Size(width, (int)(width / aspectRatio));
                resize(size);
            }
            bmp.Dispose();
        }

        private void resize(Size newSize)
        {
            var newFileName = file + "-" + (newSize.Width < 1000 ? "0" : "") + newSize.Width + ".jpg";
            var newPath = Path.Combine(dir, newFileName);
            if (File.Exists(newPath)) return;
            Console.WriteLine(newFileName);

            var newImage = new Bitmap(newSize.Width, newSize.Height);
            var graphics = Graphics.FromImage(newImage);
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.DrawImage(bmp, 0, 0, newSize.Width, newSize.Height);
            graphics.Dispose();

            newImage.Save(newPath, ImageFormat.Jpeg);
            newImage.Dispose();
        }

        public void Dispose()
        {
            dir = null;
            file = null;
            bmp = null;
            widths = null;
        }
    }
}