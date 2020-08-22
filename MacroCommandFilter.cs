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
            if (pguidCmdGroup == GuidList.guidMacrosCmdSet)
            {
                // Record/stop
                if (nCmdID == PkgCmdIDList.idRecordMacro)
                {
                    if (Macro.CurrentMacro == null || !Macro.CurrentMacro.IsRecording)
                        StartRecording();
                    else
                        StopRecording();

                    return VSConstants.S_OK;
                }

                // Playback
                if (nCmdID == PkgCmdIDList.idPlaybackMacro)
                {
                    if (Macro.CurrentMacro == null)
                    {
                        MessageBox.Show("Can't playback: no macro was recorded");
                    }
                    else
                    {
                        if (Macro.CurrentMacro.IsRecording)
                            StopRecording();
                        Playback(Macro.CurrentMacro);
                    }

                    return VSConstants.S_OK;
                }
                
                // Playback multiple times
                if (nCmdID == PkgCmdIDList.idPlaybackMacroMultipleTimes)
                {
                    if (Macro.CurrentMacro == null)
                    {
                        MessageBox.Show("Can't playback: no macro was recorded");
                    }
                    else
                    {
                        if (Macro.CurrentMacro.IsRecording)
                            StopRecording();

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
                    {
                        MessageBox.Show("Can't save macro: no macro was recorded");
                    }
                    else
                    {
                        if (Macro.CurrentMacro.IsRecording)
                            StopRecording();

                        ShowSaveMacroDialog(Macro.CurrentMacro);
                    }

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

            if (pguidCmdGroup == GuidList.guidMacrosRunSavedCmdSet)
            {
                // Run saved macro #
                if (nCmdID >= PkgCmdIDList.idRunSavedMacro1 && nCmdID <= PkgCmdIDList.idRunSavedMacro5)
                {
                    var index = nCmdID - PkgCmdIDList.idRunSavedMacro1;
                    var macroList = SavedMacros.GetMacroList();
                    if (index < macroList.Count)
                    {
                        var macro = SavedMacros.GetSavedMacro(macroList[(int)index].Guid);
                        Playback(macro);
                    }

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
            if (pguidCmdGroup == GuidList.guidMacrosCmdSet)
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
                        if (Macro.CurrentMacro == null)
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED);
                        else
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED);
                    }
                }

                return VSConstants.S_OK;
            }

            if (pguidCmdGroup == GuidList.guidMacrosRunSavedCmdSet)
            {
                // Arguments check
                if (prgCmds == null)
                    return VSConstants.E_POINTER;

                for (int i = 0; i < cCmds; i++)
                {
                    if (prgCmds[i].cmdID >= PkgCmdIDList.idRunSavedMacro1 && prgCmds[i].cmdID <= PkgCmdIDList.idRunSavedMacro5)
                    {
                        var index = prgCmds[i].cmdID - PkgCmdIDList.idRunSavedMacro1;
                        var macros = SavedMacros.GetMacroList();
                        var enabled = index < macros.Count;

                        if (enabled)
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED | OLECMDF.OLECMDF_ENABLED);
                        else
                            prgCmds[i].cmdf = (uint)(OLECMDF.OLECMDF_SUPPORTED);
                    }
                }

                return VSConstants.S_OK;
            }

            return Next.QueryStatus(ref pguidCmdGroup, cCmds, prgCmds, pCmdText);
        }

        // Starts recording a macro
        private void StartRecording()
        {
            Macro.StartNew();
            AdornmentManager.ShowVisual();
        }

        // Stops recording a macro
        private void StopRecording()
        {
            Macro.CurrentMacro.StopRecording();
            Macro.SaveToFile(Macro.CurrentMacro, Path.Combine(VSTextMacrosPackage.Current.MacroDirectory, "Current.xml"));
            AdornmentManager.HideVisual();
        }

        // Replays a macro
        private void Playback(Macro macro, int times = 1)
        {
            var ownUndoContext = !VSTextMacrosPackage.Current.DTE.UndoContext.IsOpen;
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
                            continue;
                        }
                        // Add some support to "Find (Next/Previous)[Selected]" via DTE.
                        switch (command.CommandID) {
                            case (uint)VSConstants.VSStd97CmdID.FindNext:
                                VSTextMacrosPackage.Current.DTE.ExecuteCommand("Edit.FindNext");
                                break;
                            case (uint)VSConstants.VSStd97CmdID.FindSelectedNext:
                                VSTextMacrosPackage.Current.DTE.ExecuteCommand("Edit.FindNextSelected");
                                break;
                            case (uint)VSConstants.VSStd97CmdID.FindPrev:
                                VSTextMacrosPackage.Current.DTE.ExecuteCommand("Edit.FindPrevious");
                                break;
                            case (uint)VSConstants.VSStd97CmdID.FindSelectedPrev:
                                VSTextMacrosPackage.Current.DTE.ExecuteCommand("Edit.FindPreviousSelected");
                                break;
                            default:
                                Next.Exec(ref pguidCmdGroup, command.CommandID, command.CommandOptions, IntPtr.Zero, IntPtr.Zero);
                                break;
                        }
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
