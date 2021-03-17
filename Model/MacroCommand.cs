using System;

namespace VSTextMacros.Model
{
    // A macro command (such as a keypress)
    public class MacroCommand
    {
        public Guid CommandGroup { get; set; }
        public uint CommandID { get; set; }
        public uint CommandOptions { get; set; }
        public char? Character { get; set; }
    }

    public class MacroCustomCommand
    {
        public Guid Group;
        public uint ID;
        public String Cmd;
    };
}
