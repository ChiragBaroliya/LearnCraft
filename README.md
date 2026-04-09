# LearnCraft 🚀

LearnCraft is a scalable, production-ready Learning Management System (LMS) backend built with **.NET 10** following **Clean Architecture** principles.

## 🏗️ Architecture

The project is structured into four main layers:

1.  **Domain**: Core business logic, entities, and value objects.
2.  **Application**: Application logic using MediatR for CQRS (Command Query Responsibility Segregation).
3.  **Infrastructure**: Data persistence (EF Core + PostgreSQL), authentication (JWT), and external services.
4.  **API**: RESTful endpoints and middleware.

## ✨ Features

-   **User Authentication**: Secure Login and Registration using JWT tokens.
    -   **Password Hashing**: Industry-standard **BCrypt** algorithm for secure password storage.
-   **Course Management**: Create and manage courses with multiple lessons.
-   **Content Support**: Support for both Video and Document content types.
-   **Enrollment System**: Track student progress and course enrollments.
-   **Clean & Scalable**: Decoupled architecture using the Repository and Unit of Work patterns.

## 🛠️ Tech Stack

-   **Runtime**: .NET 10
-   **Database**: PostgreSQL
-   **ORM**: Entity Framework Core
-   **Communication**: MediatR (CQRS)
-   **Security**: JWT Bearer Authentication, BCrypt.Net-Next
-   **Logging**: Serilog
-   **Validation**: FluentValidation

## 🚀 Getting Started

### Prerequisites

-   [.NET 10 SDK](https://dotnet.microsoft.com/download)
-   [PostgreSQL](https://www.postgresql.org/download/)

### Setup

1.  **Clone the repository**:
    ```bash
    git clone https://github.com/your-repo/learn-craft.git
    cd learn-craft
    ```

2.  **Configure Database**:
    Update the connection string in `LearnCraft.API/appsettings.json`:
    ```json
    "ConnectionStrings": {
      "Database": "Host=localhost;Database=LearnCraftDb;Username=postgres;Password=yourpassword"
    }
    ```

3.  **Run Migrations & Seed**:
    The application is configured to automatically apply migrations and seed initial data on startup.

4.  **Run the application**:
    ```bash
    dotnet run --project LearnCraft.API
    ```
    The API will be available at `https://localhost:5001` (or the port specified in `launchSettings.json`). You can access the Swagger UI at `/swagger`.

## 🧪 Testing

Run unit and integration tests using:
```bash
dotnet test
```

## 📜 License

This project is licensed under the MIT License.
