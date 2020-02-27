using InterfaceMATest_Agha.Controllers;
using InterfaceMATest_DAL.Interface;
using InterfaceMATest_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using System.Threading.Tasks;
using Xunit;

namespace InterfaceMATest_Agha_Tests.API
{
    public class HomeProviderControllerTest
    {
        private Mock<IBaseHouseService> _baseHouseServiceMock;
        private Mock<IHouseService> _houseServiceMock;


        [Fact]
        public async void GetMoveIntoNewHouse_Failed()
        {
            // Arrange
            InitializeData();

            var homeController = new HomeProviderController(_houseServiceMock.Object, _baseHouseServiceMock.Object);

            // Act
            var result = await homeController.GetMoveIntoNewHouse() as ObjectResult;

            // Assert
            Assert.NotNull(result);

            Assert.Equal((int) HttpStatusCode.NotFound, result.StatusCode);
        }

        [Fact]
        public async void GetMoveIntoNewHouse_Success()
        {
            // Arrange
            InitializeData();
            _houseServiceMock.Setup(x => x.GetMoveIntoNewHouse(It.IsAny<Coordinates>(), It.IsAny<int>(), It.IsAny<int>())).Returns(
                Task.FromResult(new House { Street = "Test Street", Coords = new Coordinates { Lat = 123, Lon = 735 }, Params = null }));
            var homeController = new HomeProviderController(_houseServiceMock.Object, _baseHouseServiceMock.Object);

            // Act
            var result = await homeController.GetMoveIntoNewHouse() as ObjectResult;

            // Assert
            Assert.NotNull(result.Value);
            Assert.Equal((int)HttpStatusCode.OK, result.StatusCode);
        }

        private void InitializeData()
        {
            _houseServiceMock = new Mock<IHouseService>();
            _baseHouseServiceMock = new Mock<IBaseHouseService>();
        }
     

    }
}
