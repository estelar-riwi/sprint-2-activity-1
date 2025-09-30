using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sprint_2_actividad_1.Models
{
    public class Veterinarian
    {
        public int VeterinarianId { get; set; }
        [Required] public string FirstName { get; set; }
        [Required] public string LastName { get; set; }
        public string Specialty { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        public List<Attention> Attentions { get; set; } = new List<Attention>();

        public string GetFullName() => $"{FirstName} {LastName}";
    }
}