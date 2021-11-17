namespace SrcSet.Core;

public static class FileHelpers
{
	public static string GetFilename(string filePath, ushort width)
	{
		var file = Path.GetFileNameWithoutExtension(filePath);
		var extension = Path.GetExtension(filePath);
		return $"{file}-{width:D4}{extension}";
	}
}