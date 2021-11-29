using SrcSet.Core;

namespace SrcSet;

public static class Arguments
{
	public const string RecursiveFlag = "-r";

	public static bool IsDirectory(this string fileOrDirectoryArg)
		=> File.GetAttributes(fileOrDirectoryArg)
			.HasFlag(FileAttributes.Directory);

	public static IEnumerable<string> GetFiles(this string fileOrDirectoryArg, bool resizeRecursively, bool resizeDirectory)
	{
		var searchOption = resizeRecursively
			? SearchOption.AllDirectories
			: SearchOption.TopDirectoryOnly;
		return resizeDirectory
			? Directory.EnumerateFiles(fileOrDirectoryArg, "*.*", searchOption).Where(f => SrcSetManager.ValidExtensions.Contains(Path.GetExtension(f).ToLower()))
			: new[] { fileOrDirectoryArg };
	}
}