using System;
using System.Collections.Generic;
using System.IO;
using ImageSharp;

namespace ImageResizer
{
    public static class Resizer
    {
        // The following declaration opens a image file at filepath t. And calls the private method
        // "Resize" to save the file.
        public static void Resize(string filePath, IEnumerable<int> widths)
        {
            using (var stream = File.OpenRead(filePath))
            {
                var image = Image.Load(stream);
                var aspectRatio = new Size(image.Width, image.Height).AspectRatio();
                foreach (var width in widths)
                {
                    var size = new Size(width, (int)(width / aspectRatio));
                    Resize(filePath, image , size);
                }
            }
        }
        //The following declaration takes the image file from memory ,sets the image dimensions specified
        // ,appends demensions to the file name resizes image to specified height and width and saves file in current working dir
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

            var resized = image.Resize(newSize.Width, newSize.Height);

            using (var output = File.OpenWrite(newPath))
            {
                resized.MetaData.Quality = 255;
                resized.Save(output);
            }
        }
    }
}