using System;
using System.Windows.Forms;

namespace VSTextMacros.Dialogs
{
    public partial class RepeatMacroMultipleTimesDialog : Form
    {
        public int Times { get; private set; }

        public RepeatMacroMultipleTimesDialog()
        {
            InitializeComponent();
        }

        private void repeatButton_Click(object sender, EventArgs e)
        {
            Times = Convert.ToInt32(timesTextBox.Text.Trim());
            DialogResult = DialogResult.OK;
            Close();
        }

        private void timesTextBox_TextChanged(object sender, EventArgs e)
        {
            int times;
            repeatButton.Enabled = !string.IsNullOrWhiteSpace(timesTextBox.Text) && int.TryParse(timesTextBox.Text, out times) && times > 0;
        }

        private void RepeatMacroMultipleTimesDialog_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
                Close();
            }
        }
    }
}