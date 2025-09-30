# Veterinary Clinic Management System

## OOP Justification

This project applies Object-Oriented Programming (OOP) as follows:
- **Domain classes**: `Client`, `Pet`, `Veterinarian` and `Care` represent entities with attributes and behaviors.
- **Encapsulation**: Data is stored in properties, while logic is grouped inside `ClinicService` for managing clients, pets, vets, and attentions.
- **Abstraction & Responsibility Separation**: `AppDbContext` handles persistence, `ClinicService` handles business logic, and `Program.cs` is the user interface.
- **Relationships**: Objects reference each other (`Client` owns `Pets`, `Attention` links to `Pet` and `Veterinarian`).
- **ORM**: Entity Framework Core maps classes to database tables, making data accessible as objects.

---

## UML Diagrams 

### Class Diagram
See `class-diagram.png`

### Use Case Diagram
See `usecase-diagram.png`

### Entity-Relationship Diagram (ERD)
See `erd-diagram.png`

---

## Dependencies installed in terminal

- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Pomelo.EntityFrameworkCore.MySql

---

## How to Run

```bash
dotnet restore
dotnet build
dotnet run --project sprint-2-actividad-1
```

---

## Features

- Register clients, pets, veterinarians, and attentions
- List and search data
- Report: top client by pet count

---

## Author

Vanessa Gómez López