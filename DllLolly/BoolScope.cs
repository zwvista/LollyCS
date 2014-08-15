using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lolly
{
    public class BoolRef
    {
        public bool Value { get; set; }
        public BoolRef(bool v)
        {
            Value = v;
        }
    }

    public class BoolScope : IDisposable
    {
        private BoolRef br;
        public BoolScope(BoolRef br)
        {
            br.Value = !br.Value;
            this.br = br;
        }
        
        #region IDisposable Members

        public void Dispose()
        {
            br.Value = !br.Value;
        }

        #endregion
    }
}
