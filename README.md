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



## ✨ Features
### 🧑 User
Represents a job seeker, employer or Admin.
- `Id` (int) Unique identifier
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
## 💼 JobListing  
Represents a job listing posted by a company.  

- `JobId` (int)  => Unique identifier
- `Title` (string)  => Title of the job
- `Description` (string?)  => Optional detailed job description  
- `CompanyId` (int)  => ID of the company offering the job  
- `Location` (string)  => Location of the job 
- `JobType` (JobTypeEnum)  => Job type (FullTime, PartTime, Internship)  
- `SalaryRange` (string?)  => Optional salary range (e.g : "$20k–$50k")  
- `DatePosted` (DateTime)  => Date when the job was posted  
- `Status` (JobStatusEnum)  => Status of the job listing (Open, Closed, Pending)  
- `CreatedByUserId` (int)  => ID of the user who created the listing

## 🏢 Company  
Represents a company that posts job listings on the platform.

- `CompanyId` (int)  => Unique identifier for the company  
- `CompanyName` (string)  => Official name of the company  
- `Description` (string)  => Short description or summary of the company  
- `WebsiteUrl` (string)  => Company’s official website URL  
- `Location` (string)  => Company’s physical or headquarters location  
- `PhoneNumber` (string?)  => Optional phone contact number  
- `Email` (string)  => Official email address of the company  
- `Fax` (string?)  => Optional fax number

### 🛠️ Skill  
Represents a specific skill or technology that can be associated with job listings.

- `SkillId` (int)  => Unique identifier for the skill  
- `Name` (string)  => Name of the skill (e.g., "C#", "Project Management")  
- `Description` (string?)  => Optional description or details about the skill  

