using System;
using System.ComponentModel.DataAnnotations;

namespace sprint_2_actividad_1.Models
{
    public class Attention
    {
        public int AttentionId { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Diagnosis { get; set; }

        // FK a Pet
        public int PetId { get; set; }
        public Pet Pet { get; set; }

        // FK a Veterinarian
        public int VeterinarianId { get; set; }
        public Veterinarian Veterinarian { get; set; }
    }
}