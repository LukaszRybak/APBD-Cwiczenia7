using System.ComponentModel.DataAnnotations;
using TripsManager.Models.ValidationAttributes;

namespace TripsManager.Models.DTO.Requests
{
    public class AssignClientToTripRequestDto
    {
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email adress  is required")]
        [EmailAddress(ErrorMessage = "Invalid email adress")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number  is required")]
        [PhoneNumber(ErrorMessage = "Invalid phone number, required format: 123-456-789")]
        public string Telephone { get; set; }
        
        [Required(ErrorMessage = "Pesel is required")]
        public string Pesel { get; set; }

        [Required(ErrorMessage = "IdTrip is required")]
        public int IdTrip { get; set; }

        [Required(ErrorMessage = "Trip name is required")]
        public string TripName { get; set; }

        [DateFormat(ErrorMessage = "Payment date must be in this format: MM/DD/YYYY")]
        public DateTime? PaymentDate { get; set; }

        public AssignClientToTripRequestDto(string firstName, string lastName, string email,
            string telephone, string pesel, int idTrip, string tripName, DateTime? paymentDate)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Telephone = telephone;
            Pesel = pesel;
            IdTrip = idTrip;
            TripName = tripName;
            PaymentDate = paymentDate;
        }




    }

}
