using System.Threading.Tasks;

namespace LollyCommon
{
    public interface ILollySettings
    {
        Task OnSettingsChanged();
    }
}
