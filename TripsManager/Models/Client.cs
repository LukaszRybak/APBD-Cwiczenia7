using System;
using System.Collections.Generic;

namespace TripsManager.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Telephone { get; set; } = null!;

    public string Pesel { get; set; } = null!;

    public virtual ICollection<ClientTrip> ClientTrips { get; } = new List<ClientTrip>();


    public Client(string firstName, string lastName, string email, string telephone, string pesel)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Telephone = telephone;
        Pesel = pesel;
    }
}
