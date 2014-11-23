namespace Lolly
{
  partial class SharedImageLists1 : ForwardSoftware.Windows.Forms.SharedImageLists
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

    #region Component Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SharedImageLists1));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Silver;
            this.imageList1.Images.SetKeyName(0, "Offline0");
            this.imageList1.Images.SetKeyName(1, "Offline1");
            this.imageList1.Images.SetKeyName(2, "Offline2");
            this.imageList1.Images.SetKeyName(3, "Offline3");
            this.imageList1.Images.SetKeyName(4, "Offline4");
            this.imageList1.Images.SetKeyName(5, "Offline5");
            this.imageList1.Images.SetKeyName(6, "Offline6");
            this.imageList1.Images.SetKeyName(7, "Offline7");
            this.imageList1.Images.SetKeyName(8, "Offline8");
            this.imageList1.Images.SetKeyName(9, "Offline9");
            this.imageList1.Images.SetKeyName(10, "Online0");
            this.imageList1.Images.SetKeyName(11, "Online1");
            this.imageList1.Images.SetKeyName(12, "Online2");
            this.imageList1.Images.SetKeyName(13, "Online3");
            this.imageList1.Images.SetKeyName(14, "Online4");
            this.imageList1.Images.SetKeyName(15, "Online5");
            this.imageList1.Images.SetKeyName(16, "Online6");
            this.imageList1.Images.SetKeyName(17, "Online7");
            this.imageList1.Images.SetKeyName(18, "Online8");
            this.imageList1.Images.SetKeyName(19, "Online9");
            this.imageList1.Images.SetKeyName(20, "Live0");
            this.imageList1.Images.SetKeyName(21, "Live1");
            this.imageList1.Images.SetKeyName(22, "Live2");
            this.imageList1.Images.SetKeyName(23, "Live3");
            this.imageList1.Images.SetKeyName(24, "Live4");
            this.imageList1.Images.SetKeyName(25, "Live5");
            this.imageList1.Images.SetKeyName(26, "Live6");
            this.imageList1.Images.SetKeyName(27, "Live7");
            this.imageList1.Images.SetKeyName(28, "Live8");
            this.imageList1.Images.SetKeyName(29, "Live9");
            this.imageList1.Images.SetKeyName(30, "Custom");
            this.imageList1.Images.SetKeyName(31, "Local");
            this.imageList1.Images.SetKeyName(32, "Special");
            this.imageList1.Images.SetKeyName(33, "Conjugator");
            this.imageList1.Images.SetKeyName(34, "Web");

    }

    #endregion

    static ForwardSoftware.Windows.Forms.SharedImageLists sharedImageLists;

    [System.Diagnostics.DebuggerNonUserCode()]
    static SharedImageLists1()
    {
      sharedImageLists = new SharedImageLists1();
    }
    /// This Method returns the SharedImageLists1 component that is used to share
    /// the ImageLists.
    [System.Diagnostics.DebuggerNonUserCode()]
    public override ForwardSoftware.Windows.Forms.SharedImageLists GetSharedImageLists()
    {
      return sharedImageLists;
    }
    public System.Windows.Forms.ImageList imageList1;
  }
}
