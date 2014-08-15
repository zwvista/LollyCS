using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Lolly
{
    class Options
    {
        public Color WordLevelP3BackColor { get; set; }
        public Color WordLevelP2BackColor { get; set; }
        public Color WordLevelP1BackColor { get; set; }
        public Color WordLevelN1BackColor { get; set; }
        public Color WordLevelN2BackColor { get; set; }
        public Color WordLevelN3BackColor { get; set; }

        public Options()
        {
            WordLevelP3BackColor = Color.Gray;
            WordLevelP2BackColor = Color.Teal;
            WordLevelP1BackColor = Color.Green;
            WordLevelN1BackColor = Color.DarkOrange;
            WordLevelN2BackColor = Color.Fuchsia;
            WordLevelN3BackColor = Color.Red;
        }
    }
}
