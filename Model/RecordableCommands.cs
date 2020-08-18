using Microsoft.VisualStudio;
using System;
using System.Collections.Generic;

namespace VSTextMacros.Model
{
    // List of all recordable commands
    public class RecordableCommands
    {
        // Key is the command group guid, value is the list of command IDs
        public static readonly Dictionary<Guid, IEnumerable<uint>> Commands = new Dictionary<Guid, IEnumerable<uint>>
        {
            {
                // VSConstants.VSStd2K,
                Guid.Parse("1496A755-94DE-11D0-8C3F-00C04FC2AAE2"),
                new HashSet<uint>
                {
                    (uint)VSConstants.VSStd2KCmdID.TYPECHAR,
                    (uint)VSConstants.VSStd2KCmdID.BACKSPACE,
                    (uint)VSConstants.VSStd2KCmdID.RETURN,
                    (uint)VSConstants.VSStd2KCmdID.ECMD_TAB,
                    (uint)VSConstants.VSStd2KCmdID.TAB,
                    (uint)VSConstants.VSStd2KCmdID.BACKTAB,
                    (uint)VSConstants.VSStd2KCmdID.DELETE,
                    (uint)VSConstants.VSStd2KCmdID.LEFT,
                    (uint)VSConstants.VSStd2KCmdID.LEFT_EXT,
                    (uint)VSConstants.VSStd2KCmdID.RIGHT,
                    (uint)VSConstants.VSStd2KCmdID.RIGHT_EXT,
                    (uint)VSConstants.VSStd2KCmdID.UP,
                    (uint)VSConstants.VSStd2KCmdID.UP_EXT,
                    (uint)VSConstants.VSStd2KCmdID.DOWN,
                    (uint)VSConstants.VSStd2KCmdID.DOWN_EXT,
                    (uint)VSConstants.VSStd2KCmdID.HOME,
                    (uint)VSConstants.VSStd2KCmdID.HOME_EXT,
                    (uint)VSConstants.VSStd2KCmdID.END,
                    (uint)VSConstants.VSStd2KCmdID.END_EXT,
                    (uint)VSConstants.VSStd2KCmdID.BOL,
                    (uint)VSConstants.VSStd2KCmdID.BOL_EXT,
                    (uint)VSConstants.VSStd2KCmdID.FIRSTCHAR,
                    (uint)VSConstants.VSStd2KCmdID.FIRSTCHAR_EXT,
                    (uint)VSConstants.VSStd2KCmdID.EOL,
                    (uint)VSConstants.VSStd2KCmdID.EOL_EXT,
                    (uint)VSConstants.VSStd2KCmdID.LASTCHAR,
                    (uint)VSConstants.VSStd2KCmdID.LASTCHAR_EXT,
                    (uint)VSConstants.VSStd2KCmdID.PAGEUP,
                    (uint)VSConstants.VSStd2KCmdID.PAGEUP_EXT,
                    (uint)VSConstants.VSStd2KCmdID.PAGEDN,
                    (uint)VSConstants.VSStd2KCmdID.PAGEDN_EXT,
                    (uint)VSConstants.VSStd2KCmdID.TOPLINE,
                    (uint)VSConstants.VSStd2KCmdID.TOPLINE_EXT,
                    (uint)VSConstants.VSStd2KCmdID.BOTTOMLINE,
                    (uint)VSConstants.VSStd2KCmdID.BOTTOMLINE_EXT,
                    (uint)VSConstants.VSStd2KCmdID.SELTABIFY,
                    (uint)VSConstants.VSStd2KCmdID.SELUNTABIFY,
                    (uint)VSConstants.VSStd2KCmdID.SELLOWCASE,
                    (uint)VSConstants.VSStd2KCmdID.SELUPCASE,
                    (uint)VSConstants.VSStd2KCmdID.SELECTCURRENTWORD,
                    (uint)VSConstants.VSStd2KCmdID.DELETEWORDRIGHT,
                    (uint)VSConstants.VSStd2KCmdID.DELETEWORDLEFT,
                    (uint)VSConstants.VSStd2KCmdID.WORDPREV,
                    (uint)VSConstants.VSStd2KCmdID.WORDPREV_EXT,
                    (uint)VSConstants.VSStd2KCmdID.WORDNEXT,
                    (uint)VSConstants.VSStd2KCmdID.WORDNEXT_EXT,
                    (uint)VSConstants.VSStd2KCmdID.FORMATDOCUMENT,
                    (uint)VSConstants.VSStd2KCmdID.FORMATSELECTION,
                    (uint)VSConstants.VSStd2KCmdID.COMMENT_BLOCK,
                    (uint)VSConstants.VSStd2KCmdID.COMMENTBLOCK,
                    (uint)VSConstants.VSStd2KCmdID.UNCOMMENT_BLOCK,
                    (uint)VSConstants.VSStd2KCmdID.UNCOMMENTBLOCK,
                    (uint)VSConstants.VSStd2KCmdID.DELETEWHITESPACE,
                    (uint)VSConstants.VSStd2KCmdID.DELETELINE,
                    (uint)VSConstants.VSStd2KCmdID.DELETEBLANKLINES,
                    (uint)VSConstants.VSStd2KCmdID.INDENT,
                    (uint)VSConstants.VSStd2KCmdID.UNINDENT,
                    (uint)VSConstants.VSStd2KCmdID.EditorLineFirstColumn,
                    (uint)VSConstants.VSStd2KCmdID.EditorLineFirstColumnExtend,
                    (uint)VSConstants.VSStd2KCmdID.GOTOBRACE,
                    (uint)VSConstants.VSStd2KCmdID.GOTOBRACE_EXT,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_HIDE_SELECTION,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_TOGGLE_CURRENT,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_TOGGLE_ALL,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_STOP_HIDING_ALL,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_STOP_HIDING_CURRENT,
                    (uint)VSConstants.VSStd2KCmdID.OUTLN_COLLAPSE_TO_DEF
                }
            },
            {
                // VSConstants.VSStd97
                Guid.Parse("5EFC7975-14BC-11CF-9B2B-00AA00573819"),
                new HashSet<uint>
                {
                    (uint)VSConstants.VSStd97CmdID.Copy,
                    (uint)VSConstants.VSStd97CmdID.Cut,
                    (uint)VSConstants.VSStd97CmdID.Delete,
                    (uint)VSConstants.VSStd97CmdID.Paste,
                    (uint)VSConstants.VSStd97CmdID.Redo,
                    (uint)VSConstants.VSStd97CmdID.MultiLevelRedo,
                    (uint)VSConstants.VSStd97CmdID.SelectAll,
                    (uint)VSConstants.VSStd97CmdID.Undo,
                    (uint)VSConstants.VSStd97CmdID.SaveProjectItem,
                    // Support to find features.
                    (uint)VSConstants.VSStd97CmdID.FindNext,
                    (uint)VSConstants.VSStd97CmdID.FindPrev,
                    (uint)VSConstants.VSStd97CmdID.FindSelectedNext,
                    (uint)VSConstants.VSStd97CmdID.FindSelectedPrev
                }
            },
            {
                // VSConstants.VSStd12
                Guid.Parse("2A8866DC-7BDE-4DC8-A360-A60679534384"),
                new HashSet<uint>
                {
                    258, // VSConstants.VSStd12CmdID.MoveSelLinesUp
                    259, // VSConstants.VSStd12CmdID.MoveSelLinesDown
                }
            },
            {
                // Organize usings menu
                Guid.Parse("5D7E7F65-A63F-46EE-84F1-990B2CAB23F9"),
                new HashSet<uint>
                {
                    6417, // Remove unused usings
                    6418, // Sort usings
                    6419  // Remove and sort
                }
            }
        };
    }
}
