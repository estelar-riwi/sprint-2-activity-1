using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace sprint_2_actividad_1.Models
{
    public class Pet
    {
        public int PetId { get; set; }
        [Required] public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public DateTime? BirthDate { get; set; }

        // FK a Client
        public int ClientId { get; set; }
        public Client Client { get; set; }

        public List<Attention> Attentions { get; set; } = new List<Attention>();
    }
}