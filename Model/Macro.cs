using System;
using System.Collections.Generic;
using System.IO;
using VSTextMacros.Utilities;

namespace VSTextMacros.Model
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

        // Saves the current macro to a file
        public static void SaveToFile(string filename)
        {
            if (CurrentMacro != null)
                File.WriteAllText(filename, XmlHelpers.Serialize(CurrentMacro.Commands));
        }

        // Loads the current macro from a file
        public static void LoadFromFile(string filename)
        {
            CurrentMacro = new Macro
            {
                Commands = XmlHelpers.Deserialize<List<MacroCommand>>(File.ReadAllText(filename))
            };
        }
    }
}
