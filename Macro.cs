using System;
using System.Collections.Generic;

namespace VSTextMacros
{
    // A macro, i.e. a list of macro commands
    public class Macro
    {
        // The list of commands
        public List<MacroCommand> Commands { get; private set; }

        // Is this macro being recorded?
        public bool IsRecording { get; private set; }

        // At this point we only deal with 1 macro at a time
        public static Macro CurrentMacro { get; private set; }
        
        // Constructor
        private Macro()
        {
            Commands = new List<MacroCommand>();
        }

        // Starts a new macro in record mode
        public static void StartNew()
        {
            CurrentMacro = new Macro
            { 
                IsRecording = true
            };
        }

        // Stops recording the macro
        public void StopRecording()
        {
            IsRecording = false;
        }

        // Adds a command to the list of recorded commands
        public void AddCommand(Guid commandGroup, uint commandID, uint commandOptions, char? character = null)
        {
            var cmd = new MacroCommand
            {
                CommandGroup = commandGroup,
                CommandID = commandID,
                CommandOptions = commandOptions,
                Character = character
            };

            Commands.Add(cmd);
        }

        // Adds a command to the list of recorded commands
        public void AddCommand(MacroCommand command)
        {
            Commands.Add(command);
        }
    }

    // A macro command (such as a keypress)
    public class MacroCommand
    {
        public Guid CommandGroup { get; set; }
        public uint CommandID { get; set; }
        public uint CommandOptions { get; set; }
        public char? Character { get; set; }
    }
}
