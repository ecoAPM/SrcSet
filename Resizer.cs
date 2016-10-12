using System;
using System.Collections.Generic;
using System.IO;
using ImageProcessorCore;

namespace ImageResizer
{
    public static class Resizer
    {
        public static void Resize(string filePath, IEnumerable<int> widths)
        {
            foreach (var width in widths)
            {
                using (FileStream stream = File.OpenRead(filePath))
                {
                    Image image = new Image(stream);
                    var aspectRatio = new Size(image.Width, image.Height).AspectRatio();
                    var size = new Size(width, (int)(width / aspectRatio));
                    Resize(filePath, image, size);
                }
            }
        }

        private static void Resize(string filePath, Image image, Size newSize)
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
            
            using (FileStream output = File.OpenWrite(newPath))
            {                
                image.Resize(newSize.Width, newSize.Height).Save(output);
            }
        }
    }
}