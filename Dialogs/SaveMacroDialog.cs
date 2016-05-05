using System;
using System.Windows.Forms;

namespace VSTextMacros.Dialogs
{
    public partial class SaveMacroDialog : Form
    {
        public string MacroName { get; set; }

        public SaveMacroDialog(string macroName = null)
        {
            InitializeComponent();
            macroNameTextBox.Text = macroName;
            saveButton.Enabled = !string.IsNullOrWhiteSpace(macroName);
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            MacroName = macroNameTextBox.Text.Trim();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void macroNameTextBox_TextChanged(object sender, EventArgs e)
        {
            saveButton.Enabled = !string.IsNullOrWhiteSpace(macroNameTextBox.Text);
        }

        private void SaveMacroDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}
