namespace Lolly
{
    partial class BlogPostForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BlogPostForm));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.sourceViewToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.lineOnlyToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.newWordToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newPatternToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.patternNamesToolStripComboBox = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.originalToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.definitionToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.translationToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.b_ToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.i_ToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.sourceTextBox = new TextBoxEx();
            this.applicationControl1 = new AppControl.ApplicationControl();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sourceViewToolStripButton,
            this.toolStripSeparator4,
            this.lineOnlyToolStripButton,
            this.newWordToolStripButton,
            this.toolStripSeparator1,
            this.newPatternToolStripButton,
            this.patternNamesToolStripComboBox,
            this.toolStripSeparator2,
            this.originalToolStripButton,
            this.definitionToolStripButton,
            this.translationToolStripButton,
            this.toolStripSeparator3,
            this.b_ToolStripButton,
            this.i_ToolStripButton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(784, 25);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // sourceViewToolStripButton
            // 
            this.sourceViewToolStripButton.CheckOnClick = true;
            this.sourceViewToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.sourceViewToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("sourceViewToolStripButton.Image")));
            this.sourceViewToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.sourceViewToolStripButton.Name = "sourceViewToolStripButton";
            this.sourceViewToolStripButton.Size = new System.Drawing.Size(73, 22);
            this.sourceViewToolStripButton.Text = "Source View";
            this.sourceViewToolStripButton.Click += new System.EventHandler(this.sourceViewToolStripButton_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // lineOnlyToolStripButton
            // 
            this.lineOnlyToolStripButton.CheckOnClick = true;
            this.lineOnlyToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.lineOnlyToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("lineOnlyToolStripButton.Image")));
            this.lineOnlyToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.lineOnlyToolStripButton.Name = "lineOnlyToolStripButton";
            this.lineOnlyToolStripButton.Size = new System.Drawing.Size(57, 22);
            this.lineOnlyToolStripButton.Text = "Line Only";
            // 
            // newWordToolStripButton
            // 
            this.newWordToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newWordToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newWordToolStripButton.Image")));
            this.newWordToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newWordToolStripButton.Name = "newWordToolStripButton";
            this.newWordToolStripButton.Size = new System.Drawing.Size(34, 22);
            this.newWordToolStripButton.Text = "Word";
            this.newWordToolStripButton.Click += new System.EventHandler(this.newWordToolStripButton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // newPatternToolStripButton
            // 
            this.newPatternToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.newPatternToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("newPatternToolStripButton.Image")));
            this.newPatternToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.newPatternToolStripButton.Name = "newPatternToolStripButton";
            this.newPatternToolStripButton.Size = new System.Drawing.Size(46, 22);
            this.newPatternToolStripButton.Text = "Pattern";
            this.newPatternToolStripButton.Click += new System.EventHandler(this.newPatternToolStripButton_Click);
            // 
            // patternNamesToolStripComboBox
            // 
            this.patternNamesToolStripComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.patternNamesToolStripComboBox.DropDownWidth = 400;
            this.patternNamesToolStripComboBox.Name = "patternNamesToolStripComboBox";
            this.patternNamesToolStripComboBox.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // originalToolStripButton
            // 
            this.originalToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.originalToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("originalToolStripButton.Image")));
            this.originalToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.originalToolStripButton.Name = "originalToolStripButton";
            this.originalToolStripButton.Size = new System.Drawing.Size(48, 22);
            this.originalToolStripButton.Text = "Original";
            this.originalToolStripButton.Click += new System.EventHandler(this.originalToolStripButton_Click);
            // 
            // definitionToolStripButton
            // 
            this.definitionToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.definitionToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("definitionToolStripButton.Image")));
            this.definitionToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.definitionToolStripButton.Name = "definitionToolStripButton";
            this.definitionToolStripButton.Size = new System.Drawing.Size(58, 22);
            this.definitionToolStripButton.Text = "Definition";
            this.definitionToolStripButton.Click += new System.EventHandler(this.definitionToolStripButton_Click);
            // 
            // translationToolStripButton
            // 
            this.translationToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.translationToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("translationToolStripButton.Image")));
            this.translationToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.translationToolStripButton.Name = "translationToolStripButton";
            this.translationToolStripButton.Size = new System.Drawing.Size(66, 22);
            this.translationToolStripButton.Text = "Translation";
            this.translationToolStripButton.Click += new System.EventHandler(this.translationToolStripButton_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // b_ToolStripButton
            // 
            this.b_ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.b_ToolStripButton.Enabled = false;
            this.b_ToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("b_ToolStripButton.Image")));
            this.b_ToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.b_ToolStripButton.Name = "b_ToolStripButton";
            this.b_ToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.b_ToolStripButton.Text = "B ";
            this.b_ToolStripButton.Click += new System.EventHandler(this.bi_ToolStripButton_Click);
            // 
            // i_ToolStripButton
            // 
            this.i_ToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.i_ToolStripButton.Enabled = false;
            this.i_ToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("i_ToolStripButton.Image")));
            this.i_ToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.i_ToolStripButton.Name = "i_ToolStripButton";
            this.i_ToolStripButton.Size = new System.Drawing.Size(23, 22);
            this.i_ToolStripButton.Text = "I ";
            this.i_ToolStripButton.Click += new System.EventHandler(this.bi_ToolStripButton_Click);
            // 
            // sourceTextBox
            // 
            this.sourceTextBox.AcceptsReturn = true;
            this.sourceTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sourceTextBox.Location = new System.Drawing.Point(0, 25);
            this.sourceTextBox.Multiline = true;
            this.sourceTextBox.Name = "sourceTextBox";
            this.sourceTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sourceTextBox.Size = new System.Drawing.Size(784, 537);
            this.sourceTextBox.TabIndex = 2;
            this.sourceTextBox.Visible = false;
            // 
            // applicationControl1
            // 
            this.applicationControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.applicationControl1.ExeName = global::Lolly.Properties.Settings.Default.XmlNotepadExe;
            this.applicationControl1.Location = new System.Drawing.Point(0, 25);
            this.applicationControl1.Name = "applicationControl1";
            this.applicationControl1.Size = new System.Drawing.Size(784, 537);
            this.applicationControl1.TabIndex = 1;
            // 
            // BlogPostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 562);
            this.Controls.Add(this.sourceTextBox);
            this.Controls.Add(this.applicationControl1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "BlogPostForm";
            this.Text = "Blog Post";
            this.Load += new System.EventHandler(this.BlogPostForm_Load);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton lineOnlyToolStripButton;
        private System.Windows.Forms.ToolStripButton definitionToolStripButton;
        private System.Windows.Forms.ToolStripButton translationToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private AppControl.ApplicationControl applicationControl1;
        private System.Windows.Forms.ToolStripComboBox patternNamesToolStripComboBox;
        private System.Windows.Forms.ToolStripButton originalToolStripButton;
        private System.Windows.Forms.ToolStripButton newWordToolStripButton;
        private System.Windows.Forms.ToolStripButton newPatternToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton b_ToolStripButton;
        private System.Windows.Forms.ToolStripButton i_ToolStripButton;
        private System.Windows.Forms.ToolStripButton sourceViewToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private TextBoxEx sourceTextBox;
    }
}