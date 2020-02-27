using InterfaceMATest_Domain.Response;
using System.Threading.Tasks;

namespace InterfaceMATest_DAL.Interface
{
    public interface IBaseHouseService
    {
        Task<BaseHouseServiceResponse> GetHouses();
    }
}
