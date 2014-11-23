namespace Lolly
{
    partial class ConfigDictDlg
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
            this.dictTreeView = new System.Windows.Forms.TreeView();
            this.sharedImageLists11 = new Lolly.SharedImageLists1(this.components);
            this.imageList1 = this.sharedImageLists11.NewImageList(this.components, ((Lolly.SharedImageLists1)(this.sharedImageLists11.GetSharedImageLists())).imageList1);
            this.addButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.dictListView = new System.Windows.Forms.ListView();
            this.Dictionaries = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.cancelButton = new VistaButtonTest.VistaButton();
            this.okButton = new VistaButtonTest.VistaButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.topButton = new System.Windows.Forms.Button();
            this.BottomButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dictTreeView
            // 
            this.dictTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dictTreeView.HideSelection = false;
            this.dictTreeView.ImageIndex = 0;
            this.dictTreeView.ImageList = this.imageList1;
            this.dictTreeView.Location = new System.Drawing.Point(0, 0);
            this.dictTreeView.Margin = new System.Windows.Forms.Padding(4);
            this.dictTreeView.Name = "dictTreeView";
            this.dictTreeView.SelectedImageIndex = 0;
            this.dictTreeView.Size = new System.Drawing.Size(200, 636);
            this.dictTreeView.TabIndex = 0;
            // 
            // addButton
            // 
            this.addButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addButton.Location = new System.Drawing.Point(206, 13);
            this.addButton.Margin = new System.Windows.Forms.Padding(4);
            this.addButton.Name = "addButton";
            this.addButton.Size = new System.Drawing.Size(100, 31);
            this.addButton.TabIndex = 1;
            this.addButton.Text = "Add";
            this.addButton.UseVisualStyleBackColor = true;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(206, 66);
            this.removeButton.Margin = new System.Windows.Forms.Padding(4);
            this.removeButton.Name = "removeButton";
            this.removeButton.Size = new System.Drawing.Size(100, 31);
            this.removeButton.TabIndex = 1;
            this.removeButton.Text = "Remove";
            this.removeButton.UseVisualStyleBackColor = true;
            this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
            // 
            // upButton
            // 
            this.upButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.upButton.Location = new System.Drawing.Point(206, 172);
            this.upButton.Margin = new System.Windows.Forms.Padding(4);
            this.upButton.Name = "upButton";
            this.upButton.Size = new System.Drawing.Size(100, 31);
            this.upButton.TabIndex = 1;
            this.upButton.Text = "Up";
            this.upButton.UseVisualStyleBackColor = true;
            this.upButton.Click += new System.EventHandler(this.upButton_Click);
            // 
            // downButton
            // 
            this.downButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.downButton.Location = new System.Drawing.Point(206, 225);
            this.downButton.Margin = new System.Windows.Forms.Padding(4);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(100, 31);
            this.downButton.TabIndex = 1;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
            // 
            // dictListView
            // 
            this.dictListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dictListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Dictionaries});
            this.dictListView.FullRowSelect = true;
            this.dictListView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.dictListView.HideSelection = false;
            this.dictListView.Location = new System.Drawing.Point(0, 0);
            this.dictListView.Name = "dictListView";
            this.dictListView.Size = new System.Drawing.Size(200, 636);
            this.dictListView.SmallImageList = this.imageList1;
            this.dictListView.TabIndex = 2;
            this.dictListView.UseCompatibleStateImageBehavior = false;
            this.dictListView.View = System.Windows.Forms.View.Details;
            // 
            // Dictionaries
            // 
            this.Dictionaries.Text = "Dictionaries";
            this.Dictionaries.Width = 146;
            // 
            // cancelButton
            // 
            this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cancelButton.BackColor = System.Drawing.Color.Transparent;
            this.cancelButton.BaseColor = System.Drawing.Color.Transparent;
            this.cancelButton.ButtonColor = System.Drawing.Color.FromArgb(((int)(((byte)(210)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cancelButton.ButtonText = "Cancel";
            this.cancelButton.CornerRadius = 20;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelButton.GlowColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(141)))), ((int)(((byte)(189)))));
            this.cancelButton.Location = new System.Drawing.Point(203, 62);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.Size = new System.Drawing.Size(100, 32);
            this.cancelButton.TabIndex = 14;
            // 
            // okButton
            // 
            this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.okButton.BackColor = System.Drawing.Color.Transparent;
            this.okButton.BaseColor = System.Drawing.Color.Transparent;
            this.okButton.ButtonColor = System.Drawing.Color.MediumBlue;
            this.okButton.ButtonText = "OK";
            this.okButton.CornerRadius = 20;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.okButton.GlowColor = System.Drawing.Color.Cyan;
            this.okButton.Location = new System.Drawing.Point(203, 11);
            this.okButton.Name = "okButton";
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 15;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.addButton);
            this.splitContainer1.Panel1.Controls.Add(this.dictTreeView);
            this.splitContainer1.Panel1.Controls.Add(this.removeButton);
            this.splitContainer1.Panel1.Controls.Add(this.BottomButton);
            this.splitContainer1.Panel1.Controls.Add(this.topButton);
            this.splitContainer1.Panel1.Controls.Add(this.downButton);
            this.splitContainer1.Panel1.Controls.Add(this.upButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.cancelButton);
            this.splitContainer1.Panel2.Controls.Add(this.okButton);
            this.splitContainer1.Panel2.Controls.Add(this.dictListView);
            this.splitContainer1.Size = new System.Drawing.Size(628, 637);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 16;
            // 
            // topButton
            // 
            this.topButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topButton.Location = new System.Drawing.Point(205, 119);
            this.topButton.Margin = new System.Windows.Forms.Padding(4);
            this.topButton.Name = "topButton";
            this.topButton.Size = new System.Drawing.Size(100, 31);
            this.topButton.TabIndex = 1;
            this.topButton.Text = "Top";
            this.topButton.UseVisualStyleBackColor = true;
            this.topButton.Click += new System.EventHandler(this.topButton_Click);
            // 
            // BottomButton
            // 
            this.BottomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomButton.Location = new System.Drawing.Point(205, 278);
            this.BottomButton.Margin = new System.Windows.Forms.Padding(4);
            this.BottomButton.Name = "BottomButton";
            this.BottomButton.Size = new System.Drawing.Size(100, 31);
            this.BottomButton.TabIndex = 1;
            this.BottomButton.Text = "Bottom";
            this.BottomButton.UseVisualStyleBackColor = true;
            this.BottomButton.Click += new System.EventHandler(this.BottomButton_Click);
            // 
            // ConfigDictDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(628, 637);
            this.Controls.Add(this.splitContainer1);
            this.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigDictDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Dictionaries";
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TreeView dictTreeView;
        private System.Windows.Forms.Button addButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private System.Windows.Forms.ListView dictListView;
        private System.Windows.Forms.ColumnHeader Dictionaries;
        private VistaButtonTest.VistaButton cancelButton;
        private VistaButtonTest.VistaButton okButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private SharedImageLists1 sharedImageLists11;
        protected internal System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button BottomButton;
        private System.Windows.Forms.Button topButton;
    }
}