# Job Portal API
Welcome to **Job Portal API** Designed to connect job seekers with employers. It provides a simple and efficient way for individuals to search for jobs, and for companies or recruiters to post new job listings.

![Diagram](JobPortalDiagram.png)

## Technologies : 
- **.NET 8** 
- **Entity Framework Core 8** ORM for seamless database access.
- **SQL Server** Database Provider
- **ASP.NET Core Identity** for Authentication and user managements
- **JWT + Refresh Tokens** Secure, stateless authentication.
- **AutoMapper** Efficient mapping between domain models and DTOs.
- **FluentValidation** defining validation logic cleanly.
- **MediatR + CQRS Pattern** Separation of concerns for commands and queries.
- **Generic Repository Pattern** for data access layer.
- **Clean Code & Clean Architecture** for testability and maintainability.
- **MailKit** and **SMTP** for sending registration or any email notifications.



	public required DateTime DateOfBirth { get; set; }
	public string? Address { get; set; }
	public string? ImagePath { get; set; }
	public string CountryName { get; set; }


## ✨ Features
### 🧑 User
Represents a job seeker, employer or Admin.
- `Id` (int) – Unique identifier
- `FirstName` (string)
- `LastName` (string)
- `UserName` (string)
- `Password` (string)
- `Email` (string)
- `PhoneNumber` (string)
- `Gendor` (enum=> (male, female, other...))
- `DateOfBirth` (DateTime)
- `Address` (string)
- `ImagePath` (string)
- `CountryName` (string)
