//------------------------------------------------------------------------------
// <auto-generated>
//     このコードはテンプレートから生成されました。
//
//     このファイルを手動で変更すると、アプリケーションで予期しない動作が発生する可能性があります。
//     このファイルに対する手動の変更は、コードが再生成されると上書きされます。
// </auto-generated>
//------------------------------------------------------------------------------

namespace LollyShared
{
    using System;
    using System.Collections.Generic;
    
    public partial class MDICTIONARY
    {
        public long LANGID { get; set; }
        public long ORD { get; set; }
        public long DICTTYPEID { get; set; }
        public string DICTNAME { get; set; }
        public long LANGIDTO { get; set; }
        public string URL { get; set; }
        public string CHCONV { get; set; }
        public string AUTOMATION { get; set; }
        public long AUTOJUMP { get; set; }
        public string DICTTABLE { get; set; }
        public string TEMPLATE { get; set; }
    
        public virtual MDICTTYPE DICTTYPES { get; set; }
    }
}
