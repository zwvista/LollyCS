//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LollyBase
{
    using System;
    using System.Collections.Generic;
    
    public partial class MLANGUAGE
    {
        public MLANGUAGE()
        {
            this.BOOKS = new HashSet<MBOOK>();
            this.PICBOOKS = new HashSet<MPICBOOK>();
            this.WORDSLANG = new HashSet<MWORDLANG>();
            this.AUTOCORRECT = new HashSet<MAUTOCORRECT>();
        }
    
        public int LANGID { get; set; }
        public string CHNNAME { get; set; }
        public string VOICE { get; set; }
        public Nullable<int> CURBOOKID { get; set; }
        public string LANGNAME { get; set; }
    
        public virtual ICollection<MBOOK> BOOKS { get; set; }
        public virtual ICollection<MPICBOOK> PICBOOKS { get; set; }
        public virtual ICollection<MWORDLANG> WORDSLANG { get; set; }
        public virtual ICollection<MAUTOCORRECT> AUTOCORRECT { get; set; }
    }
}
