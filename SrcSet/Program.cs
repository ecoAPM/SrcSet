using Spectre.Console.Cli;

namespace SrcSet;

public static class Program
{
    public static async Task<int> Main(string[] args)
        => await new CommandApp<Command>().RunAsync(args);
}