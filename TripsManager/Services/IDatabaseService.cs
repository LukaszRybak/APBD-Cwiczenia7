namespace TripsManager.Services;

using Microsoft.AspNetCore.Mvc;
using TripsManager.Models;
using TripsManager.Models.DTO.Requests;
using TripsManager.Models.DTO.Responses;

public interface IDatabaseService
{
    Task<DatabaseResponseDto> GetTripsAsync();
    Task<DatabaseResponseDto> DeleteClientAsync(int idClient);
    Task<DatabaseResponseDto> AssignClientToTripAsync(int idTrip, AssignClientToTripRequestDto assignClientToTripRequestDto);
}


