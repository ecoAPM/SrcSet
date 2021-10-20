using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SrcSet
{
	public static class Arguments
	{
		public static readonly ushort[] DefaultSizes = { 240, 320, 480, 640, 800, 960, 1280, 1600, 1920, 2400 };
		public static readonly string[] ValidExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };
		public const string RecursiveFlag = "-r";

		public static IEnumerable<ushort> GetSizes(this IReadOnlyCollection<string> args)
		{
			var numberOfFilesystemArguments = args.Contains(RecursiveFlag) ? 2 : 1;
			return args.Count == numberOfFilesystemArguments
				? DefaultSizes
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
				? Directory.EnumerateFiles(fileOrDirectoryArg, "*.*", searchOption).Where(f => ValidExtensions.Contains(Path.GetExtension(f).ToLower()))
				: new[] { fileOrDirectoryArg };
		}
	}
}