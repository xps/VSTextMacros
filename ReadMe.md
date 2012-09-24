What is it?
===============================

An extension for Visual Studio 2012 that brings support for macros in text/code editors.

You can use it to automate repetitive text editing tasks.
It is inspired by the macro feature of [Notepad++][1], so if you have used it then you already know how to use this extension.

 [1]: http://notepad-plus-plus.org

How does it work?
===============================

After installing this extension, you will find a menu called `Text Macros` in the `Tools` menu.
It will only be visible when there is an active text or code editor.
It has 2 submenus, one that toggles the recording of a macro (Start/stop recording macro) and one that replays the last recorded macro (Playback macro).

So let's say you wanted to add a comma after each word on a line:

    Lorem ipsum dolor sit amet consectetur adipiscing elit

 1. Place you cursor at the begining of the first word
 2. Start recording a macro (`Ctrl+Shift+M`)
 3. Press `Ctrl+Right` to go to the end of the word
 4. Type in the comma
 5. Press `Ctrl+Right` to go to the begining of the next word
 6. Stop recording the macro (`Ctrl+Shift+M`)

You should now have this:

    Lorem, ipsum dolor sit amet consectetur adipiscing elit

If you now replay the macro (`Ctrl+Shift+P`), a comma will be added after the second word. Press `Ctrl+Shift+P` a few more times and you will get a comma after each word:

    Lorem, ipsum, dolor, sit, amet, consectetur, adipiscing, elit

This is a simple example, but you are not limited to single-line edits.

You can change the shortcuts associated with the macro menus in Tools > Options > Environment > Keyboard.  
Look for the following commands:

 - Tools.ToggleMacroRecording
 - Tools.PlaybackMacro

What's recorded?
===============================

Only text editing features are recorded, so you cannot use this extension to automate work in, say, a Windows Forms editor.

Currently, here are the commands that are recorded:

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