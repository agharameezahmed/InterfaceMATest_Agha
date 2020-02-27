using InterfaceMATest_DAL.Interface;
using InterfaceMATest_Domain.Response;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace InterfaceMATest_DAL.Implementation
{
    public class BaseHouseService : IBaseHouseService
    {
        private readonly HttpClient _httpClient;
        public BaseHouseService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<BaseHouseServiceResponse> GetHouses()
        {
            var responseString = await _httpClient.GetStringAsync("https://demo.interfacema.de/programming-assessment-1.0/buildings");

            var houseCatalog = JsonConvert.DeserializeObject<BaseHouseServiceResponse>(responseString);
            return houseCatalog;
        }
    }
}
