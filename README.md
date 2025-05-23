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



## ✨ Features :

### 🧑 User
Represents a system user, which can be a job seeker, employer, or admin. This entity stores personal and contact details required for authentication and profile management.

- `Id` (int) => Unique identifier  
- `FirstName` (string) => User's first name  
- `LastName` (string) => User's last name  
- `UserName` (string) => Unique username for login  
- `Password` (string) => User's password (hashed)  
- `Email` (string) => User's email address  
- `PhoneNumber` (string) => Contact phone number  
- `Gendor` (enum) => Gender of the user (Male, Female, Other)  
- `DateOfBirth` (DateTime) => User's date of birth  
- `Address` (string) => address of the user  
- `ImagePath` (string) => Path to the user's profile image  
- `CountryName` (string) => Name of the user's country

### 🌍 Country Entity

Represents a country in the system. This entity is used for associating users with specific geographical locations.

- `CountryId` (int) => Unique identifier for the country  
- `CountryName` (string) => Name of the country

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
Represents a specific skill that can be associated with job listings.

- `SkillId` (int)  => Unique identifier for the skill  
- `Name` (string)  => Name of the skill (e.g., "C#", "Project Management")  
- `Description` (string?)  => Optional description or details about the skill  

### 🔗 JobSkill
Represents the many-to-many relationship between job listings and skills. Each record links one job listing to one required skill.

- `JobListingId` (int) => Reference to the associated job listing  
- `SkillId` (int) => Reference to the associated skill  

### 🗂️ Category  
Represents a job category used to classify job listings (e.g., IT, Finance, Healthcare).

- `CategoryId` (int) => Unique identifier for the category  
- `Name` (string) => Name of the category
- `Description` (string) => Description or details about the category  

### 🔗 JobCategory  
Represents the many-to-many relationship between job listings and categories.

- `CategoryId` (int) => Reference to the associated category  
- `JobListingId` (int) => Reference to the associated job listing  

### 📄 Application

Represents a user's job application for a specific job listing. This entity is used to track the application submission, its status, and relevant timestamps.

- `ApplicationId` (int) => Unique identifier
- `JobListingId` (int) => Identifier of the job listing to which the application is submitted  
- `UserId` (int) => Identifier of the user who submitted the application  
- `Description` (string?) => Optional  Description or details about the application
-  `CreatedOn` (DateTime) => Date and time when the application was created  
- `Status` (Enum) => Current status of the application (e.g., Submitted, InReview, Accepted, Rejected)  
- `LastStatusDate` (DateTime) => Date and time when the application status was changed

### 📌 Bookmark Entity

Represents a job bookmarked by a user for future reference. This entity helps users save job listings they are interested in.

- `BookMarkId` (int) => Unique identifier for the bookmark entry  
- `JobId` (int) => Identifier of the job listing that was bookmarked  
- `UserId` (int) => Identifier of the user who bookmarked the job  
- `DateBooked` (DateTime) => Date and time when the job was bookmarked



