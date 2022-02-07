using SixLabors.ImageSharp;
using SrcSet.Core;

namespace SrcSet;

public class App
{
	private readonly Action<string> _log;

	public App(Action<string> log)
	{
		_log = log;
	}

    public async Task<int> Run(Settings settings)
    {
        if (!File.Exists(settings.Path) && !Directory.Exists(settings.Path))
		{
			_log($"Could not find file or directory named \"{settings.Path}\"");
			return 1;
		}

		var resizeDirectory = settings.Path.IsDirectory();
		if (settings.Recursive && !resizeDirectory)
		{
			_log($"\"{Arguments.RecursiveFlag}\" can only be used with a directory");
			return 1;
		}

		var manager = new SrcSetManager(s => Image.LoadAsync(s), _log);
		var resizeTasks = settings.Path
			.GetFiles(settings.Recursive, resizeDirectory)
			.Select(file => manager.SaveSrcSet(file, settings.Sizes));
		await Task.WhenAll(resizeTasks);
		return 0;
	}
}