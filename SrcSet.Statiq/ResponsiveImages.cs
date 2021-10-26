using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SrcSet.Core;
using Statiq.Common;
using Statiq.Core;
using Statiq.Web;
using Statiq.Web.Modules;
using Statiq.Web.Pipelines;

namespace SrcSet.Statiq
{
	public class ResponsiveImages : Pipeline
	{
		public ResponsiveImages(string fileGlob = null, IEnumerable<ushort> widths = null, Func<Stream, Task<Image>> loadImage = null)
		{
			Dependencies.Add(nameof(Inputs));

			ProcessModules = new ModuleList
			{
				new GetPipelineDocuments(ContentType.Asset),
				new FilterSources(Config.FromValue(fileGlob)),
				new FilterDocuments(Config.FromDocument(doc => doc.Source.IsImage())),
				new CreateResponsiveImages(loadImage ?? Image.LoadAsync, widths ?? SrcSetManager.DefaultSizes)
			};

			OutputModules = new ModuleList { new WriteFiles() };
		}

		public static string SrcSet(string baseImage, IReadOnlyList<ushort> widths = null, ushort? defaultWidth = null, IDictionary<string, string> attributes = null)
		{
			widths ??= SrcSetManager.DefaultSizes.ToArray();
			defaultWidth ??= widths[widths.Count / 3];

			var defaultFilename = new NormalizedPath(baseImage).GetDestination(defaultWidth.Value);
			var srcset = widths.Select(w => SrcSetItem(baseImage, w));
			var attributeStrings = attributes?.Select(a => $@"{a.Key}=""{a.Value}""") ?? ArraySegment<string>.Empty;

			return $@"<img src=""/{defaultFilename}"" srcset=""{string.Join(", ", srcset)}"" {string.Join(" ", attributeStrings)} />".Replace("  />", " />");
		}

		private static string SrcSetItem(string baseImage, ushort width)
		{
			var path = new NormalizedPath(baseImage);
			var destination = path.GetDestination(width);
			return $"/{destination} {width}w";
		}
	}
}