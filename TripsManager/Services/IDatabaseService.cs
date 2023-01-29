namespace TripsManager.Services;
using TripsManager.Models;
using TripsManager.Models.DTO.Responses;

public interface IDatabaseService
{
    Task<DatabaseResponse> GetTripsAsync();
    Task<DatabaseResponse> DeleteClientAsync(int idClient);
}


