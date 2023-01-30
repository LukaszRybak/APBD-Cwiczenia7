using TripsManager.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using TripsManager.Services;
using TripsManager.Models.DTO.Responses;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using TripsManager.Models.DTO.Requests;

namespace TripsManager.Services
{

    public class TripsDatabaseService : IDatabaseService
    {
        private readonly S15339Context _context;

        public TripsDatabaseService(S15339Context context)
        {
            _context = context;
        }

        public async Task<DatabaseResponseDto> GetTripsAsync()
        {
            var trips = await _context.Trips
                                    .Include(t => t.ClientTrips)
                                    .Include(t => t.CountryTrips)
                                    .ToListAsync();
            var clients = await _context.Clients
                                    .Include(cl => cl.ClientTrips)
                                    .ToListAsync();

            var countries = await _context.Countries
                                    .Include(cn => cn.CountryTrips)
                                    .ToListAsync();

            var output = trips.Select(t => new
            {
                idTrip = t.IdTrip,
                name = t.Name,
                description = t.Description,
                dateFrom = t.DateFrom,
                dateTo = t.DateTo,
                maxPeople = t.MaxPeople,
                Countries = t.CountryTrips
                    .Select(cnt => countries.First(cn => cn.IdCountry == cnt.IdCountry))
                    .Select(cn => new { Name = cn.Name }),
                Clients = t.ClientTrips
                    .Select(clt => clients.First(cl => cl.IdClient == clt.IdClient))
                    .Select(cl => new { FirstName = cl.FirstName, LastName = cl.LastName })
            })
                .OrderByDescending(t => t.dateFrom)
                .ToList();

            return new DatabaseResponseDto(200, "", output);
        }

        public async Task<DatabaseResponseDto> DeleteClientAsync(int idClient)
        {
            var client = await _context.Clients.Where(cl => cl.IdClient == idClient).SingleOrDefaultAsync();

            if (client == null)
            {
                return new DatabaseResponseDto(404, "Client with given Id doesn't exist", null);
            }
            var clientTrips = _context.ClientTrips.Where(ct => ct.IdClient == idClient).ToList();

            if (clientTrips.Any())
            {
                return new DatabaseResponseDto(400, "Cannot delete client data because they are assigned to one or more trips", null);
            }

            _context.Clients.Remove(client);
            _context.SaveChanges();

            return new DatabaseResponseDto(200, "Client data removed succesfully", null);
        }

        public async Task<DatabaseResponseDto> AssignClientToTripAsync(int idTrip, AssignClientToTripRequestDto assignClientToTripRequestDto)
        {
            var client = await _context.Clients.Where(cl => cl.Pesel == assignClientToTripRequestDto.Pesel)
                                               .SingleOrDefaultAsync();
            if (client == null)
            {
                client = new Client(
                                assignClientToTripRequestDto.FirstName,
                                assignClientToTripRequestDto.LastName,
                                assignClientToTripRequestDto.Email,
                                assignClientToTripRequestDto.Telephone,
                                assignClientToTripRequestDto.Pesel);
                _context.Clients.Add(client);
            }
            _context.SaveChanges();

            var existingClientTrip = await _context.ClientTrips.Where(ct => ct.IdClient == client.IdClient && ct.IdTrip == assignClientToTripRequestDto.IdTrip)
                                                               .SingleOrDefaultAsync();
            if (existingClientTrip != null)
            {
                return new DatabaseResponseDto(400, "Client is already assigned to this trip", null);
            }

            var trip = await _context.Trips.Where(t => t.IdTrip == assignClientToTripRequestDto.IdTrip)
                                           .SingleOrDefaultAsync();
            if (trip == null)
            {
                return new DatabaseResponseDto(404, "Trip not found", null);
            }

            var newClientTrip = new ClientTrip(
                                        client.IdClient,
                                        trip.IdTrip,
                                        DateTime.Now,
                                        assignClientToTripRequestDto.PaymentDate
                                        );

            _context.ClientTrips.Add(newClientTrip);
            _context.SaveChanges();

            return new DatabaseResponseDto(200, "Client added to trip sucessfully", null);
        }

    }
}
