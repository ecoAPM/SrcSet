using System.Drawing;
using System.IO;

namespace SrcSet.Core
{
	public static class FileHelpers
	{
		public static string GetFilename(string filePath, Size newSize)
		{
			var file = Path.GetFileNameWithoutExtension(filePath);
			var extension = Path.GetExtension(filePath);
			return $"{file}-{newSize.Width:D4}{extension}";
		}
	}
}