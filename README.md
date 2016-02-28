# ImageResizer

CLI for creating sets of responsive images for the web

## Usage

    resize {file or directory} [-r] [space delimited set of widths]

e.g.

    resize IMG_9687.jpg 500 1000

will resize the image to 500 and 1000 pixels wide, with the filenames `IMG_9687-0500.jpg` and `IMG_9687-1000.jpg`

    resize all_images/

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

Currently supported file types are:

- jpg
- png
- bmp
- gif _(not animated)_
- tif/tiff
