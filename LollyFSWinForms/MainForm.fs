module MainForm

open System
open System.Windows.Forms
open System.Drawing
open System.Web
open LollyShared

type MainForm() as this =
    inherit Form()
    do this.InitializeComponent()

    [<DefaultValue>]val mutable components: System.ComponentModel.Container;
    override this.Dispose(disposing) =
        if (disposing && (match this.components with null -> false | _ -> true)) then
            this.components.Dispose();
        base.Dispose(disposing)

    member this.InitializeComponent() =
        this.components <- new System.ComponentModel.Container();
        this.tableLayoutPanel1 <- new System.Windows.Forms.TableLayoutPanel();
        this.langComboBox <- new System.Windows.Forms.ComboBox();
        this.mLANGUAGEBindingSource <- new System.Windows.Forms.BindingSource(this.components);
        this.dictComboBox <- new System.Windows.Forms.ComboBox();
        this.mDICTALLBindingSource <- new System.Windows.Forms.BindingSource(this.components);
        this.label2 <- new System.Windows.Forms.Label();
        this.label3 <- new System.Windows.Forms.Label();
        this.panel1 <- new System.Windows.Forms.Panel();
        this.searchButton <- new System.Windows.Forms.Button();
        this.wordTextBox <- new System.Windows.Forms.TextBox();
        this.label1 <- new System.Windows.Forms.Label();
        this.dictWebBrowser <- new System.Windows.Forms.WebBrowser();
        this.tableLayoutPanel1.SuspendLayout();
        ((this.mLANGUAGEBindingSource) :> (System.ComponentModel.ISupportInitialize)).BeginInit();
        ((this.mDICTALLBindingSource) :> (System.ComponentModel.ISupportInitialize)).BeginInit();
        this.panel1.SuspendLayout();
        this.SuspendLayout();
        // 
        // tableLayoutPanel1
        // 
        this.tableLayoutPanel1.ColumnCount <- 4;
        ignore(this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()));
        ignore(this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f)));
        ignore(this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle()));
        ignore(this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0f)));
        this.tableLayoutPanel1.Controls.Add(this.langComboBox, 1, 0);
        this.tableLayoutPanel1.Controls.Add(this.dictComboBox, 3, 0);
        this.tableLayoutPanel1.Controls.Add(this.label2, 0, 0);
        this.tableLayoutPanel1.Controls.Add(this.label3, 2, 0);
        this.tableLayoutPanel1.Dock <- System.Windows.Forms.DockStyle.Bottom;
        this.tableLayoutPanel1.Location <- new System.Drawing.Point(0, 366);
        this.tableLayoutPanel1.Margin <- new System.Windows.Forms.Padding(4);
        this.tableLayoutPanel1.Name <- "tableLayoutPanel1";
        this.tableLayoutPanel1.RowCount <- 1;
        ignore(this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0f)));
        this.tableLayoutPanel1.Size <- new System.Drawing.Size(657, 31);
        this.tableLayoutPanel1.TabIndex <- 0;
        // 
        // langComboBox
        // 
        this.langComboBox.DataSource <- this.mLANGUAGEBindingSource;
        this.langComboBox.DisplayMember <- "LANGNAME";
        this.langComboBox.DropDownStyle <- System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.langComboBox.FormattingEnabled <- true;
        this.langComboBox.Location <- new System.Drawing.Point(86, 4);
        this.langComboBox.Margin <- new System.Windows.Forms.Padding(4);
        this.langComboBox.Name <- "langComboBox";
        this.langComboBox.Size <- new System.Drawing.Size(180, 24);
        this.langComboBox.TabIndex <- 2;
        this.langComboBox.ValueMember <- "LANGID";
        let langComboBox_SelectionChangeCommitted = (fun e ->
            this.mDICTALLBindingSource.DataSource <- LollyDB.DictAll_GetDataByLang(this.langComboBox.SelectedValue :?> int64);
        )
        this.langComboBox.SelectionChangeCommitted.Add(langComboBox_SelectionChangeCommitted);
        // 
        // mLANGUAGEBindingSource
        // 
        this.mLANGUAGEBindingSource.DataSource <- typedefof<LollyShared.MLANGUAGE>;
        // 
        // dictComboBox
        // 
        this.dictComboBox.DataSource <- this.mDICTALLBindingSource;
        this.dictComboBox.DisplayMember <- "DICTNAME";
        this.dictComboBox.DropDownStyle <- System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.dictComboBox.FormattingEnabled <- true;
        this.dictComboBox.Location <- new System.Drawing.Point(417, 4);
        this.dictComboBox.Margin <- new System.Windows.Forms.Padding(4);
        this.dictComboBox.Name <- "dictComboBox";
        this.dictComboBox.Size <- new System.Drawing.Size(180, 24);
        this.dictComboBox.TabIndex <- 3;
        this.dictComboBox.ValueMember <- "URL";
        // 
        // mDICTALLBindingSource
        // 
        this.mDICTALLBindingSource.DataSource <- typedefof<LollyShared.MDICTALL>;
        // 
        // label2
        // 
        this.label2.AutoSize <- true;
        this.label2.Location <- new System.Drawing.Point(4, 8);
        this.label2.Margin <- new System.Windows.Forms.Padding(4, 8, 4, 4);
        this.label2.Name <- "label2";
        this.label2.Size <- new System.Drawing.Size(74, 16);
        this.label2.TabIndex <- 0;
        this.label2.Text <- "Language:";
        // 
        // label3
        // 
        this.label3.AutoSize <- true;
        this.label3.Location <- new System.Drawing.Point(330, 8);
        this.label3.Margin <- new System.Windows.Forms.Padding(4, 8, 4, 4);
        this.label3.Name <- "label3";
        this.label3.Size <- new System.Drawing.Size(79, 16);
        this.label3.TabIndex <- 1;
        this.label3.Text <- "Dictionary:";
        // 
        // panel1
        // 
        this.panel1.Controls.Add(this.searchButton);
        this.panel1.Controls.Add(this.wordTextBox);
        this.panel1.Controls.Add(this.label1);
        this.panel1.Dock <- System.Windows.Forms.DockStyle.Top;
        this.panel1.Location <- new System.Drawing.Point(0, 0);
        this.panel1.Margin <- new System.Windows.Forms.Padding(4);
        this.panel1.Name <- "panel1";
        this.panel1.Size <- new System.Drawing.Size(657, 33);
        this.panel1.TabIndex <- 1;
        // 
        // searchButton
        // 
        this.searchButton.Location <- new System.Drawing.Point(570, 6);
        this.searchButton.Name <- "searchButton";
        this.searchButton.Size <- new System.Drawing.Size(75, 23);
        this.searchButton.TabIndex <- 2;
        this.searchButton.Text <- "Search";
        this.searchButton.UseVisualStyleBackColor <- true;
        this.searchButton.Click.Add(fun e ->
            let row = this.mDICTALLBindingSource.Current :?> MDICTALL;
            let url = System.String.Format(row.URL, HttpUtility.UrlEncode(this.wordTextBox.Text));
            this.dictWebBrowser.Navigate(url);
        );
        // 
        // wordTextBox
        // 
        this.wordTextBox.Location <- new System.Drawing.Point(58, 6);
        this.wordTextBox.Margin <- new System.Windows.Forms.Padding(4);
        this.wordTextBox.Name <- "wordTextBox";
        this.wordTextBox.Size <- new System.Drawing.Size(505, 23);
        this.wordTextBox.TabIndex <- 1;
        this.wordTextBox.Text <- "一人";
        // 
        // label1
        // 
        this.label1.AutoSize <- true;
        this.label1.Location <- new System.Drawing.Point(4, 8);
        this.label1.Margin <- new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.label1.Name <- "label1";
        this.label1.Size <- new System.Drawing.Size(45, 16);
        this.label1.TabIndex <- 0;
        this.label1.Text <- "Word:";
        // 
        // dictWebBrowser
        // 
        this.dictWebBrowser.Dock <- System.Windows.Forms.DockStyle.Fill;
        this.dictWebBrowser.Location <- new System.Drawing.Point(0, 33);
        this.dictWebBrowser.Margin <- new System.Windows.Forms.Padding(4);
        this.dictWebBrowser.MinimumSize <- new System.Drawing.Size(30, 27);
        this.dictWebBrowser.Name <- "dictWebBrowser";
        this.dictWebBrowser.ScriptErrorsSuppressed <- true;
        this.dictWebBrowser.Size <- new System.Drawing.Size(657, 333);
        this.dictWebBrowser.TabIndex <- 2;
        // 
        // MainForm
        // 
        this.AutoScaleDimensions <- new System.Drawing.SizeF(9.0f, 16.0f);
        this.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize <- new System.Drawing.Size(657, 397);
        this.Controls.Add(this.dictWebBrowser);
        this.Controls.Add(this.panel1);
        this.Controls.Add(this.tableLayoutPanel1);
        this.Font <- new System.Drawing.Font("MS UI Gothic", 12.0f);
        this.Margin <- new System.Windows.Forms.Padding(4)
        this.Name <- "MainForm";
        this.StartPosition <- System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text <- "Form1";
        this.Load.Add(fun _ ->
            this.mLANGUAGEBindingSource.DataSource <- LollyDB.Languages_GetDataNonChinese();
            langComboBox_SelectionChangeCommitted(null);
        )
        this.tableLayoutPanel1.ResumeLayout(false);
        this.tableLayoutPanel1.PerformLayout();
        ((this.mLANGUAGEBindingSource) :> (System.ComponentModel.ISupportInitialize)).EndInit();
        ((this.mDICTALLBindingSource) :> (System.ComponentModel.ISupportInitialize)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.ResumeLayout(false);

    [<DefaultValue>]val mutable tableLayoutPanel1 : System.Windows.Forms.TableLayoutPanel
    [<DefaultValue>]val mutable panel1 : System.Windows.Forms.Panel
    [<DefaultValue>]val mutable wordTextBox : System.Windows.Forms.TextBox
    [<DefaultValue>]val mutable label1 : System.Windows.Forms.Label
    [<DefaultValue>]val mutable dictWebBrowser : System.Windows.Forms.WebBrowser
    [<DefaultValue>]val mutable label2 : System.Windows.Forms.Label
    [<DefaultValue>]val mutable label3 : System.Windows.Forms.Label
    [<DefaultValue>]val mutable langComboBox : System.Windows.Forms.ComboBox
    [<DefaultValue>]val mutable dictComboBox : System.Windows.Forms.ComboBox
    [<DefaultValue>]val mutable mLANGUAGEBindingSource : System.Windows.Forms.BindingSource
    [<DefaultValue>]val mutable mDICTALLBindingSource : System.Windows.Forms.BindingSource
    [<DefaultValue>]val mutable searchButton : System.Windows.Forms.Button
