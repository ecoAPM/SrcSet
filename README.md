# ImageResizer

CLI for creating sets of responsive images for the web

## Usage

```
Usage:  resize [options]

Options:
  -f                Image file name
  -d                Image files directory
  -s                Result image size
  -r                Recursively resize images in a directory
  -? | -h | --help  Show help information
```

e.g.

    resize -f IMG_9687.jpg -s 500 -s 1000

will resize the image to 500 and 1000 pixels wide, with the filenames `IMG_9687-0500.jpg` and `IMG_9687-1000.jpg`

    resize -d all_images/

will resize all images in the `all_images` directory (recursively if `-r` is included) to each of the default widths

using `dotnet CLI command` to resize image

    dotnet run resize -- -r -d all_images/ -s 240
    
using visual studio `Application Arguments` to run program

    resize -f "D:\path\to\file\ImageResizer\src\cat.jpg" -s 240

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

Currently supported file types are:

- jpg/jpeg
- png
- bmp
- gif _(not animated)_


## Contributing

To build in VS2017, make sure you have:
- .NET Core 1.1 SDK
- .NET Core Tools Preview 2 for VS

See here for downloads:  
https://www.microsoft.com/net/core#windowsvs2017
