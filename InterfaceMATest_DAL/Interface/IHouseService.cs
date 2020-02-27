using InterfaceMATest_Domain.Models;
using InterfaceMATest_Domain.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InterfaceMATest_DAL.Interface
{
    public interface IHouseService
    {
        Task<IEnumerable<House>> GetShortestDistance(Coordinates coordinates);
        Task<IEnumerable<House>> GetMoreThanFiveRoomsHouses();
        Task<IEnumerable<House>> GetAtleastGivenNumberOfRoomsHouses(int rooms);
        Task<IEnumerable<House>> GetMissingInfoHouses();
        Task<House> GetMoveIntoNewHouse(Coordinates coordinates, int rooms, int price);
    }
}
