using Spectre.Console;

namespace SrcSet;

public static class Factory
{
    public static App App(IAnsiConsole console)
        => new App(console.WriteLine);
}