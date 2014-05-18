# Sierpinski Triangle

A simple C# program to generate Sierpinski triangle.

You can download binary file [here][binary].

## How to Use

Click "Update" to refresh the graphics.

### Navigation

Move - Middle-click
Zoom - Mouse wheel or press left-click then drag
Save image - Right-click

### Options

All options are in the "Control" panel.
You can click Menu: Options -> Reset to Default Settings if you mess them up.

## Examples

![screenshot1][screenshot1]

Modified parameters:

- Generator/NumberOfLevels = 16
- Style/IsShowZeroLines = False
- Style/IsUseAntiAlias = True

***

![screenshot2][screenshot2]

Modified parameters:

- Generator/Mode = Bypass
- Generator/NumberOfLevels = 5
- Style/Visible/IsShowNumber = True

***

![screenshot3][screenshot3]

Modified parameters:

- Generator/Expression = "0 = n() % 666"
- Generator/Mode = Expression
- Generator/NumberOfLevels = 129
- Pattern/PatternMode = RightAngledToLeft
- Style/BackColor = Gray
- Style/Visible/BorderColor = GreenYellow

***

![screenshot4][screenshot4]

Modified parameters:

- Generator/NumberOfLevels = 4
- Style/Hidden/IsShow = True
- Style/Hidden/IsShowNumber = True
- Style/Visible/IsShowNumber = True

***

![screenshot5][screenshot5]

Modified parameters:

- Generator/DefaultVisibility = False
- Generator/ModuloBy = 4
- Generator/NumberOfLevels = 64
- Generator/RemainderToHide = -1 (-1 means ignoring this rule)
- Generator/RemainderToShow = 2

## Building

Visual Studio 2010 or higher

## License

Copyright (c) 2014 Shawn

See the LICENSE file for license rights and limitations (MIT).

[binary]: http://github.com/ebola777/Sierpinski-Triangle/raw/master/binaries/SierpinskiTriangle.zip
[screenshot1]: http://raw.githubusercontent.com/ebola777/Sierpinski-Triangle/master/docs/screenshot1.jpg
[screenshot2]: http://raw.githubusercontent.com/ebola777/Sierpinski-Triangle/master/docs/screenshot2.jpg
[screenshot3]: http://raw.githubusercontent.com/ebola777/Sierpinski-Triangle/master/docs/screenshot3.jpg
[screenshot4]: http://raw.githubusercontent.com/ebola777/Sierpinski-Triangle/master/docs/screenshot4.jpg
[screenshot5]: http://raw.githubusercontent.com/ebola777/Sierpinski-Triangle/master/docs/screenshot5.jpg
