module MainForm

open System
open System.Windows.Forms
open System.Drawing
open System.Web
open LollyShared

type MainForm() as this =
    inherit Form()
    do this.InitializeComponent()

    [<DefaultValue>]val mutable private components: System.ComponentModel.Container;
    override this.Dispose(disposing) =
        if (disposing && (match this.components with null -> false | _ -> true)) then
            this.components.Dispose();
        base.Dispose(disposing)

    member private this.InitializeComponent() =
        this.components <- new System.ComponentModel.Container();
        this.mLANGUAGEBindingSource <- new System.Windows.Forms.BindingSource(this.components);
        this.mDICTALLBindingSource <- new System.Windows.Forms.BindingSource(this.components);
        this.panel1 <- new System.Windows.Forms.Panel();
        this.searchButton <- new System.Windows.Forms.Button();
        this.wordTextBox <- new System.Windows.Forms.TextBox();
        this.label1 <- new System.Windows.Forms.Label();
        this.dictWebBrowser <- new System.Windows.Forms.WebBrowser();
        this.panel2 <- new System.Windows.Forms.Panel();
        this.langComboBox <- new System.Windows.Forms.ComboBox();
        this.dictComboBox <- new System.Windows.Forms.ComboBox();
        this.label2 <- new System.Windows.Forms.Label();
        this.label3 <- new System.Windows.Forms.Label();
        ((this.mLANGUAGEBindingSource) :> (System.ComponentModel.ISupportInitialize)).BeginInit();
        ((this.mDICTALLBindingSource) :> (System.ComponentModel.ISupportInitialize)).BeginInit();
        this.panel1.SuspendLayout();
        this.panel2.SuspendLayout();
        this.SuspendLayout();
        // 
        // mLANGUAGEBindingSource
        // 
        this.mLANGUAGEBindingSource.DataSource <- typedefof<LollyShared.MLANGUAGE>;
        // 
        // mDICTALLBindingSource
        // 
        this.mDICTALLBindingSource.DataSource <- typedefof<LollyShared.MDICTALL>;
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
        this.searchButton.Click.AddHandler(new System.EventHandler(this.searchButton_Click));
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
        this.dictWebBrowser.Size <- new System.Drawing.Size(657, 364);
        this.dictWebBrowser.TabIndex <- 2;
        // 
        // panel2
        // 
        this.panel2.Controls.Add(this.langComboBox);
        this.panel2.Controls.Add(this.dictComboBox);
        this.panel2.Controls.Add(this.label2);
        this.panel2.Controls.Add(this.label3);
        this.panel2.Dock <- System.Windows.Forms.DockStyle.Bottom;
        this.panel2.Location <- new System.Drawing.Point(0, 360);
        this.panel2.Name <- "panel2";
        this.panel2.Size <- new System.Drawing.Size(657, 37);
        this.panel2.TabIndex <- 3;
        // 
        // langComboBox
        // 
        this.langComboBox.DataSource <- this.mLANGUAGEBindingSource;
        this.langComboBox.DisplayMember <- "LANGNAME";
        this.langComboBox.DropDownStyle <- System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.langComboBox.FormattingEnabled <- true;
        this.langComboBox.Location <- new System.Drawing.Point(87, 5);
        this.langComboBox.Margin <- new System.Windows.Forms.Padding(0, 4, 0, 4);
        this.langComboBox.Name <- "langComboBox";
        this.langComboBox.Size <- new System.Drawing.Size(180, 24);
        this.langComboBox.TabIndex <- 6;
        this.langComboBox.ValueMember <- "LANGID";
        this.langComboBox.SelectionChangeCommitted.AddHandler(new System.EventHandler(this.langComboBox_SelectionChangeCommitted));
        // 
        // dictComboBox
        // 
        this.dictComboBox.DataSource <- this.mDICTALLBindingSource;
        this.dictComboBox.DisplayMember <- "DICTNAME";
        this.dictComboBox.DropDownStyle <- System.Windows.Forms.ComboBoxStyle.DropDownList;
        this.dictComboBox.FormattingEnabled <- true;
        this.dictComboBox.Location <- new System.Drawing.Point(364, 5);
        this.dictComboBox.Margin <- new System.Windows.Forms.Padding(4);
        this.dictComboBox.Name <- "dictComboBox";
        this.dictComboBox.Size <- new System.Drawing.Size(180, 24);
        this.dictComboBox.TabIndex <- 7;
        this.dictComboBox.ValueMember <- "URL";
        // 
        // label2
        // 
        this.label2.AutoSize <- true;
        this.label2.Location <- new System.Drawing.Point(5, 6);
        this.label2.Margin <- new System.Windows.Forms.Padding(4, 0, 4, 0);
        this.label2.Name <- "label2";
        this.label2.Size <- new System.Drawing.Size(74, 16);
        this.label2.TabIndex <- 4;
        this.label2.Text <- "Language:";
        // 
        // label3
        // 
        this.label3.AutoSize <- true;
        this.label3.Location <- new System.Drawing.Point(277, 6);
        this.label3.Margin <- new System.Windows.Forms.Padding(4, 8, 4, 4);
        this.label3.Name <- "label3";
        this.label3.Size <- new System.Drawing.Size(79, 16);
        this.label3.TabIndex <- 5;
        this.label3.Text <- "Dictionary:";
        // 
        // MainForm
        // 
        this.AutoScaleDimensions <- new System.Drawing.SizeF(9.F, 16.F);
        this.AutoScaleMode <- System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize <- new System.Drawing.Size(657, 397);
        this.Controls.Add(this.panel2);
        this.Controls.Add(this.dictWebBrowser);
        this.Controls.Add(this.panel1);
        this.Font <- new System.Drawing.Font("MS UI Gothic", 12.F);
        this.Margin <- new System.Windows.Forms.Padding(4);
        this.Name <- "MainForm";
        this.StartPosition <- System.Windows.Forms.FormStartPosition.CenterScreen;
        this.Text <- "Form1";
        this.Load.AddHandler(new System.EventHandler(this.MainForm_Load));
        ((this.mLANGUAGEBindingSource) :> (System.ComponentModel.ISupportInitialize)).EndInit();
        ((this.mDICTALLBindingSource) :> (System.ComponentModel.ISupportInitialize)).EndInit();
        this.panel1.ResumeLayout(false);
        this.panel1.PerformLayout();
        this.panel2.ResumeLayout(false);
        this.panel2.PerformLayout();
        this.ResumeLayout(false);

    [<DefaultValue>]val mutable private panel1 : System.Windows.Forms.Panel;
    [<DefaultValue>]val mutable private wordTextBox : System.Windows.Forms.TextBox;
    [<DefaultValue>]val mutable private label1 : System.Windows.Forms.Label;
    [<DefaultValue>]val mutable private dictWebBrowser : System.Windows.Forms.WebBrowser;
    [<DefaultValue>]val mutable private mLANGUAGEBindingSource : System.Windows.Forms.BindingSource;
    [<DefaultValue>]val mutable private mDICTALLBindingSource : System.Windows.Forms.BindingSource;
    [<DefaultValue>]val mutable private searchButton : System.Windows.Forms.Button;
    [<DefaultValue>]val mutable private panel2 : System.Windows.Forms.Panel;
    [<DefaultValue>]val mutable private langComboBox : System.Windows.Forms.ComboBox;
    [<DefaultValue>]val mutable private dictComboBox : System.Windows.Forms.ComboBox;
    [<DefaultValue>]val mutable private label2 : System.Windows.Forms.Label;
    [<DefaultValue>]val mutable private label3 : System.Windows.Forms.Label;

    member private this.MainForm_Load sender e =
        this.mLANGUAGEBindingSource.DataSource <- LollyDB.Languages_GetDataNonChinese();
        this.langComboBox_SelectionChangeCommitted(null, null) |> ignore;

    member private this.langComboBox_SelectionChangeCommitted sender e =
        this.mDICTALLBindingSource.DataSource <- LollyDB.DictAll_GetDataByLang(this.langComboBox.SelectedValue :?> int64);

    member private this.searchButton_Click sender e =
        let row = this.mDICTALLBindingSource.Current :?> MDICTALL;
        let url = System.String.Format(row.URL, HttpUtility.UrlEncode(this.wordTextBox.Text));
        this.dictWebBrowser.Navigate(url);
