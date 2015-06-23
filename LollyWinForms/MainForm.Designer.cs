namespace LollyWinForms
{
    partial class MainForm
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mLANGUAGEBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.mDICTALLBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.searchButton = new System.Windows.Forms.Button();
            this.wordTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dictWebBrowser = new System.Windows.Forms.WebBrowser();
            this.panel2 = new System.Windows.Forms.Panel();
            this.langComboBox = new System.Windows.Forms.ComboBox();
            this.dictComboBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mLANGUAGEBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDICTALLBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mLANGUAGEBindingSource
            // 
            this.mLANGUAGEBindingSource.DataSource = typeof(LollyShared.MLANGUAGE);
            // 
            // mDICTALLBindingSource
            // 
            this.mDICTALLBindingSource.DataSource = typeof(LollyShared.MDICTALL);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.searchButton);
            this.panel1.Controls.Add(this.wordTextBox);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(657, 33);
            this.panel1.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(570, 6);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(75, 23);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "Search";
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // wordTextBox
            // 
            this.wordTextBox.Location = new System.Drawing.Point(58, 6);
            this.wordTextBox.Margin = new System.Windows.Forms.Padding(4);
            this.wordTextBox.Name = "wordTextBox";
            this.wordTextBox.Size = new System.Drawing.Size(505, 23);
            this.wordTextBox.TabIndex = 1;
            this.wordTextBox.Text = "一人";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Word:";
            // 
            // dictWebBrowser
            // 
            this.dictWebBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dictWebBrowser.Location = new System.Drawing.Point(0, 33);
            this.dictWebBrowser.Margin = new System.Windows.Forms.Padding(4);
            this.dictWebBrowser.MinimumSize = new System.Drawing.Size(30, 27);
            this.dictWebBrowser.Name = "dictWebBrowser";
            this.dictWebBrowser.ScriptErrorsSuppressed = true;
            this.dictWebBrowser.Size = new System.Drawing.Size(657, 364);
            this.dictWebBrowser.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.langComboBox);
            this.panel2.Controls.Add(this.dictComboBox);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 360);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(657, 37);
            this.panel2.TabIndex = 3;
            // 
            // langComboBox
            // 
            this.langComboBox.DataSource = this.mLANGUAGEBindingSource;
            this.langComboBox.DisplayMember = "LANGNAME";
            this.langComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.langComboBox.FormattingEnabled = true;
            this.langComboBox.Location = new System.Drawing.Point(87, 5);
            this.langComboBox.Margin = new System.Windows.Forms.Padding(0, 4, 0, 4);
            this.langComboBox.Name = "langComboBox";
            this.langComboBox.Size = new System.Drawing.Size(180, 24);
            this.langComboBox.TabIndex = 6;
            this.langComboBox.ValueMember = "LANGID";
            this.langComboBox.SelectionChangeCommitted += new System.EventHandler(this.langComboBox_SelectionChangeCommitted);
            // 
            // dictComboBox
            // 
            this.dictComboBox.DataSource = this.mDICTALLBindingSource;
            this.dictComboBox.DisplayMember = "DICTNAME";
            this.dictComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dictComboBox.FormattingEnabled = true;
            this.dictComboBox.Location = new System.Drawing.Point(364, 5);
            this.dictComboBox.Margin = new System.Windows.Forms.Padding(4);
            this.dictComboBox.Name = "dictComboBox";
            this.dictComboBox.Size = new System.Drawing.Size(180, 24);
            this.dictComboBox.TabIndex = 7;
            this.dictComboBox.ValueMember = "URL";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 6);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 16);
            this.label2.TabIndex = 4;
            this.label2.Text = "Language:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(277, 6);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 8, 4, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dictionary:";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(657, 397);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.dictWebBrowser);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("MS UI Gothic", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.MainForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.mLANGUAGEBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDICTALLBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox wordTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.WebBrowser dictWebBrowser;
        private System.Windows.Forms.BindingSource mLANGUAGEBindingSource;
        private System.Windows.Forms.BindingSource mDICTALLBindingSource;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox langComboBox;
        private System.Windows.Forms.ComboBox dictComboBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}

