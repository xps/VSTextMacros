using System;

namespace VSTextMacros.Model
{
    // A command that can be defined in an XML file
    public class MacroCustomCommand
    {
        public Guid Group { get; set; }
        public uint ID { get; set; }
        public string Cmd { get; set; }
    }
}
