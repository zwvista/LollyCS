namespace Lolly
{
    partial class NewPhrasesDlg
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
            this.label1 = new System.Windows.Forms.Label();
            this.phrasesTranslationsTextBox = new LollyTextBox();
            this.cancelButton = new VistaButtonTest.VistaButton();
            this.okButton = new VistaButtonTest.VistaButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(192, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Phrases && Translations:";
            // 
            // phrasesTranslationsTextBox
            // 
            this.phrasesTranslationsTextBox.AcceptsReturn = true;
            this.phrasesTranslationsTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.phrasesTranslationsTextBox.Location = new System.Drawing.Point(12, 36);
            this.phrasesTranslationsTextBox.Multiline = true;
            this.phrasesTranslationsTextBox.Name = "phrasesTranslationsTextBox";
            this.phrasesTranslationsTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.phrasesTranslationsTextBox.Size = new System.Drawing.Size(470, 193);
            this.phrasesTranslationsTextBox.TabIndex = 1;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BaseColor = System.Drawing.Color.Transparent;
            this.cancelButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cancelButton.ButtonText = "Cancel";
            this.cancelButton.CornerRadius = 20;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(189)))));
            this.cancelButton.Location = new System.Drawing.Point(368, 240);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 32);
            this.cancelButton.TabIndex = 5;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BaseColor = System.Drawing.Color.Transparent;
            this.okButton.ButtonColor = System.Drawing.Color.MediumBlue;
            this.okButton.ButtonText = "OK";
            this.okButton.CornerRadius = 20;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.GlowColor = System.Drawing.Color.Cyan;
            this.okButton.Location = new System.Drawing.Point(244, 240);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 4;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // NewPhrasesDlg
            // 
            this.AcceptButton = this.okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.CancelButton = this.cancelButton;
            this.ClientSize = new System.Drawing.Size(494, 282);
            this.ControlBox = false;
            this.Controls.Add(this.phrasesTranslationsTextBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(this.okButton);
            this.Font = new System.Drawing.Font("SimSun", 12F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 310);
            this.Name = "NewPhrasesDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Phrases";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private VistaButtonTest.VistaButton cancelButton;
        private VistaButtonTest.VistaButton okButton;
        private System.Windows.Forms.Label label1;
        private LollyTextBox phrasesTranslationsTextBox;
    }
}