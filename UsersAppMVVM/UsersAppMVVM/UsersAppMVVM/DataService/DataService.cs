using System.Net.Http;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace UsersAppMVVM
{
    public class DataService
    {
        readonly HttpClient client = new HttpClient();

        public async Task<string> GetUsers()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    HttpResponseMessage response = await client.GetAsync("https://randomuser.me/api/?results=1000&inc=name,email,phone,picture");
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    return "network_error";
                }
            }
            catch
            {
                return "request_error";
            }
        }
    }
}
