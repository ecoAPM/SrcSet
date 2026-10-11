using Spectre.Console;
using Spectre.Console.Cli;

namespace SrcSet;

public class Command(IAnsiConsole console) : AsyncCommand<Settings>
{
	public async Task<int> ExecuteAsync(CommandContext context, Settings settings)
		=> await ExecuteAsync(context, settings, CancellationToken.None);

	public override async Task<int> ExecuteAsync(CommandContext context, Settings settings, CancellationToken cancellationToken)
		=> await console.Status().StartAsync("Running...", _ => Run(settings));

	private async Task<int> Run(Settings settings)
		=> await Factory.App(console).Run(settings);
}