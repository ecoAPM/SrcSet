using NSubstitute;
using Spectre.Console;
using Spectre.Console.Cli;
using Xunit;

namespace SrcSet.Tests;

public sealed class CommandTests
{
	[Fact]
	public async Task CanRunCommand()
	{
		//arrange
		var console = Substitute.For<IAnsiConsole>();
		var command = new Command(console);

		var remaining = Substitute.For<IRemainingArguments>();
		var context = new CommandContext([], remaining, string.Empty, null);
		var settings = new Settings();

		//act
		var result = await command.ExecuteAsync(context, settings);

		//assert
		Assert.Equal(0, result);
	}
}