using System;
using sprint_2_actividad_1.Services;
using sprint_2_actividad_1.Models;
using System.Globalization;

namespace sprint_2_actividad_1
{
    class Program
    {
        static void Main(string[] args)
        {
            using var service = new ClinicService();
            bool exit = false;

            Console.WriteLine("=== Bienvenido a Veterinaria San Miguel ===");

            while (!exit)
            {
                Console.WriteLine("\n Menú Principal");
                Console.WriteLine("1. Gestión de Clientes");
                Console.WriteLine("2. Gestión de Mascotas");
                Console.WriteLine("3. Gestión de Veterinarios");
                Console.WriteLine("4. Gestión de Atenciones Médicas");
                Console.WriteLine("5. Historial Médico (por mascota)");
                Console.WriteLine("6. Consultas Avanzadas (LINQ)");
                Console.WriteLine("0. Salir");
                Console.Write("Selecciona una opción: ");
                var opt = 
                    Console.ReadLine();

                switch (opt)
                {
                    case "1": 
                        ClientsMenu(service); 
                        break;
                    case "2": 
                        PetsMenu(service); 
                        break;
                    case "3": 
                        VetsMenu(service); 
                        break;
                    case "4": 
                        AttentionsMenu(service); 
                        break;
                    case "5": 
                        ShowMedicalHistory(service); 
                        break;
                    case "6": 
                        AdvancedQueries(service); 
                        break;
                    case "0": 
                        exit = true; 
                        break;
                    default: 
                        Console.WriteLine("Opción inválida."); 
                        break;
                }
            }

            Console.WriteLine("Saliendo... ¡Hasta pronto!");
        }

        // Menus and interaction methods:
        static void ClientsMenu(ClinicService svc)
        {
            while (true)
            {
                Console.WriteLine("\n--- Gestión de Clientes ---");
                Console.WriteLine("1. Registrar cliente");
                Console.WriteLine("2. Listar clientes");
                Console.WriteLine("3. Editar cliente");
                Console.WriteLine("4. Eliminar cliente");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                var o = 
                    Console.ReadLine();
                if (o == "0") 
                    break;

                switch (o)
                {
                    case "1":
                        Console.Write("Nombre: "); var fn = Console.ReadLine();
                        Console.Write("Apellido: "); var ln = Console.ReadLine();
                        Console.Write("Teléfono: "); var ph = Console.ReadLine();
                        Console.Write("Email: "); var em = Console.ReadLine();
                        Console.Write("Dirección: "); var ad = Console.ReadLine();
                        var c = svc.CreateClient(fn, ln, ph, em, ad);
                        Console.WriteLine($"Cliente creado con ID: {c.ClientId}");
                            break;
                    case "2":
                        var clients = svc.GetAllClients();
                        foreach (var cl in clients) Console.WriteLine($"{cl.ClientId}: {cl.GetFullName()} - {cl.Email} - Mascotas: {cl.Pets.Count}");
                            break;
                    case "3":
                        Console.Write("ID cliente a editar: "); if (!int.TryParse(Console.ReadLine(), out int idc)) { Console.WriteLine("ID inválido"); 
                            break; }
                        var cli = svc.GetClientById(idc);
                        if (cli == null) { Console.WriteLine("No encontrado"); 
                            break; }
                        Console.Write($"Nombre ({cli.FirstName}): "); var nfn = Console.ReadLine(); if (!string.IsNullOrEmpty(nfn)) cli.FirstName = nfn;
                        Console.Write($"Apellido ({cli.LastName}): "); var nln = Console.ReadLine(); if (!string.IsNullOrEmpty(nln)) cli.LastName = nln;
                        Console.Write($"Teléfono ({cli.Phone}): "); var nph = Console.ReadLine(); if (!string.IsNullOrEmpty(nph)) cli.Phone = nph;
                        svc.UpdateClient(cli); Console.WriteLine("Cliente actualizado.");
                            break;
                    case "4":
                        Console.Write("ID cliente a eliminar: "); if (!int.TryParse(Console.ReadLine(), out int idd)) { Console.WriteLine("ID inválido"); 
                            break; }
                        if (svc.DeleteClient(idd)) Console.WriteLine("Eliminado."); else Console.WriteLine("No encontrado.");
                            break;
                    default: 
                        Console.WriteLine("Opción inválida"); 
                            break;
                }
            }
        }

        static void PetsMenu(ClinicService svc)
        {
            while (true)
            {
                Console.WriteLine("\n--- Gestión de Mascotas ---");
                Console.WriteLine("1. Registrar mascota");
                Console.WriteLine("2. Listar mascotas");
                Console.WriteLine("3. Editar mascota");
                Console.WriteLine("4. Eliminar mascota");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                var o = 
                    Console.ReadLine();
                if (o == "0") 
                    break;

                switch (o)
                {
                    case "1":
                        Console.Write("Nombre: "); var name = Console.ReadLine();
                        Console.Write("Especie: "); var sp = Console.ReadLine();
                        Console.Write("Raza: "); var br = Console.ReadLine();
                        Console.Write("Fecha nacimiento (yyyy-MM-dd) o vacío: "); var bd = Console.ReadLine();
                        DateTime? birth = null;
                        if (!string.IsNullOrEmpty(bd) && DateTime.TryParseExact(bd, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)) birth = dt;
                        Console.Write("ID Cliente (propietario): "); if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("ID inválido"); 
                            break; }
                        var pet = svc.CreatePet(name, sp, br, birth, cid);
                        Console.WriteLine($"Mascota creada ID: {pet.PetId}");
                            break;
                    case "2":
                        var pets = svc.GetAllPets();
                        foreach (var p in pets) Console.WriteLine($"{p.PetId}: {p.Name} ({p.Species}) - Cliente: {p.Client?.GetFullName()}");
                            break;
                    case "3":
                        Console.Write("ID mascota a editar: "); if (!int.TryParse(Console.ReadLine(), out int idp)) { Console.WriteLine("ID inválido"); 
                            break; }
                        var petToEdit = svc.GetPetById(idp);
                        if (petToEdit == null) { Console.WriteLine("No encontrado"); 
                            break; }
                        Console.Write($"Nombre ({petToEdit.Name}): "); var nn = Console.ReadLine(); if (!string.IsNullOrEmpty(nn)) petToEdit.Name = nn;
                        svc.UpdatePet(petToEdit); Console.WriteLine("Mascota actualizada.");
                            break;
                    case "4":
                        Console.Write("ID mascota a eliminar: "); if (!int.TryParse(Console.ReadLine(), out int idpd)) { Console.WriteLine("ID inválido"); 
                            break; }
                        if (svc.DeletePet(idpd)) Console.WriteLine("Eliminada."); else Console.WriteLine("No encontrada.");
                            break;
                    default: 
                        Console.WriteLine("Opción inválida"); 
                            break;
                }
            }
        }

        static void VetsMenu(ClinicService svc)
        {
            while (true)
            {
                Console.WriteLine("\n--- Gestión de Veterinarios ---");
                Console.WriteLine("1. Registrar veterinario");
                Console.WriteLine("2. Listar veterinarios");
                Console.WriteLine("3. Editar veterinario");
                Console.WriteLine("4. Eliminar veterinario");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                var o = 
                    Console.ReadLine();
                if (o == "0") 
                    break;

                switch (o)
                {
                    case "1":
                        Console.Write("Nombre: "); var fn = Console.ReadLine();
                        Console.Write("Apellido: "); var ln = Console.ReadLine();
                        Console.Write("Especialidad: "); var sp = Console.ReadLine();
                        var v = svc.CreateVeterinarian(fn, ln, sp);
                        Console.WriteLine($"Veterinario creado ID: {v.VeterinarianId}");
                            break;
                    case "2":
                        var vets = svc.GetAllVeterinarians();
                        foreach (var vet in vets) Console.WriteLine($"{vet.VeterinarianId}: {vet.GetFullName()} - {vet.Specialty} - Atenciones: {vet.Attentions.Count}");
                                break;
                    case "3":
                        Console.Write("ID veterinario a editar: "); if (!int.TryParse(Console.ReadLine(), out int idv)) { Console.WriteLine("ID inválido"); 
                                break; }
                        var vetEdit = svc.GetVeterinarianById(idv);
                        if (vetEdit == null) { Console.WriteLine("No encontrado"); 
                                break; }
                        Console.Write($"Nombre ({vetEdit.FirstName}): "); var nfn = Console.ReadLine(); if (!string.IsNullOrEmpty(nfn)) vetEdit.FirstName = nfn;
                        svc.UpdateVeterinarian(vetEdit); Console.WriteLine("Veterinario actualizado.");
                            break;
                    case "4":
                        Console.Write("ID a eliminar: "); if (!int.TryParse(Console.ReadLine(), out int idvd)) { Console.WriteLine("ID inválido"); 
                            break; }
                        if (svc.DeleteVeterinarian(idvd)) Console.WriteLine("Eliminado."); else Console.WriteLine("No encontrado.");
                            break;
                    default: 
                        Console.WriteLine("Opción inválida"); 
                            break;
                }
            }
        }

        static void AttentionsMenu(ClinicService svc)
        {
            while (true)
            {
                Console.WriteLine("\n--- Gestión de Atenciones Médicas ---");
                Console.WriteLine("1. Registrar atención");
                Console.WriteLine("2. Listar atenciones");
                Console.WriteLine("3. Editar atención");
                Console.WriteLine("4. Eliminar atención");
                Console.WriteLine("0. Volver");
                Console.Write("Opción: ");
                var o = 
                    Console.ReadLine();
                if (o == "0") 
                    break;

                switch (o)
                {
                    case "1":
                        Console.Write("Fecha (yyyy-MM-dd HH:mm) o vacío para ahora: "); var dstr = Console.ReadLine();
                        DateTime date = DateTime.Now;
                        if (!string.IsNullOrEmpty(dstr) && DateTime.TryParseExact(dstr, "yyyy-MM-dd HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime dt)) date = dt;
                        Console.Write("Diagnóstico: "); var diag = Console.ReadLine();
                        Console.Write("ID Mascota: "); if (!int.TryParse(Console.ReadLine(), out int pid)) { Console.WriteLine("ID inválido"); 
                            break; }
                        Console.Write("ID Veterinario: "); if (!int.TryParse(Console.ReadLine(), out int vid)) { Console.WriteLine("ID inválido"); 
                            break; }
                        var a = svc.CreateAttention(date, diag, pid, vid);
                        Console.WriteLine($"Atención creada ID: {a.AttentionId}");
                            break;
                    case "2":
                        var atts = svc.GetAllAttentions();
                        foreach (var att in atts) Console.WriteLine($"{att.AttentionId}: {att.Date} - {att.Pet?.Name} - Vet: {att.Veterinarian?.GetFullName()} - {att.Diagnosis}");
                            break;
                    case "3":
                        Console.Write("ID atención a editar: "); if (!int.TryParse(Console.ReadLine(), out int ida)) { Console.WriteLine("ID inválido"); break; }
                        var attEdit = svc.GetAttentionById(ida);
                        if (attEdit == null) { Console.WriteLine("No encontrada"); 
                            break; }
                        Console.Write($"Diagnóstico ({attEdit.Diagnosis}): "); var ndi = Console.ReadLine(); if (!string.IsNullOrEmpty(ndi)) attEdit.Diagnosis = ndi;
                        svc.UpdateAttention(attEdit); Console.WriteLine("Atención actualizada.");
                            break;
                    case "4":
                        Console.Write("ID atención a eliminar: "); if (!int.TryParse(Console.ReadLine(), out int idad)) { Console.WriteLine("ID inválido"); 
                            break; }
                        if (svc.DeleteAttention(idad)) Console.WriteLine("Eliminada."); else Console.WriteLine("No encontrada.");
                            break;
                    default: 
                        Console.WriteLine("Opción inválida"); 
                            break;
                }
            }
        }

        static void ShowMedicalHistory(ClinicService svc)
        {
            Console.Write("ID de la mascota: "); if (!int.TryParse(Console.ReadLine(), out int pid)) { Console.WriteLine("ID inválido"); return; }
            var list = svc.GetMedicalHistoryByPet(pid);
            if (list.Count == 0) { Console.WriteLine("Sin atenciones registradas para esa mascota."); return; }
            foreach (var a in list) Console.WriteLine($"{a.Date} - Vet: {a.Veterinarian?.GetFullName()} - {a.Diagnosis}");
        }

        static void AdvancedQueries(ClinicService svc)
        {
            Console.WriteLine("\n--- Consultas avanzadas ---");
            Console.WriteLine("1. Todas las mascotas de un cliente");
            Console.WriteLine("2. Veterinario con más atenciones");
            Console.WriteLine("3. Especie de mascota más atendida");
            Console.WriteLine("4. Cliente con más mascotas");
            Console.Write("Opción: ");
            var o = 
                Console.ReadLine();
            switch (o)
            {
                case "1":
                    Console.Write("ID Cliente: "); if (!int.TryParse(Console.ReadLine(), out int cid)) { Console.WriteLine("ID inválido"); 
                        break; }
                    var pets = svc.GetPetsByClient(cid);
                    foreach (var p in pets) Console.WriteLine($"{p.PetId}: {p.Name} ({p.Species})");
                        break;
                case "2":
                    var topVet = svc.GetTopVeterinarian();
                    if (topVet == null) Console.WriteLine("No hay veterinarios o atenciones.");
                    else Console.WriteLine($"{topVet.GetFullName()} - Atenciones: {topVet.Attentions.Count}");
                        break;
                case "3":
                    var species = svc.GetMostAttendedSpecies();
                    Console.WriteLine(species ?? "No hay registros.");
                        break;
                case "4":
                    var topClient = svc.GetTopClientByPetCount();
                    if (topClient == null) Console.WriteLine("No hay clientes.");
                    else Console.WriteLine($"{topClient.GetFullName()} - Mascotas: {topClient.Pets.Count}");
                        break;
                default:
                    Console.WriteLine("Opción inválida"); 
                        break;
            }
        }
    }
}