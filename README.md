# dotnet-srcset

[![NuGet version](https://badge.fury.io/nu/srcset.svg)](https://www.nuget.org/packages/srcset/)
[![Build Status](https://github.com/ecoAPM/dotnet-srcset/workflows/CI/badge.svg)](https://github.com/ecoAPM/dotnet-srcset/actions)

CLI for creating sets of responsive images for the web

## Installation

    dotnet tool install -g srcset

## Usage

    srcset {file or directory} [-r] [space delimited set of widths]

e.g.

    srcset IMG_9687.jpg 500 1000

will resize the image to 500 and 1000 pixels wide, with the filenames `IMG_9687-0500.jpg` and `IMG_9687-1000.jpg`

    srcset all_images/

will resize all images in the `all_images` directory (recursively if `-r` is included) to each of the default widths

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

`dotnet-srcset` uses [ImageSharp](https://imagesharp.net) under the hood, and therefore should theoretically support all file types that ImageSharp supports by entering the filename as a parameter, however when entering a directory as a parameter, file types are limited to:

- jpg/jpeg
- png
- bmp
- gif
- tif/tiff

Feel free to contribute an update that adds more file types to `Arguments.ValidExtensions`!
