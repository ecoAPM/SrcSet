using SrcSet.Core;
using Statiq.Common;

namespace SrcSet.Statiq;

public static class NormalizedPathExtensions
{
	public static bool IsImage(this NormalizedPath path)
		=> SrcSetManager.ValidExtensions.Contains(path.Extension);

	public static NormalizedPath GetDestination(this NormalizedPath source, ushort width)
	{
		var input = source.GetRelativeInputPath();
		var filename = FileHelpers.GetFilename(source.FileName.ToString(), width);
		return input.ChangeFileName(filename);
	}
}