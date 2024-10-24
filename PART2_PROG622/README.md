
# PART2_PROG622 Claim Management System

## Description
This is a web-based **Claim Management System** built using **ASP.NET Core MVC**. The project allows users to submit, manage, and review claims, as well as upload and manage related files.

## Features
- **Submit Claims**: Lecturers can submit claims for reimbursement.
- **Manage Claims**: Managers and coordinators can review and update submitted claims.
- **Upload Files**: Users can attach documents to support their claims.
- **Download Files**: Download uploaded documents easily.
- **Delete Files**: Manage uploaded files by deleting them if needed.
- **Role-Based Access Control**: Certain features are restricted based on user roles.

## Technologies Used
- **ASP.NET Core MVC** for the web application.
- **Entity Framework Core** for database interaction.
- **SQL Server or SQLite** as the database.
- **Bootstrap** for frontend styling.
- **xUnit** for unit testing.
- **Moq** for mocking in unit tests.

## Prerequisites
- .NET 6 SDK or later
- Visual Studio 2022 or any compatible IDE
- SQL Server or SQLite configured via `ApplicationDbContext`

## How to Run the Project
1. **Clone the repository**:
   ```bash
   git clone <repository-url>
   cd PART2_PROG622
   ```

2. **Restore dependencies**:
   ```bash
   dotnet restore
   ```

3. **Apply migrations**:
   ```bash
   dotnet ef migrations add InitialCreate
   dotnet ef database update
   ```

4. **Run the project**:
   ```bash
   dotnet run
   ```

5. **Open in browser**:
   Navigate to `http://localhost:5226/` to access the application.

## Folder Structure
```
PART2_PROG622/
├── Controllers/
│   ├── ClaimController.cs
│   ├── FileCreationController.cs
│   └── HomeController.cs
├── Models/
│   ├── Claim.cs
│   ├── FileCreation.cs
│   └── FileUploadViewModel.cs
├── Views/
│   ├── Claim/
│   ├── FileCreation/
│   ├── Home/
│   └── Shared/
├── Data/
│   └── ApplicationDbContext.cs
└── wwwroot/
    ├── css/
    ├── js/
    └── uploads/
```

## Unit Testing
Unit tests have been implemented to ensure the correctness of critical functions.

### Running Tests
1. Navigate to the test project directory.
2. Use the following command to run tests:
   ```bash
   dotnet test
   ```

## Author
- **Neo Mokoatle**

## License
This project is licensed under the MIT License.

## Acknowledgments
Thanks to all contributors and mentors who provided guidance throughout the development process.
