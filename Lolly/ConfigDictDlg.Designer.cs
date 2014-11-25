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
            this.dictATreeView = new DllLolly.LollyTreeView();
            this.addCollectionButton = new System.Windows.Forms.Button();
            this.removeButton = new System.Windows.Forms.Button();
            this.upButton = new System.Windows.Forms.Button();
            this.downButton = new System.Windows.Forms.Button();
            this.cancelButton = new VistaButtonTest.VistaButton();
            this.okButton = new VistaButtonTest.VistaButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.clearButton = new System.Windows.Forms.Button();
            this.addAllbutton = new System.Windows.Forms.Button();
            this.addSwitchbutton = new System.Windows.Forms.Button();
            this.BottomButton = new System.Windows.Forms.Button();
            this.topButton = new System.Windows.Forms.Button();
            this.dictBTreeView = new System.Windows.Forms.TreeView();
            this.sharedImageLists11 = new Lolly.SharedImageLists1(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dictATreeView
            // 
            this.dictATreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dictATreeView.CheckBoxes = true;
            this.dictATreeView.HideSelection = false;
            this.dictATreeView.Location = new System.Drawing.Point(0, 0);
            this.dictATreeView.Margin = new System.Windows.Forms.Padding(4);
            this.dictATreeView.Name = "dictATreeView";
            this.dictATreeView.Size = new System.Drawing.Size(200, 636);
            this.dictATreeView.TabIndex = 0;
            this.dictATreeView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.dictATreeView_AfterCheck);
            // 
            // addCollectionButton
            // 
            this.addCollectionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addCollectionButton.Location = new System.Drawing.Point(206, 13);
            this.addCollectionButton.Margin = new System.Windows.Forms.Padding(4);
            this.addCollectionButton.Name = "addCollectionButton";
            this.addCollectionButton.Size = new System.Drawing.Size(101, 31);
            this.addCollectionButton.TabIndex = 1;
            this.addCollectionButton.Text = "Collection";
            this.addCollectionButton.UseVisualStyleBackColor = true;
            this.addCollectionButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // removeButton
            // 
            this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.removeButton.Location = new System.Drawing.Point(206, 201);
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
            this.upButton.Location = new System.Drawing.Point(208, 295);
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
            this.downButton.Location = new System.Drawing.Point(208, 342);
            this.downButton.Margin = new System.Windows.Forms.Padding(4);
            this.downButton.Name = "downButton";
            this.downButton.Size = new System.Drawing.Size(100, 31);
            this.downButton.TabIndex = 1;
            this.downButton.Text = "Down";
            this.downButton.UseVisualStyleBackColor = true;
            this.downButton.Click += new System.EventHandler(this.downButton_Click);
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
            this.splitContainer1.Panel1.Controls.Add(this.clearButton);
            this.splitContainer1.Panel1.Controls.Add(this.addAllbutton);
            this.splitContainer1.Panel1.Controls.Add(this.addSwitchbutton);
            this.splitContainer1.Panel1.Controls.Add(this.addCollectionButton);
            this.splitContainer1.Panel1.Controls.Add(this.dictATreeView);
            this.splitContainer1.Panel1.Controls.Add(this.removeButton);
            this.splitContainer1.Panel1.Controls.Add(this.BottomButton);
            this.splitContainer1.Panel1.Controls.Add(this.topButton);
            this.splitContainer1.Panel1.Controls.Add(this.downButton);
            this.splitContainer1.Panel1.Controls.Add(this.upButton);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dictBTreeView);
            this.splitContainer1.Panel2.Controls.Add(this.cancelButton);
            this.splitContainer1.Panel2.Controls.Add(this.okButton);
            this.splitContainer1.Size = new System.Drawing.Size(628, 637);
            this.splitContainer1.SplitterDistance = 314;
            this.splitContainer1.TabIndex = 16;
            // 
            // clearButton
            // 
            this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.clearButton.Location = new System.Drawing.Point(206, 154);
            this.clearButton.Margin = new System.Windows.Forms.Padding(4);
            this.clearButton.Name = "clearButton";
            this.clearButton.Size = new System.Drawing.Size(100, 31);
            this.clearButton.TabIndex = 1;
            this.clearButton.Text = "Clear";
            this.clearButton.UseVisualStyleBackColor = true;
            this.clearButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addAllbutton
            // 
            this.addAllbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addAllbutton.Location = new System.Drawing.Point(207, 107);
            this.addAllbutton.Margin = new System.Windows.Forms.Padding(4);
            this.addAllbutton.Name = "addAllbutton";
            this.addAllbutton.Size = new System.Drawing.Size(100, 31);
            this.addAllbutton.TabIndex = 1;
            this.addAllbutton.Text = "Add All";
            this.addAllbutton.UseVisualStyleBackColor = true;
            this.addAllbutton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // addSwitchbutton
            // 
            this.addSwitchbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.addSwitchbutton.Location = new System.Drawing.Point(207, 60);
            this.addSwitchbutton.Margin = new System.Windows.Forms.Padding(4);
            this.addSwitchbutton.Name = "addSwitchbutton";
            this.addSwitchbutton.Size = new System.Drawing.Size(100, 31);
            this.addSwitchbutton.TabIndex = 1;
            this.addSwitchbutton.Text = "Switch";
            this.addSwitchbutton.UseVisualStyleBackColor = true;
            this.addSwitchbutton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // BottomButton
            // 
            this.BottomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BottomButton.Location = new System.Drawing.Point(207, 389);
            this.BottomButton.Margin = new System.Windows.Forms.Padding(4);
            this.BottomButton.Name = "BottomButton";
            this.BottomButton.Size = new System.Drawing.Size(100, 31);
            this.BottomButton.TabIndex = 1;
            this.BottomButton.Text = "Bottom";
            this.BottomButton.UseVisualStyleBackColor = true;
            this.BottomButton.Click += new System.EventHandler(this.BottomButton_Click);
            // 
            // topButton
            // 
            this.topButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.topButton.Location = new System.Drawing.Point(207, 248);
            this.topButton.Margin = new System.Windows.Forms.Padding(4);
            this.topButton.Name = "topButton";
            this.topButton.Size = new System.Drawing.Size(100, 31);
            this.topButton.TabIndex = 1;
            this.topButton.Text = "Top";
            this.topButton.UseVisualStyleBackColor = true;
            this.topButton.Click += new System.EventHandler(this.topButton_Click);
            // 
            // dictBTreeView
            // 
            this.dictBTreeView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dictBTreeView.Location = new System.Drawing.Point(0, 0);
            this.dictBTreeView.Name = "dictBTreeView";
            this.dictBTreeView.Size = new System.Drawing.Size(200, 636);
            this.dictBTreeView.TabIndex = 16;
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

        private DllLolly.LollyTreeView dictATreeView;
        private System.Windows.Forms.Button addCollectionButton;
        private System.Windows.Forms.Button removeButton;
        private System.Windows.Forms.Button upButton;
        private System.Windows.Forms.Button downButton;
        private VistaButtonTest.VistaButton cancelButton;
        private VistaButtonTest.VistaButton okButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button BottomButton;
        private System.Windows.Forms.Button topButton;
        private System.Windows.Forms.TreeView dictBTreeView;
        private SharedImageLists1 sharedImageLists11;
        private System.Windows.Forms.Button clearButton;
        private System.Windows.Forms.Button addAllbutton;
        private System.Windows.Forms.Button addSwitchbutton;
    }
}