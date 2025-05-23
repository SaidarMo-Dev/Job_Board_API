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

- ### User Management
  Secure authentication and authorization system leveraging ASP.NET Identity for managing users, roles (job seekers, employers, admins)and password hashing.

- ### Comprehensive Job Listings
  Employers can create, update, and manage job postings with detailed information including title, description, location, job type, salary range, and status tracking.

- ### Advanced Job Search and Categorization
  Jobs are categorized by industry (categories) and associated with required skills, enabling precise filtering and matching for job seekers.

 - ### Skill and Category Association
  Many-to-many relationships between jobs and skills, and jobs and categories, allow flexible and accurate representation of job requirements.

- ### User Applications Tracking
  Job seekers can apply to job listings, with applications tracked through multiple statuses (Submitted, InReview, Accepted, Rejected) along with timestamps for progress monitoring.

- ### Bookmarking System
  Users can bookmark jobs they are interested in, making it easy to save and revisit listings for future consideration.

- ### Company Profiles
  Companies can maintain detailed profiles including contact information, descriptions, and location, providing credibility and context to job listings.

- ### Geographical Data Support
Countries are managed as entities, allowing user profiles and job listings to be associated with specific locations.




