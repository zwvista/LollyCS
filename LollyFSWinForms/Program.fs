module Program

open System
open System.Windows.Forms
open MainForm

[<STAThread>]
[<EntryPoint>]
let main argv = 
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    Application.Run(new MainForm())
    0
