using SixLabors.ImageSharp;
using Spectre.Console;
using Spectre.Console.Cli;
using SrcSet.Core;

namespace SrcSet;

public class Command : AsyncCommand<Settings>
{
	private readonly IAnsiConsole _console;

	public Command(IAnsiConsole console)
			=> _console = console;

	public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
	{
		if (!File.Exists(settings.Path) && !Directory.Exists(settings.Path))
		{
			_console.WriteLine("Could not find file or directory named \"{0}\"", settings.Path);
			return 1;
		}

		var resizeDirectory = settings.Path.IsDirectory();
		if (settings.Recursive && !resizeDirectory)
		{
			_console.WriteLine("\"" + Arguments.RecursiveFlag + "\" can only be used with a directory");
			return 1;
		}

		var manager = new SrcSetManager(Image.LoadAsync, _console.WriteLine);
		var resizeTasks = settings.Path
			.GetFiles(settings.Recursive, resizeDirectory)
			.Select(file => manager.SaveSrcSet(file, settings.Sizes));
		await Task.WhenAll(resizeTasks);
		return 0;
	}
}