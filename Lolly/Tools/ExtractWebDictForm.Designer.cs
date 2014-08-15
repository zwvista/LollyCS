namespace Lolly
{
    partial class ExtractWebDictForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExtractWebDictForm));
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.wordToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.wordToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.langToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.dictToolStripLabel = new System.Windows.Forms.ToolStripLabel();
            this.dictToolStripTextBox = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.timerDocumentCompleted = new System.Windows.Forms.Timer(this.components);
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 25);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.Size = new System.Drawing.Size(790, 539);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.webBrowser1_DocumentCompleted);
            // 
            // wordToolStripLabel
            // 
            this.wordToolStripLabel.Name = "wordToolStripLabel";
            this.wordToolStripLabel.Size = new System.Drawing.Size(43, 22);
            this.wordToolStripLabel.Text = "Word:";
            // 
            // wordToolStripTextBox
            // 
            this.wordToolStripTextBox.Enabled = false;
            this.wordToolStripTextBox.Name = "wordToolStripTextBox";
            this.wordToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel2.Text = "Language:";
            // 
            // langToolStripTextBox
            // 
            this.langToolStripTextBox.Enabled = false;
            this.langToolStripTextBox.Name = "langToolStripTextBox";
            this.langToolStripTextBox.Size = new System.Drawing.Size(100, 25);
            // 
            // dictToolStripLabel
            // 
            this.dictToolStripLabel.Name = "dictToolStripLabel";
            this.dictToolStripLabel.Size = new System.Drawing.Size(72, 22);
            this.dictToolStripLabel.Text = "Dictionary:";
            // 
            // dictToolStripTextBox
            // 
            this.dictToolStripTextBox.Enabled = false;
            this.dictToolStripTextBox.Name = "dictToolStripTextBox";
            this.dictToolStripTextBox.Size = new System.Drawing.Size(125, 25);
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wordToolStripLabel,
            this.wordToolStripTextBox,
            this.toolStripLabel2,
            this.langToolStripTextBox,
            this.dictToolStripLabel,
            this.dictToolStripTextBox});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(790, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // timerDocumentCompleted
            // 
            this.timerDocumentCompleted.Tick += new System.EventHandler(this.timerDocumentCompleted_Tick);
            // 
            // ExtractWebDictForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(790, 564);
            this.Controls.Add(this.webBrowser1);
            this.Controls.Add(this.toolStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExtractWebDictForm";
            this.Text = "ExtractWebDictForm";
            this.Shown += new System.EventHandler(this.ExtractWebDictForm_Shown);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.ToolStripLabel wordToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox wordToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox langToolStripTextBox;
        private System.Windows.Forms.ToolStripLabel dictToolStripLabel;
        private System.Windows.Forms.ToolStripTextBox dictToolStripTextBox;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Timer timerDocumentCompleted;
    }
}