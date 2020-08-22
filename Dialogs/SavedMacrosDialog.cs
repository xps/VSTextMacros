using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using VSTextMacros.Model;

namespace VSTextMacros.Dialogs
{
    public partial class SavedMacrosDialog : Form
    {
        private readonly BindingList<SavedMacro> macros;

        public SavedMacrosDialog(IList<SavedMacro> list)
        {
            InitializeComponent();

            macros = new BindingList<SavedMacro>(list);

            macroListBox.DisplayMember = "Name";
            macroListBox.ValueMember = "Guid";
            macroListBox.DataSource = macros;
        }
        
        private void LoadButton_Click(object sender, EventArgs e)
        {
            LoadSelectedMacro();
        }

        private void RenameButton_Click(object sender, EventArgs e)
        {
            var macro = macroListBox.SelectedItem as SavedMacro;
            if (macro != null)
            {
                ShowRenameMacroDialog(macro);
            }
        }

        private void MoveUpButton_Click(object sender, EventArgs e)
        {
            var macro = macroListBox.SelectedItem as SavedMacro;
            var index = macros.IndexOf(macro);
            if (index > 0)
            {
                var temp = macros[index - 1];
                macros[index - 1] = macro;
                macros[index] = temp;
                macroListBox.SelectedIndex = index - 1;
                SavedMacros.SaveMacroList(macros);
            }
        }

        private void MoveDownButton_Click(object sender, EventArgs e)
        {
            var macro = macroListBox.SelectedItem as SavedMacro;
            var index = macros.IndexOf(macro);
            if (index < macros.Count - 1)
            {
                var temp = macros[index + 1];
                macros[index + 1] = macro;
                macros[index] = temp;
                macroListBox.SelectedIndex = index + 1;
                SavedMacros.SaveMacroList(macros);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            var macro = macroListBox.SelectedItem as SavedMacro;
            if (macro != null)
            {
                SavedMacros.DeleteMacro(macro.Guid);
                macros.Remove(macro);
            }
        }

        private void MacroListBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            LoadSelectedMacro();
        }

        private void SavedMacrosDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                Close();

            var index = -1;
            var code = (int)e.KeyCode;

            if (code >= (int)Keys.NumPad1 && code <= (int)Keys.NumPad9)
                index = code - (int)Keys.NumPad1;
            if (code >= (int)Keys.D1 && code <= (int)Keys.D9)
                index = code - (int)Keys.D1;

            if (index >= 0 && index < macroListBox.Items.Count)
            {
                var macro = (SavedMacro)macroListBox.Items[index];
                Macro.CurrentMacro = SavedMacros.GetSavedMacro(macro.Guid);
                Close();
            }
        }

        private void ShowRenameMacroDialog(SavedMacro macro)
        {
            var dialog = new SaveMacroDialog(macro.Name);
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                SavedMacros.RenameMacro(macro.Guid, dialog.MacroName);
                macro.Name = dialog.MacroName;
                macros.ResetItem(macroListBox.SelectedIndex);
            }
        }

        private void LoadSelectedMacro()
        {
            var macro = macroListBox.SelectedItem as SavedMacro;
            if (macro != null)
            {
                Macro.CurrentMacro = SavedMacros.GetSavedMacro(macro.Guid);
                Close();
            }
        }
    }
}
