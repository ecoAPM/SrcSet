using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace ImageResizer
{
    public static class Resizer
    {
        public static void SaveSrcSet(string filePath, IEnumerable<ushort> widths)
        {
            using (var image = Image.Load(filePath))
            {
                var size = new Size(image.Width, image.Height);
                foreach(var newSize in widths.Select(width => size.Resize(width)))
                    SaveResizedImage(filePath, image, newSize);
            }
        }

        private static void SaveResizedImage(string filePath, Image<Rgba32> image, Size newSize)
        {
            var dir = Path.GetDirectoryName(filePath);
            var newFileName = FileHelpers.GetFilename(filePath, newSize);
            var newPath = Path.Combine(dir, newFileName);
            if (File.Exists(newPath))
                return;

            image.Mutate(i => i.Resize(newSize.Width, newSize.Height));
            image.Save(newPath);
            Console.WriteLine(newFileName);
        }
    }
}