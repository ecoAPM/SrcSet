using Spectre.Console;
using Spectre.Console.Cli;

namespace SrcSet;

public class Command : AsyncCommand<Settings>
{
	private readonly IAnsiConsole _console;

	public Command(IAnsiConsole console)
			=> _console = console;

	public override async Task<int> ExecuteAsync(CommandContext context, Settings settings)
		=> await _console.Status().StartAsync("Running...", _ => Run(settings));
	
	private async Task<int> Run(Settings settings)
		=> await Factory.App(_console).Run(settings);
}