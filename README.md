# .TTF to Lua
This is a small (and rushed) program used to convert .ttf files to a code-readable lua(u) table.
This project was primarily made to support my custom ROBLOX text rendering system, please excuse if it's missing any important data.

## Installation
Either grab the .exe from the [releases](https://github.com/unby6/ttf-to-lua/releases/latest) section or build the executable yourself.

## Build Preparation
This project requires [LayoutFarm's Typography library](https://github.com/LayoutFarm/Typography) to properly function.
Download it and put its "Typography.OpenFont" Folder into your project directory.
Enable the "unsafe" keyword in your project settings if necessary.  
Now you're free to build

## Usage
Simply drag-and-drop your desired .ttf file on top of the executable,
a new .lua file containing the font data will then be created under the same directory your .ttf file originates from.

## Data Read Example
For an example on what each part of the glyph data array means and how to read from it, check out ReadExample.luau
