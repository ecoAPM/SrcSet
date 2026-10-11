using SkiaSharp;
using SrcSet.Core;

namespace SrcSet;

public class App(Action<string> log)
{
	public async Task<int> Run(Settings settings)
    {
        if (!File.Exists(settings.Path) && !Directory.Exists(settings.Path))
		{
			log($"Could not find file or directory named \"{settings.Path}\"");
			return 1;
		}

		var resizeDirectory = settings.Path.IsDirectory();
		if (settings.Recursive && !resizeDirectory)
		{
			log($"\"{Arguments.RecursiveFlag}\" can only be used with a directory");
			return 1;
		}

		var manager = new SrcSetManager(s => SKImage.FromEncodedData(s), log);
		var resizeTasks = settings.Path
			.GetFiles(settings.Recursive, resizeDirectory)
			.Select(file => manager.SaveSrcSet(file, settings.Sizes));
		await Task.WhenAll(resizeTasks);
		return 0;
	}
}