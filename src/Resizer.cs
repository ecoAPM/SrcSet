using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.Primitives;

namespace ImageResizer
{
    public class Resizer
    {
        private Image<Rgba32> _image;
        private IImageEncoder _encoder;

        public void Resize(string filePath, IEnumerable<int> widths)
        {
            using (var stream = File.OpenRead(filePath))
            {
                _image = Image.Load(stream);

                var filetype = Path.GetExtension(filePath);

                switch (filetype)
                {
                    case ".jpg":
                    case ".jpeg":
                        _encoder = new JpegEncoder{Quality = 255};
                        break;
                    case ".gif":
                        _encoder = new GifEncoder();
                        break;
                    case ".bmp":
                        _encoder = new BmpEncoder();
                        break;
                    case ".png":
                    case ".tif":
                    case ".tiff":
                        _encoder = new PngEncoder();
                        break;
                }

                var aspectRatio = new Size(_image.Width, _image.Height).AspectRatio();
                foreach (var width in widths)
                {
                    var size = new Size(width, (int)(width / aspectRatio));
                    Resize(filePath, size);
                }
            }
        }

        private void Resize(string filePath, Size newSize)
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

            Console.WriteLine(newFileName);

            _image.Mutate(x => x.Resize(newSize.Width, newSize.Height));


            using (var output = File.OpenWrite(newPath))
            {
                _image.Save(output, _encoder);
            }
        }
    }
}
