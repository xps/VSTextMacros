namespace VSTextMacros.Dialogs
{
    partial class SavedMacrosDialog
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
            this.macroListBox = new System.Windows.Forms.ListBox();
            this.renameButton = new System.Windows.Forms.Button();
            this.deleteButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.moveUpButton = new System.Windows.Forms.Button();
            this.moveDownButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // macroListBox
            // 
            this.macroListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.macroListBox.FormattingEnabled = true;
            this.macroListBox.IntegralHeight = false;
            this.macroListBox.Location = new System.Drawing.Point(12, 12);
            this.macroListBox.Name = "macroListBox";
            this.macroListBox.Size = new System.Drawing.Size(322, 259);
            this.macroListBox.TabIndex = 0;
            this.macroListBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.MacroListBox_MouseDoubleClick);
            // 
            // renameButton
            // 
            this.renameButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.renameButton.Location = new System.Drawing.Point(340, 41);
            this.renameButton.Name = "renameButton";
            this.renameButton.Size = new System.Drawing.Size(75, 23);
            this.renameButton.TabIndex = 2;
            this.renameButton.Text = "&Rename";
            this.renameButton.UseVisualStyleBackColor = true;
            this.renameButton.Click += new System.EventHandler(this.RenameButton_Click);
            // 
            // deleteButton
            // 
            this.deleteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.deleteButton.Location = new System.Drawing.Point(340, 128);
            this.deleteButton.Name = "deleteButton";
            this.deleteButton.Size = new System.Drawing.Size(75, 23);
            this.deleteButton.TabIndex = 3;
            this.deleteButton.Text = "&Delete";
            this.deleteButton.UseVisualStyleBackColor = true;
            this.deleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.loadButton.Location = new System.Drawing.Point(340, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(75, 23);
            this.loadButton.TabIndex = 4;
            this.loadButton.Text = "&Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.LoadButton_Click);
            // 
            // moveUpButton
            // 
            this.moveUpButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveUpButton.Location = new System.Drawing.Point(340, 70);
            this.moveUpButton.Name = "moveUpButton";
            this.moveUpButton.Size = new System.Drawing.Size(75, 23);
            this.moveUpButton.TabIndex = 5;
            this.moveUpButton.Text = "Move &Up";
            this.moveUpButton.UseVisualStyleBackColor = true;
            this.moveUpButton.Click += new System.EventHandler(this.MoveUpButton_Click);
            // 
            // moveDownButton
            // 
            this.moveDownButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.moveDownButton.Location = new System.Drawing.Point(340, 99);
            this.moveDownButton.Name = "moveDownButton";
            this.moveDownButton.Size = new System.Drawing.Size(75, 23);
            this.moveDownButton.TabIndex = 6;
            this.moveDownButton.Text = "Move &Down";
            this.moveDownButton.UseVisualStyleBackColor = true;
            this.moveDownButton.Click += new System.EventHandler(this.MoveDownButton_Click);
            // 
            // SavedMacrosDialog
            // 
            this.AcceptButton = this.loadButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(427, 283);
            this.Controls.Add(this.moveDownButton);
            this.Controls.Add(this.moveUpButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.deleteButton);
            this.Controls.Add(this.renameButton);
            this.Controls.Add(this.macroListBox);
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(360, 175);
            this.Name = "SavedMacrosDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Saved Macros";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.SavedMacrosDialog_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox macroListBox;
        private System.Windows.Forms.Button renameButton;
        private System.Windows.Forms.Button deleteButton;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button moveUpButton;
        private System.Windows.Forms.Button moveDownButton;
    }
}