using Microsoft.VisualStudio;
using Microsoft.VisualStudio.OLE.Interop;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using VSTextMacros.Dialogs;
using VSTextMacros.Model;

namespace VSTextMacros
{
    // This filter is added to every text editor
    public class MacroCommandFilter : IOleCommandTarget
    {
        // A reference to the next IOleCommandTarget in the chain
        public IOleCommandTarget Next { get; set; }

        // A reference to the manager that can show/hide the visual cue when recording
        private MacroAdornmentManager AdornmentManager { get; set; }

        // Constructor
        public MacroCommandFilter(MacroAdornmentManager adornmentManager)
        {
            this.AdornmentManager = adornmentManager;
        }

        // Executes the menu commands and does the recording of other commands
        public int Exec(ref Guid pguidCmdGroup, uint nCmdID, uint nCmdexecopt, IntPtr pvaIn, IntPtr pvaOut)
        {
            if (pguidCmdGroup == GuidList.guidToolsGroup)
            {
                // Record/stop
                if (nCmdID == PkgCmdIDList.idRecordMacro)
                {
                    if (Macro.CurrentMacro == null || !Macro.CurrentMacro.IsRecording)
                    {
                        Macro.StartNew();
                        AdornmentManager.ShowVisual();
                    }
                    else
                    {
                        Macro.CurrentMacro.StopRecording();
                        Macro.SaveToFile(Macro.CurrentMacro, Path.Combine(VSTextMacrosPackage.Current.MacroDirectory, "Current.xml"));
                        AdornmentManager.HideVisual();
                    }

                    return VSConstants.S_OK;
                }

                // Playback
                if (nCmdID == PkgCmdIDList.idPlaybackMacro)
                {
                    if (Macro.CurrentMacro == null)
                        MessageBox.Show("Can't playback: no macro was recorded");
                    else if (Macro.CurrentMacro.IsRecording)
                        MessageBox.Show("Can't playback: a macro is currently recording. Stop recording first.");
                    else
                        Playback(Macro.CurrentMacro);

                    return VSConstants.S_OK;
                }

                // Playback multiple times
                if (nCmdID == PkgCmdIDList.idPlaybackMacroMultipleTimes)
                {
                    if (Macro.CurrentMacro == null)
                        MessageBox.Show("Can't playback: no macro was recorded");
                    else if (Macro.CurrentMacro.IsRecording)
                        MessageBox.Show("Can't playback: a macro is currently recording. Stop recording first.");
                    else
                    {
                        var dlg = new RepeatMacroMultipleTimesDialog();
                        dlg.ShowDialog();
                        Playback(Macro.CurrentMacro, dlg.Times);
                    }

                    return VSConstants.S_OK;
                }

                // Save macro
                if (nCmdID == PkgCmdIDList.idSaveMacro)
                {
                    if (Macro.CurrentMacro == null)
                        MessageBox.Show("Can't save macro: no macro was recorded");
                    else if (Macro.CurrentMacro.IsRecording)
                        MessageBox.Show("Can't save macro: a macro is currently recording. Stop recording first.");
                    else
                        ShowSaveMacroDialog(Macro.CurrentMacro);

                    return VSConstants.S_OK;
                }

                // Open saved macros
                if (nCmdID == PkgCmdIDList.idOpenSavedMacros)
                {
                    var list = SavedMacros.GetMacroList();
                    var dialog = new SavedMacrosDialog(list);
                    dialog.ShowDialog();
                    return VSConstants.S_OK;
                }
            }

            // Are we recording?
            if (Macro.CurrentMacro != null && Macro.CurrentMacro.IsRecording)
            {
                // Recordable command?
                if (RecordableCommands.Commands.ContainsKey(pguidCmdGroup) && RecordableCommands.Commands[pguidCmdGroup].Contains(nCmdID))
                {
                    // For the TYPECHAR command, read the actual character code
                    if (pguidCmdGroup == VSConstants.VSStd2K && nCmdID == (uint)VSConstants.VSStd2KCmdID.TYPECHAR)
                        Macro.CurrentMacro.AddCommand(pguidCmdGroup, nCmdID, nCmdexecopt, GetTypedChar(pvaIn));
                    else
                        Macro.CurrentMacro.AddCommand(pguidCmdGroup, nCmdID, nCmdexecopt, null);
                }
#if DEBUG
                else
                {
                    Trace.WriteLine(string.Format("Not recorded - Guid: {0}, ID: {1}, Opts: {2}, In: {3}", pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn != IntPtr.Zero ? "yes" : "no"));
                }
#endif
            }

            return Next.Exec(ref pguidCmdGroup, nCmdID, nCmdexecopt, pvaIn, pvaOut);
        }

        // Updates menus states
        public int QueryStatus(ref Guid pguidCmdGroup, uint cCmds, OLECMD[] prgCmds, IntPtr pCmdText)
        {
            if (pguidCmdGroup == GuidList.guidToolsGroup)
            {
                // Arguments check
                if (prgCmds == null)
                    return VSConstants.E_POINTER;

                for (int i = 0; i < cCmds; i++)
                {
                    // Updates the text of the 'Start/Stop Recording' menu item
                    if (prgCmds[i].cmdID == PkgCmdIDList.idRecordMacro)
                    {
                        string menuText;
                        if (Macro.CurrentMacro == null || !Macro.CurrentMacro.IsRecording)
                            menuText = "&Start recording macro";
                        else
                            menuText = "&Stop recording macro";

                        // Copy the text to the OLECMDTEXT structure
                        if (pCmdText != IntPtr.Zero)
                        {
                            var cmdText = (OLECMDTEXT)Marshal.PtrToStructure(pCmdText, typeof(OLECMDTEXT));
                            if (cmdText.cmdtextf == (uint)OLECMDTEXTF.OLECMDTEXTF_NAME)
                                SetText(pCmdText, menuText);
                        }
                        
                        // Enable the menu
                        prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED);
                    }

                    // Enables/disables the 'Playback' and 'Save Macro' menu items
                    if (prgCmds[i].cmdID == PkgCmdIDList.idPlaybackMacro || prgCmds[i].cmdID == PkgCmdIDList.idPlaybackMacroMultipleTimes || prgCmds[i].cmdID == PkgCmdIDList.idSaveMacro)
                    {
                        if (Macro.CurrentMacro == null || Macro.CurrentMacro.IsRecording)
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED);
                        else
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED);
                    }
                }

                return VSConstants.S_OK;
            }

            return Next.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        // Replays a macro
        private void Playback(Macro macro, int times = 1)
        {
            bool ownUndoContext = !VSTextMacrosPackage.Current.DTE.UndoContext.IsOpen;
            if (ownUndoContext)
                VSTextMacrosPackage.Current.DTE.UndoContext.Open("Macro");

            var pvaIn = Marshal.AllocCoTaskMem(16);
            try
            {
                for (int i = 0; i < times; i++)
                {
                    foreach (var command in macro.Commands)
                    {
                        var pguidCmdGroup = command.CommandGroup;

                        if (command.Character != null)
                        {
                            Marshal.GetNativeVariantForObject((ushort)command.Character, pvaIn);
                            Next.Exec(ref pguidCmdGroup, command.CommandID, command.CommandOptions, pvaIn, IntPtr.Zero);
                        }
                        else
                            Next.Exec(ref pguidCmdGroup, command.CommandID, command.CommandOptions, IntPtr.Zero, IntPtr.Zero);
                    }
                }
            }
            finally
            {
                if (pvaIn != IntPtr.Zero)
                    Marshal.FreeCoTaskMem(pvaIn);
            }

            if (ownUndoContext)
                VSTextMacrosPackage.Current.DTE.UndoContext.Close();
        }

        // Shows the save macro dialog
        private void ShowSaveMacroDialog(Macro macro, string name = null)
        {
            var dialog = new SaveMacroDialog(name);
            if (dialog.ShowDialog() == DialogResult.OK)
                SavedMacros.SaveMacro(macro, dialog.MacroName);
        }

        // Gets the character at the given pointer
        private static char GetTypedChar(IntPtr pvaIn)
        {
            return (char)(ushort)Marshal.GetObjectForNativeVariant(pvaIn);
        }

        // Copies text to a OLECMDTEXT structure
        private static void SetText(IntPtr pCmdText, string text)
        {
            var cmdText = (OLECMDTEXT)Marshal.PtrToStructure(pCmdText, typeof(OLECMDTEXT));
            
            var offsetRgwz = Marshal.OffsetOf(typeof(OLECMDTEXT), "rgwz");
            var offsetCwActual = Marshal.OffsetOf(typeof(OLECMDTEXT), "cwActual");

            var bytes = text.ToCharArray();
            var maxBytes = Math.Min((int)cmdText.cwBuf - 1, bytes.Length);

            Marshal.Copy(bytes, 0, (IntPtr)((long)pCmdText + (long)offsetRgwz), maxBytes);
            Marshal.WriteInt16((IntPtr)((long)pCmdText + (long)offsetRgwz + maxBytes * 2), 0);
            Marshal.WriteInt32((IntPtr)((long)pCmdText + (long)offsetCwActual), maxBytes + 1);
        }
    }
}
