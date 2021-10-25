using System.Linq;
using SixLabors.ImageSharp;
using SrcSet.Core;
using Statiq.Common;

namespace SrcSet.Statiq
{
	public static class NormalizedPathExtensions
	{
		public static bool IsImage(this NormalizedPath path)
			=> SrcSetManager.ValidExtensions.Contains(path.Extension);

		public static string GetDestination(this NormalizedPath source, Size size)
		{
			var input = source.GetRelativeInputPath();
			var filename = FileHelpers.GetFilename(source.FileName.ToString(), size);
			return input.ChangeFileName(filename).ToString();
		}
	}
}