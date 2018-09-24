using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace SrcSet
{
    public class SrcSetManager
    {
        private readonly Func<string, Image<Rgba32>> _loadImage;

        public SrcSetManager(Func<string, Image<Rgba32>> loadImage) => _loadImage = loadImage;

        public void SaveSrcSet(string filePath, IEnumerable<ushort> widths)
        {
            using (var image = _loadImage(filePath))
            {
                var size = new Size(image.Width, image.Height);
                foreach (var newSize in widths.Select(width => size.Resize(width)))
                {
                    var newFile = image.SaveResizedImage(filePath, newSize);
                    if(newFile != null)
                        Console.WriteLine(newFile);
                }
            }
        }
    }
}