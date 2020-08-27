using System.Threading.Tasks;

namespace LollyCloud
{
    public interface ILollySettings
    {
        Task OnSettingsChanged();
    }
}
