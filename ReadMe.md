What is it?
===============================

An extension for Visual Studio 2012 that brings support for macros in text/code editors.

You can use it to automate repetitive text editing tasks.
It is inspired by the macro feature of [Notepad++][1], so if you have used it then you already know how to use this extension.

Download
===============================

You can download the VSIX installer from the [Visual Studio Gallery][4] or [here on GitHub][5].

How does it work?
===============================

After installing this extension, you will find a menu called `Text Macros` in the `Tools` menu.
It will only be visible when there is an active text or code editor.
It has 2 submenus, one that toggles the recording of a macro (Start/stop recording macro) and one that replays the last recorded macro (Playback macro).

 ![Screenshot][2]

So the idea is that you start recording, make some edits, stop recording, and then replay the edits.  
For example, here's what you would do if you just copied a few signatures from an interface that you have to implement:

 ![Demo][3]

You can change the shortcuts associated with the macro menus in Tools > Options > Environment > Keyboard.  
Look for the following commands:

 - Tools.ToggleMacroRecording
 - Tools.PlaybackMacro

What can be recorded?
===============================

Only text editing features can be recorded, so you cannot use this extension to automate work in, say, a Windows Forms editor.

Currently, here are the commands that can be recorded:

 - Cursor movements
 - Characters that you type
 - Tabs, new lines, deletions
 - Some text editing commands from the Edit menu (to lowercase, to uppercase...)

Want to contribute?
===============================

 - Let me know of any issues you may find (include a portion of text and keystroke sequence if it is a problem with a macro)
 - Fork the code and contribute bug fixes or new features (ability to save a macro, for instance)

License
===============================

Text Macros for Visual Studio 2012  
Copyright (C) 2012 Xavier Poinas

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
 [4]: http://visualstudiogallery.msdn.microsoft.com/8e2103b6-87cf-4fef-9410-a580c434b602
 [5]: https://github.com/xps/VSTextMacros/downloads
