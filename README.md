# SrcSet

Tools to create sets of responsive images for the web

[![SrcSet](https://img.shields.io/nuget/v/SrcSet?logo=nuget&label=SrcSet)](https://www.nuget.org/packages/SrcSet/)
[![SrcSet.Statiq](https://img.shields.io/nuget/v/SrcSet.Statiq?logo=nuget&label=SrcSet.Statiq)](https://www.nuget.org/packages/SrcSet.Statiq/)
[![SrcSet.Core](https://img.shields.io/nuget/v/SrcSet.Core?logo=nuget&label=SrcSet.Core)](https://www.nuget.org/packages/SrcSet.Core/)

[![Build Status](https://github.com/ecoAPM/SrcSet/workflows/CI/badge.svg)](https://github.com/ecoAPM/SrcSet/actions)
[![Coverage](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SrcSet&metric=coverage)](https://sonarcloud.io/dashboard?id=ecoAPM_SrcSet)

[![Maintainability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SrcSet&metric=sqale_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SrcSet)
[![Reliability](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SrcSet&metric=reliability_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SrcSet)
[![Security](https://sonarcloud.io/api/project_badges/measure?project=ecoAPM_SrcSet&metric=security_rating)](https://sonarcloud.io/dashboard?id=ecoAPM_SrcSet)

## Tools

This repository contains 3 projects:
- [SrcSet](#cli): a CLI utility to create sets of responsive images
- [SrcSet.Statiq](#statiq): pipeline and helper for integrating responsive images into a Statiq site
- [SrcSet.Core](#library): a library used by the above, also available for public consumption

## CLI

### Requirements

- .NET 8 SDK

### Installation

```bash
dotnet tool install -g SrcSet
```

### Usage

```bash
srcset {file or directory} [-r] [space delimited set of widths]
```

e.g.

```bash
srcset IMG_9687.jpg 500 1000
```

will resize the image to 500 and 1000 pixels wide, with the filenames `IMG_9687-0500.jpg` and `IMG_9687-1000.jpg`

```bash
srcset all_images/
```

will resize all images in the `all_images` directory (recursively if `-r` is included) to each of the default widths

## Statiq

This package contains a Statiq pipeline and HTML helper method to easily integrate responsive image generation into a Statiq site.

The process to accomplish this has two steps:
1. create the set of responsive images to use (using the pipeline)
2. reference the images in the generated HTML (using the HTML helper)

### Step 1

To create a set of responsive images, place the originals (to be resized) alongside your other assets, and then in your bootstrapper code, add:

```c#
bootstrapper.AddPipeline("SrcSet", new ResponsiveImages("**/*.jpg"));
```

where the optional parameter `**/*.jpg` is a glob pointing to the image assets to resize.

A custom set of widths can also be passed as a second parameter:

```c#
bootstrapper.AddPipeline("SrcSet", new ResponsiveImages("**/*.jpg", new ushort[] { 100, 200, 300 }));
```

### Step 2

In your Razor file, call the HTML helper to create an `<img/>` tag with the relevant attributes set:

```c#
@Html.Raw(ResponsiveImages.SrcSet("images/original.jpg"))
```

You can also customize the widths, default width, and add any other attributes here:

```c#
@Html.Raw(ResponsiveImages.SrcSet("images/original.jpg", new ushort[] { 100, 200, 300 }, 200))
```

```c#
@Html.Raw(ResponsiveImages.SrcSet("images/original.jpg", attributes: new Dictionary<string, string>
	{
		{ "class", "full-width" },
		{ "alt", "don't forget about accessibility!" }
	}
))
```

## Library

The "Core" library can be used to incorporate SrcSet's functionality into your own app.

First, create a new `SrcSetManager`:

```c#
var manager = new SrcSetManager();
```

Then invoke it's `SaveSrcSet()` method:

```c#
await manager.SaveSrcSet("file.png", SrcSetManager.DefaultSizes);
```

If you need more control than the default constructor and sizes provide, you can pass in a specific logging mechanism (other than the default `Console.WriteLine`) and list of widths:

```c#
var manager = new SrcSetManager(Image.LoadAsync, (string s) => logger.LogDebug(s));
await manager.SaveSrcSet("file.png", new ushort[] { 100, 200, 300 });
```

### Default widths

- 240px
- 320px
- 480px
- 640px
- 800px
- 960px
- 1280px
- 1600px
- 1920px
- 2400px

### File types

`SrcSet` uses [ImageSharp](https://imagesharp.net) under the hood, and therefore should theoretically support all file types that ImageSharp supports by entering the filename as a parameter, however when entering a directory as a parameter, file types are limited to:

- jpg / jpeg / jfif
- png
- bmp / bm / dip
- gif
- tga / vda / icb / vst

## Contributing

Please be sure to read and follow ecoAPM's [Contribution Guidelines](CONTRIBUTING.md) when submitting issues or pull requests.