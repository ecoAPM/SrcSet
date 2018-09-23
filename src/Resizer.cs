using System;
using System.Collections.Generic;
using System.IO;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Primitives;

namespace ImageResizer
{
    public static class Resizer
    {
        public static void Resize(string filePath, IEnumerable<int> widths)
        {
            using (var image = Image.Load(filePath))
            {
                var aspectRatio = new Size(image.Width, image.Height).AspectRatio();
                foreach (var width in widths)
                {
                    var size = new Size(width, (int)(width / aspectRatio));
                    Resize(filePath, image, size);
                }
            }
        }

        private static void Resize(string filePath, Image<Rgba32> image, Size newSize)
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

            image.Mutate(i => i.Resize(newSize.Width, newSize.Height));
            image.Save(newPath);
        }
    }
}