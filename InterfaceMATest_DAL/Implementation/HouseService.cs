using Geolocation;
using InterfaceMATest_DAL.Interface;
using InterfaceMATest_Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InterfaceMATest_DAL.Implementation
{
    public class HouseService : IHouseService
    {
        private readonly IBaseHouseService _baseService;
        public HouseService(IBaseHouseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<IEnumerable<House>> GetAtleastGivenNumberOfRoomsHouses(int rooms)
        {
            var houses = await _baseService.GetHouses();
            return houses?.Houses?.Where(x => x.Params?.Rooms >= rooms).OrderBy(x=>x.Params.Rooms);
        }

        

        public async Task<IEnumerable<House>> GetMissingInfoHouses()
        {
            var houses = await _baseService.GetHouses();
            return houses?.Houses?.Where(x => x.Params == null || x.Params.Rooms == null || x.Params.Value == null).OrderBy(x => x.Street);
        }

        public async Task<IEnumerable<House>> GetMoreThanFiveRoomsHouses()
        {
            return await GetAtleastGivenNumberOfRoomsHouses(5);
        }

        public async Task<IEnumerable<House>> GetShortestDistance(Coordinates coordinates)
        {
            var houses = await _baseService.GetHouses();
            var result = houses.Houses.OrderBy(x => GeoCalculator.GetDistance(coordinates.Lat, coordinates.Lon, x.Coords.Lat, x.Coords.Lon));
            return result;
        }

        public async Task<House> GetMoveIntoNewHouse(Coordinates coordinates, int rooms, int price)
        {
            var houses = await _baseService.GetHouses();
            var result = houses.Houses.Where(x=> x.Params?.Rooms >= rooms && x.Params?.Value <= price).OrderBy(x => GeoCalculator.GetDistance(coordinates.Lat, coordinates.Lon, x.Coords.Lat, x.Coords.Lon)).FirstOrDefault();
            return result;
        }
    }
}
