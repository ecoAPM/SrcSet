using SrcSet.Core;

namespace SrcSet;

public static class Arguments
{
	public const string RecursiveFlag = "-r";

	public static IEnumerable<ushort> GetSizes(this IReadOnlyCollection<string> args)
	{
		var numberOfFilesystemArguments = args.Contains(RecursiveFlag) ? 2 : 1;
		return args.Count == numberOfFilesystemArguments
			? SrcSetManager.DefaultSizes
			: args.Skip(numberOfFilesystemArguments).Select(a => Convert.ToUInt16(a));
	}

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