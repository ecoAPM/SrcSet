using System.ComponentModel;
using Spectre.Console.Cli;
using SrcSet.Core;

namespace SrcSet;

public class Settings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    [Description("the file or directory to resize")]
    public string Path { get; set; } = null!;

    [CommandOption("-r|--recursive")]
    [Description("recurse subdirectories")]
    public bool Recursive { get; set; }

    [CommandArgument(1, "[sizes]")]
    [Description("the set of widths to resize the image(s) to")]
    public ushort[] Sizes { get; set; } = SrcSetManager.DefaultSizes;
}