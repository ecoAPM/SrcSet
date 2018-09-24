using System.Drawing;
using System.IO;

namespace SrcSet
{
    public static class FileHelpers
    {
        public static string GetFilename(string filePath, Size newSize)
        {
            var file = Path.GetFileNameWithoutExtension(filePath);
            var extension = Path.GetExtension(filePath);
            var newFileName = $"{file}-{newSize.Width:D4}{extension}";
            return newFileName;
        }
    }
}