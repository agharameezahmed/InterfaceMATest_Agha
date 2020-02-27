using InterfaceMATest_DAL.Implementation;
using InterfaceMATest_DAL.Interface;
using InterfaceMATest_Domain.Models;
using InterfaceMATest_Domain.Response;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace InterfaceMATest_Agha_Tests.DAL
{
    public class HouseServiceTests
    {
        private Mock<IBaseHouseService> _baseHouseServiceMock;

        [Fact]
        public async void GetMoreThanFiveRoomsHouses()
        {
            // Arrange
            InitializeData();

            var houseService = new HouseService(_baseHouseServiceMock.Object);

            // Act
            var result = (IOrderedEnumerable<House>) await houseService.GetMoreThanFiveRoomsHouses();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async void GetMissingInfoHouses()
        {
            // Arrange
            InitializeData();

            var houseService = new HouseService(_baseHouseServiceMock.Object);

            // Act
            var result = (IOrderedEnumerable<House>)await houseService.GetMissingInfoHouses();

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
        }

        [Fact]
        public async void GetShortestDistance()
        {
            // Arrange
            InitializeData();

            var houseService = new HouseService(_baseHouseServiceMock.Object);

            // Act
            var result = (IOrderedEnumerable<House>)await houseService.GetShortestDistance(new Coordinates { Lat = 53.5013632, Lon = 12.4174913 });

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Danziger Straße 66", result.First().Street); // nearest
            Assert.Equal("Adalbertstraße 13", result.Last().Street); //Farther
        }

        [Fact]
        public async void GetMoveIntoNewHouse()
        {
            // Arrange
            InitializeData();

            var houseService = new HouseService(_baseHouseServiceMock.Object);

            // Act
            var result = await houseService.GetMoveIntoNewHouse(new Coordinates { Lat = 53.5013632, Lon = 12.4174913 }, 10, 10000000);

            // Assert
            Assert.NotNull(result);

            Assert.Equal("Danziger Straße 66", result.Street); 
        }

        private void InitializeData()
        {
            _baseHouseServiceMock = new Mock<IBaseHouseService>();
            _baseHouseServiceMock.Setup(x => x.GetHouses()).Returns(Task.FromResult(new BaseHouseServiceResponse
            {
                Houses = HousesMockData
            }));
        }
        private IEnumerable<House> HousesMockData => new List<House>
        {
            new House
            {
                Coords = new Coordinates{Lat= 52.5013632, Lon = 13.4174913},
                Params = new Parameters { Rooms=5, Value= 1000000 },
                Street = "Adalbertstraße 13"
            },
            new House
            {
                Coords = new Coordinates{Lat= 52.4888151, Lon = 13.3147011},
                Params = new Parameters { Value= 1000000 },
                Street = "Brandenburgische Straße 10"
            },
            new House
            {
                Coords = new Coordinates{Lat= 52.53931, Lon = 13.4206011},
                Params = new Parameters { Rooms = 12, Value= 5000000 },
                Street = "Danziger Straße 66"
            }
        };
    
    }
}
