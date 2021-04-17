**THE CURRENT VERSION WORKS WITH VISUAL STUDIO 2019**

For Visual Studio 2012 to 2017, please download and install this version:
https://github.com/xps/VSTextMacros/releases/download/1.12/VSTextMacros-1.12.vsix


What is it?
===============================

An extension for Visual Studio 2012-2017 that brings back support for macros in text/code editors. These macros can be used to automate repetitive text editing tasks.

You may have used such macros in previous versions of Visual Studio. Indeed, macros used to be supported and [removed][5] in Visual Studio 2010.

Although macros used to be very sophisticated at the time, letting you control almost any feature of Visual Studio, this extension only aims at supporting the text editing part of macros.

And if you have not used Visual Studio macros before, you may have used them in other text editors such as [Notepad++][1] or [Sublime Text][6]. If you have, then you already know how to use this extension.


Download
===============================

You can download the VSIX installer from the [Visual Studio Marketplace][4].


How does it work?
===============================

After installing this extension, you will find a menu called `Text Macros` in the `Tools` menu.
It will only be visible when there is an active text or code editor.
It has 3 submenus: one that toggles the recording of a macro (Start/stop recording macro), one that replays the last recorded macro (Playback macro), and another that can repeat the macro any given number of times.

 ![Screenshot][2]

So the idea is that you start recording, make some edits, stop recording, and then replay the edits.  
For example, here's what you would do if you just copied a few signatures from an interface that you have to implement:

 ![Demo][3]

You can change the shortcuts associated with the macro menus in Tools > Options > Environment > Keyboard.  
Look for the following commands:

 - Tools.RecordMacro
 - Tools.PlaybackMacro
 - Tools.PlaybackMacroMultipleTimes
 - Tools.SaveMacro
 - Tools.OpenSavedMacros
 - Tools.RunSavedMacro1
 - Tools.RunSavedMacro2
 - ...


What can be recorded?
===============================

Only text editing features can be recorded, so you cannot use this extension to automate work in, say, a Windows Forms editor.

Currently, here are the commands that can be recorded:

 - Cursor movements
 - Characters that you type
 - Tabs, new lines, deletions
 - Find operations (Find Next...)
 - Some text editing commands from the Edit menu (to lowercase, to uppercase...)
 
**Known limitations:**

 - Recording copy/paste operations does not work well when the Productivity Power Tools extension is installed (more details [here][12])
 
 
Change Log
===============================

**1.16 - Apr 17, 2021**
 - Support for loading extra recordable commands from an external file (thanks to [gregtbrown][14])
 - Added "Duplicate" to recordable commands
 - Added a menu item to open the macros directory in Explorer
	
**1.15 - Aug 22, 2020**
 - Support for Find operations (thanks to [viniciusvillas][13])
 - Added macro names in playback menu
 - Automatically stop recording on playback

**1.14 - Nov 10, 2019**
 - Fixed reordering macros

**1.13 - Sep 30, 2019**
 - Support for Visual Studio 2019 (thanks to [viniciusvillas][13])

**1.12 - Dec 10, 2018**
 - New recorded commands: "Go to brace" and outlining commands

**1.11 - Aug 15, 2018**
 - Added shortcuts to run saved macros
 
**1.10 - Apr 23, 2017**
 - Support for Visual Studio 2017
 - Playback macros in a single undo transaction (thanks to [Yves Goergen][11])

**1.9 - May 17, 2016**

 - Fixed saving macros (thanks to [Hung Dinh][9])

**1.8 - May 5, 2016**

 - Added option to save macros

**1.7 - Aug 8, 2015**

 - Fixed support for VS 2012/2013

**1.6 - Jul 21, 2015**

 - Support for Visual Studio 2015 (thanks to [Ian Prest][8])

**1.5 - Jul 8, 2014**

 - Added "Edit.LineFirstColumn" as a recordable command (thanks to [Jose Jiminiz][7]).

**1.4 - Dec 7, 2013**

 - Added menu to repeat macro a given number of times.

**1.3 - Nov 28, 2013**

 - Fixed macros not working in certain file types (HTML...)
 - Recording additional edit commands: Format Document/Selection, Comment/Uncomment Selection,
   Delete Horizontal White Space, Increase/Decrease Line Indent, Organize Usings, Move Selected Lines Up/Down (VS 2013).

**1.2 - Oct 24, 2013**

 - Support for Visual Studio 2013
 - Added 'Ctrl+Shift+R' shortcut to match shortcut of previous Visual Studio versions

**1.1 - Feb 8, 2013**

 - Added visual cue while recording
 - Current macro now persists across sessions

**1.0 - Nov 13, 2012**

 - Initial version


Want to contribute?
===============================

 - Let me know of any issues you may find (include a portion of text and keystroke sequence if it is a problem with a macro)
 - Fork the code and contribute bug fixes or new features


License
===============================

Text Macros for Visual Studio
Copyright (C) 2012-2020 Xavier Poinas

This program is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, either version 3 of the License, or
(at your option) any later version.

This program is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with this program. If not, see <http://www.gnu.org/licenses/>.

 [1]: http://notepad-plus-plus.org
 [2]: https://raw.github.com/xps/VSTextMacros/master/Documentation/screenshot.png
 [3]: https://github.com/xps/VSTextMacros/raw/master/Documentation/example.gif
 [4]: https://marketplace.visualstudio.com/items?itemName=XavierPoinas.TextMacrosforVisualStudio201220132015
 [5]: http://social.msdn.microsoft.com/Forums/en-US/vsx/thread/d8410838-085b-4647-8c42-e31b669c9f11
 [6]: http://sublimetext.info/docs/en/extensibility/macros.html
 [7]: https://github.com/JoseJimeniz
 [8]: https://github.com/ijprest
 [9]: https://github.com/nhdinh
 [10]: https://github.com/xps/VSTextMacros/issues/1 
 [11]: https://github.com/ygoe
 [12]: https://github.com/xps/VSTextMacros/issues/14
 [13]: https://github.com/viniciusvillas
 [14]: https://github.com/gregtbrown