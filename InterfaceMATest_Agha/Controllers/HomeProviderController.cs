using InterfaceMATest_DAL.Interface;
using InterfaceMATest_Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

namespace InterfaceMATest_Agha.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeProviderController : ControllerBase
    {
        public readonly IHouseService _houseService;
        public readonly IBaseHouseService _baseHouseService;

        public HomeProviderController(IHouseService houseService,
            IBaseHouseService baseHouseService
            )
        {
            _houseService = houseService;
            _baseHouseService = baseHouseService;
        }
        /// <summary>
        /// Returns all houses
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {
                var model = await _baseHouseService.GetHouses();
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// return a list of houses w hich have more then 5 rooms. Start with the low est number of rooms.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFiveRoomHouses")]
        public async Task<IActionResult> GetFiveRoomHouses()
        {
            try
            {
                var model = await _houseService.GetMoreThanFiveRoomsHouses();
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// return a list of houses that you do not have all the data for. Sort them by the street-name.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetIncompleteInfoHouses")]
        public async Task<IActionResult> GetIncompleteInfoHouses()
        {
            try
            {
                var model = await _houseService.GetMissingInfoHouses();
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
        /// <summary>
        /// return nearest houses from Eberswalder Straße 55 order by distance
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNearestHouse")]
        public async Task<IActionResult> GetNearestHouse()
        {
            try
            {
                var model = await _houseService.GetShortestDistance(new Coordinates { Lat = 52.5418174, Lon = 13.4062778 });
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }

        /// <summary>
        /// return a house has at least 10 rooms and does not cost more than 5.000.000 €
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMoveIntoNewHouse")]
        public async Task<IActionResult> GetMoveIntoNewHouse()
        {
            try
            {
                var model = await _houseService.GetMoveIntoNewHouse(new Coordinates { Lat = 52.5418174, Lon = 13.4062778 }, 10, 5000000);
                if (model == null)
                    return NotFound();
                return Ok(model);
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
