using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LollyCloud
{
    public interface ILollySettings
    {
        Task OnSettingsChanged();
    }
}
