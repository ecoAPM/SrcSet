using SrcSet.Core;

namespace SrcSet.Statiq;

public static class NormalizedPathExtensions
{
	extension(NormalizedPath path)
	{
		public bool IsImage()
			=> SrcSetManager.ValidExtensions.Contains(path.Extension);

		public NormalizedPath GetDestination(ushort width)
		{
			var input = path.GetRelativeInputPath();
			var filename = FileHelpers.GetFilename(path.FileName.ToString(), width);
			return input.ChangeFileName(filename);
		}
	}
}