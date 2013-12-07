namespace VSTextMacros.Dialogs
{
    partial class RepeatMacroMultipleTimesDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.timesTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.repeatButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timesTextBox
            // 
            this.timesTextBox.Location = new System.Drawing.Point(12, 27);
            this.timesTextBox.MaxLength = 6;
            this.timesTextBox.Name = "timesTextBox";
            this.timesTextBox.Size = new System.Drawing.Size(454, 20);
            this.timesTextBox.TabIndex = 0;
            this.timesTextBox.Text = "1";
            this.timesTextBox.TextChanged += new System.EventHandler(this.timesTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(152, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Number of times to repeat:";
            // 
            // repeatButton
            // 
            this.repeatButton.Location = new System.Drawing.Point(391, 53);
            this.repeatButton.Name = "repeatButton";
            this.repeatButton.Size = new System.Drawing.Size(75, 23);
            this.repeatButton.TabIndex = 2;
            this.repeatButton.Text = "Repeat";
            this.repeatButton.UseVisualStyleBackColor = true;
            this.repeatButton.Click += new System.EventHandler(this.repeatButton_Click);
            // 
            // RepeatMacroMultipleTimesDialog
            // 
            this.AcceptButton = this.repeatButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(478, 87);
            this.Controls.Add(this.repeatButton);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.timesTextBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RepeatMacroMultipleTimesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Repeat macro multiple times...";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.RepeatMacroMultipleTimesDialog_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox timesTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button repeatButton;
    }
}