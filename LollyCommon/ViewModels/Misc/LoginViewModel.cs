using System.Linq;
using System.Reactive.Linq;
using System.Threading.Tasks;

namespace LollyCommon
{
    public class LoginViewModel : MUser
    {
        UserDataStore userDS = new UserDataStore();
        public async Task<string> Login()
        {
            var lst = await userDS.GetData(USERNAME, PASSWORD);
            return lst.IsEmpty() ? "" : lst[0].USERID;
        }
    }
}
