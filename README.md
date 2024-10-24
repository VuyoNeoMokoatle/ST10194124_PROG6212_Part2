#Claim Management System

## Description
This is a web-based **Claim Management System** built with **ASP.NET Core MVC**. The project allows users to submit, manage, and review claims and upload associated files. There are different roles, including **Lecturer**, **Coordinator**, and **Manager**, each with specific access to various features.

## Features
- **Submit and Manage Claims:** Lecturers can submit claims, and coordinators or managers can review them.
- **Upload Files:** Users can upload documents supporting their claims.
- **Role-Based Access:** Access to certain views is restricted based on the user's role.
- **File Management:** Files can be downloaded, managed, or deleted via the application.

## Prerequisites
- .NET 6 SDK or later
- Visual Studio 2022 (or any compatible IDE)
- SQL Server or SQLite (configured via `ApplicationDbContext`)

## How to Run
1. **Clone the repository**:
    ```bash
    git clone <repository-url>
    cd PART2_PROG622
    ```

2. **Install dependencies**:
   Open the project in Visual Studio and run the following commands in the terminal:
    ```bash
    dotnet restore
    ```

3. **Apply database migrations**:
    ```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

4. **Run the application**:
    ```bash
    dotnet run
    ```

5. **Open in browser**:  
   Go to `http://localhost:5226/` to access the home page.

## Folder Structure
