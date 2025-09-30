using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using sprint_2_actividad_1.Data;
using sprint_2_actividad_1.Models;

namespace sprint_2_actividad_1.Services
{
    public class ClinicService : IDisposable
    {
        private readonly AppDbContext _db = new AppDbContext();

        public ClinicService()
        {
            _db.Database.EnsureCreated();
        }

        // CUSTOMERS
        public Client CreateClient(string firstName, string lastName, string phone = null, string email = null, string address = null)
        {
            var client = new Client { FirstName = firstName, LastName = lastName, Phone = phone, Email = email, Address = address };
            _db.Clients.Add(client);
            _db.SaveChanges();
            return client;
        }
                public List<Client> GetAllClients() => _db.Clients.Include(c => c.Pets).ToList();

        public Client GetClientById(int id) => _db.Clients.Include(c => c.Pets).FirstOrDefault(c => c.ClientId == id);

        public void UpdateClient(Client client)
        {
            _db.Clients.Update(client);
            _db.SaveChanges();
        }

        public bool DeleteClient(int id)
        {
            var c = _db.Clients.Find(id);
            if (c == null) return false;
            _db.Clients.Remove(c);
            _db.SaveChanges();
            return true;
        }

        // PETS
        public Pet CreatePet(string name, string species, string breed, DateTime? birthDate, int clientId)
        {
            var pet = new Pet { Name = name, Species = species, Breed = breed, BirthDate = birthDate, ClientId = clientId };
            _db.Pets.Add(pet);
            _db.SaveChanges();
            return pet;
        }

        public List<Pet> GetAllPets() => _db.Pets.Include(p => p.Client).Include(p => p.Attentions).ToList();

        public Pet GetPetById(int id) => _db.Pets.Include(p => p.Client).Include(p => p.Attentions).FirstOrDefault(p => p.PetId == id);

        public void UpdatePet(Pet pet)
        {
            _db.Pets.Update(pet);
            _db.SaveChanges();
        }

        public bool DeletePet(int id)
        {
            var p = _db.Pets.Find(id);
            if (p == null) return false;
            _db.Pets.Remove(p);
            _db.SaveChanges();
            return true;
        }

        // VETERINARIANS
        public Veterinarian CreateVeterinarian(string firstName, string lastName, string specialty = null, string phone = null, string email = null)
        {
            var v = new Veterinarian { FirstName = firstName, LastName = lastName, Specialty = specialty, Phone = phone, Email = email };
            _db.Veterinarians.Add(v);
            _db.SaveChanges();
            return v;
        }

        public List<Veterinarian> GetAllVeterinarians() => _db.Veterinarians.Include(v => v.Attentions).ToList();

        public Veterinarian GetVeterinarianById(int id) => _db.Veterinarians.Include(v => v.Attentions).FirstOrDefault(v => v.VeterinarianId == id);

        public void UpdateVeterinarian(Veterinarian v)
        {
            _db.Veterinarians.Update(v);
            _db.SaveChanges();
        }

        public bool DeleteVeterinarian(int id)
        {
            var v = _db.Veterinarians.Find(id);
            if (v == null) return false;
            _db.Veterinarians.Remove(v);
            _db.SaveChanges();
            return true;
        }

        // ATTENTIONS
        public Attention CreateAttention(DateTime date, string diagnosis, int petId, int veterinarianId)
        {
            var a = new Attention { Date = date, Diagnosis = diagnosis, PetId = petId, VeterinarianId = veterinarianId };
            _db.Attentions.Add(a);
            _db.SaveChanges();
            return a;
        }

        public List<Attention> GetAllAttentions() => _db.Attentions.Include(a => a.Pet).ThenInclude(p => p.Client).Include(a => a.Veterinarian).ToList();

        public Attention GetAttentionById(int id) => _db.Attentions.Include(a => a.Pet).Include(a => a.Veterinarian).FirstOrDefault(a => a.AttentionId == id);

        public void UpdateAttention(Attention a)
        {
            _db.Attentions.Update(a);
            _db.SaveChanges();
        }

        public bool DeleteAttention(int id)
        {
            var a = _db.Attentions.Find(id);
            if (a == null) return false;
            _db.Attentions.Remove(a);
            _db.SaveChanges();
            return true;
        }

        // MEDICAL HISTORY per pet
        public List<Attention> GetMedicalHistoryByPet(int petId)
        {
            return _db.Attentions
                      .Where(a => a.PetId == petId)
                      .Include(a => a.Veterinarian)
                      .OrderByDescending(a => a.Date)
                      .ToList();
        }

        // ADVANCED QUERIES (LINQ over EF)
        // 1. Consult all of a client's pets.
        public List<Pet> GetPetsByClient(int clientId)
        {
            return _db.Pets.Where(p => p.ClientId == clientId).Include(p => p.Client).ToList();
        }

        // 2. Veterinarian with more care.
        public Veterinarian GetTopVeterinarian()
        {
            return _db.Veterinarians
                      .Include(v => v.Attentions)
                      .OrderByDescending(v => v.Attentions.Count)
                      .FirstOrDefault();
        }

        // 3. Most cared for pet species.
        public string GetMostAttendedSpecies()
        {
            return _db.Attentions
                      .Include(a => a.Pet)
                      .GroupBy(a => a.Pet.Species)
                      .OrderByDescending(g => g.Count())
                      .Select(g => g.Key)
                      .FirstOrDefault();
        }

        // 4. Client with the most registered pets.
        public Client GetTopClientByPetCount()
        {
            return _db.Clients
                      .Include(c => c.Pets)
                      .OrderByDescending(c => c.Pets.Count)
                      .FirstOrDefault();
        }

        public void Dispose() => _db.Dispose();
    }
}