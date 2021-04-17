using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace VSTextMacros.Model
{
    // List of all recordable commands
    public class RecordableCommands
    {
        public class Command
        {
            public uint Id { get; set; }
            public string Cmd { get; set; }

            public Command(uint id, string cmd = null)
            {
                Id = id;
                Cmd = cmd;
            }
        };

        public class CommandSet : HashSet<Command>
        {
            private class CommandComparer : IEqualityComparer<Command>
            {
                public bool Equals(Command x, Command y)
                {
                    return x.Id == y.Id;
                }

                public int GetHashCode(Command obj)
                {
                    return (int)obj.Id;
                }
            };

            public CommandSet() : base(new CommandComparer())
            {
            }

            public bool Add(uint id, string cmd = null)
            {
                return Add(new Command(id, cmd));
            }

            public bool Contains(uint id)
            {
                return Contains(new Command(id));
            }
            public bool TryGetValue(uint id, out Command cmd)
            {
                return TryGetValue(new Command(id), out cmd);
            }
        };

        public static bool Add(Guid group, uint id, string cmd = null)
        {
            if (!Commands.TryGetValue(group, out CommandSet cmdSet))
            {
                cmdSet = new CommandSet();
                Commands.Add(group, cmdSet);
            }
            return cmdSet.Add(id, cmd);
        }

        public static bool AddFromFile(string fileName)
        {
            if (!File.Exists(fileName))
                return true;

            try
            {
                var cmds = Utilities.XmlHelpers.Deserialize<List<MacroCustomCommand>>(File.ReadAllText(fileName));
                foreach (var cmd in cmds)
                    Add(cmd.Group, cmd.ID, cmd.Cmd);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error loading custom macros:\n" + e.Message, "Text Macros", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        // Key is the command group guid, value is the list of command IDs
        public static Dictionary<Guid, CommandSet> Commands = new Dictionary<Guid, CommandSet>
        {
            {
                // VSConstants.VSStd2K,
                Guid.Parse("1496A755-94DE-11D0-8C3F-00C04FC2AAE2"),
                new CommandSet()
                {
                    new Command((uint)VSConstants.VSStd2KCmdID.TYPECHAR),
                    new Command((uint)VSConstants.VSStd2KCmdID.BACKSPACE),
                    new Command((uint)VSConstants.VSStd2KCmdID.RETURN),
                    new Command((uint)VSConstants.VSStd2KCmdID.ECMD_TAB),
                    new Command((uint)VSConstants.VSStd2KCmdID.TAB),
                    new Command((uint)VSConstants.VSStd2KCmdID.BACKTAB),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETE),
                    new Command((uint)VSConstants.VSStd2KCmdID.LEFT),
                    new Command((uint)VSConstants.VSStd2KCmdID.LEFT_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.RIGHT),
                    new Command((uint)VSConstants.VSStd2KCmdID.RIGHT_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.UP),
                    new Command((uint)VSConstants.VSStd2KCmdID.UP_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.DOWN),
                    new Command((uint)VSConstants.VSStd2KCmdID.DOWN_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.HOME),
                    new Command((uint)VSConstants.VSStd2KCmdID.HOME_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.END),
                    new Command((uint)VSConstants.VSStd2KCmdID.END_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.BOL),
                    new Command((uint)VSConstants.VSStd2KCmdID.BOL_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.FIRSTCHAR),
                    new Command((uint)VSConstants.VSStd2KCmdID.FIRSTCHAR_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.EOL),
                    new Command((uint)VSConstants.VSStd2KCmdID.EOL_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.LASTCHAR),
                    new Command((uint)VSConstants.VSStd2KCmdID.LASTCHAR_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.PAGEUP),
                    new Command((uint)VSConstants.VSStd2KCmdID.PAGEUP_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.PAGEDN),
                    new Command((uint)VSConstants.VSStd2KCmdID.PAGEDN_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.TOPLINE),
                    new Command((uint)VSConstants.VSStd2KCmdID.TOPLINE_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.BOTTOMLINE),
                    new Command((uint)VSConstants.VSStd2KCmdID.BOTTOMLINE_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.SELTABIFY),
                    new Command((uint)VSConstants.VSStd2KCmdID.SELUNTABIFY),
                    new Command((uint)VSConstants.VSStd2KCmdID.SELLOWCASE),
                    new Command((uint)VSConstants.VSStd2KCmdID.SELUPCASE),
                    new Command((uint)VSConstants.VSStd2KCmdID.SELECTCURRENTWORD),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETEWORDRIGHT),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETEWORDLEFT),
                    new Command((uint)VSConstants.VSStd2KCmdID.WORDPREV),
                    new Command((uint)VSConstants.VSStd2KCmdID.WORDPREV_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.WORDNEXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.WORDNEXT_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.FORMATDOCUMENT),
                    new Command((uint)VSConstants.VSStd2KCmdID.FORMATSELECTION),
                    new Command((uint)VSConstants.VSStd2KCmdID.COMMENT_BLOCK),
                    new Command((uint)VSConstants.VSStd2KCmdID.COMMENTBLOCK),
                    new Command((uint)VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK),
                    new Command((uint)VSConstants.VSStd2KCmdID.UNCOMMENTBLOCK),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETEWHITESPACE),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETELINE),
                    new Command((uint)VSConstants.VSStd2KCmdID.DELETEBLANKLINES),
                    new Command((uint)VSConstants.VSStd2KCmdID.INDENT),
                    new Command((uint)VSConstants.VSStd2KCmdID.UNINDENT),
                    new Command((uint)VSConstants.VSStd2KCmdID.EditorLineFirstColumn),
                    new Command((uint)VSConstants.VSStd2KCmdID.EditorLineFirstColumnExtend),
                    new Command((uint)VSConstants.VSStd2KCmdID.GOTOBRACE),
                    new Command((uint)VSConstants.VSStd2KCmdID.GOTOBRACE_EXT),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_HIDE_SELECTION),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_TOGGLE_CURRENT),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_TOGGLE_ALL),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_STOP_HIDING_ALL),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_STOP_HIDING_CURRENT),
                    new Command((uint)VSConstants.VSStd2KCmdID.OUTLN_COLLAPSE_TO_DEF)
                }
            },
            {
                // VSConstants.VSStd97
                Guid.Parse("5EFC7975-14BC-11CF-9B2B-00AA00573819"),
                new CommandSet()
                {
                    new Command((uint)VSConstants.VSStd97CmdID.Copy),
                    new Command((uint)VSConstants.VSStd97CmdID.Cut),
                    new Command((uint)VSConstants.VSStd97CmdID.Delete),
                    new Command((uint)VSConstants.VSStd97CmdID.Paste),
                    new Command((uint)VSConstants.VSStd97CmdID.Redo),
                    new Command((uint)VSConstants.VSStd97CmdID.MultiLevelRedo),
                    new Command((uint)VSConstants.VSStd97CmdID.SelectAll),
                    new Command((uint)VSConstants.VSStd97CmdID.Undo),
                    new Command((uint)VSConstants.VSStd97CmdID.SaveProjectItem),
                    // Support to find features.
                    new Command((uint)VSConstants.VSStd97CmdID.FindNext, "Edit.FindNext"),
                    new Command((uint)VSConstants.VSStd97CmdID.FindPrev, "Edit.FindPrevious"),
                    new Command((uint)VSConstants.VSStd97CmdID.FindSelectedNext, "Edit.FindNextSelected"),
                    new Command((uint)VSConstants.VSStd97CmdID.FindSelectedPrev, "Edit.FindPreviousSelected")
                }
            },
            {
                // VSConstants.VSStd12
                Guid.Parse("2A8866DC-7BDE-4DC8-A360-A60679534384"),
                new CommandSet()
                {
                    new Command((int)VSConstants.VSStd12CmdID.MoveSelLinesUp),
                    new Command((int)VSConstants.VSStd12CmdID.MoveSelLinesDown)
                }
            },
            {
                // Edit menu
                Guid.Parse("160961B3-909D-4B28-9353-A1BEF587B4A6"),
                new CommandSet()
                {
                    new Command(1) // Duplicate
                }
            },
            {
                // Organize usings menu
                Guid.Parse("5D7E7F65-A63F-46EE-84F1-990B2CAB23F9"),
                new CommandSet()
                {
                    new Command(6417), // Remove unused usings
                    new Command(6418), // Sort usings
                    new Command(6419)  // Remove and sort
                }
            }
        };
    }
}
