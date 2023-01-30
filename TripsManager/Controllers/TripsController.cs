using TripsManager.Models;
using TripsManager.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.Data.SqlClient;
using TripsManager.Models.DTO.Responses;
using Microsoft.EntityFrameworkCore;
using TripsManager.Models.DTO.Requests;
using System.Net.Sockets;

namespace TripsController.Controllers
{
    [Route("api/trips")]
    [ApiController]
    public class TripsController : ControllerBase
    {

        private IDatabaseService _databaseService;

        public TripsController(IDatabaseService databaseService)
        {
            _databaseService = databaseService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTripsAsync()
        {
            var result = await _databaseService.GetTripsAsync();
            return Ok(result.Output);
        }

        [Route("/api/clients/{idClient}")]
        [HttpDelete]
        public async Task<IActionResult> DeleteClientAsync(int idClient)
        {
            DatabaseResponseDto result;
            try
            {
                result = await _databaseService.DeleteClientAsync(idClient);
            }
            catch (SqlException exc)
            {
                return StatusCode(500, exc.Message);
            }

            return StatusCode(result.StatusCode, result.Message);

        }

        [HttpPost("/api/trips/{idTrip}/clients")]
        public async Task<IActionResult> AssignClientToTripAsync(int idTrip, [FromBody] AssignClientToTripRequestDto assignClientToTripRequestDto)
        {

            DatabaseResponseDto result;
            try
            {
                result = await _databaseService.AssignClientToTripAsync(idTrip, assignClientToTripRequestDto);
            }
            catch (SqlException exc)
            {
                return StatusCode(500, exc.Message);
            }

            return StatusCode(result.StatusCode, result.Message);

        }

    }
}
