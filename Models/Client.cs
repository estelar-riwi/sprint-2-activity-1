using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sprint_2_actividad_1.Models
{
    public class Client
    {
        public int ClientId { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        // Relationship: Client has pets
        public List<Pet> Pets { get; set; } = new List<Pet>();

        // Simple overload example: Full name
        public string GetFullName() => $"{FirstName} {LastName}";
        public string GetFullName(string title) => $"{title} {FirstName} {LastName}";
    }
}