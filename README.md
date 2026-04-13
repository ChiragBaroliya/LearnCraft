# LearnCraft 🚀

LearnCraft is a scalable, production-ready Learning Management System (LMS) backend built with **.NET 10** following **Clean Architecture** principles. The platform is powered by a high-performance **GraphQL** API.

## 🏗️ Architecture

The project follows Clean Architecture and is organized into four main layers:

1.  **Domain**: Core business logic, entities, and value objects.
2.  **Application**: Application logic using MediatR for CQRS (Command Query Responsibility Segregation).
3.  **Infrastructure**: Data persistence (EF Core + PostgreSQL), authentication (JWT), and external services.
4.  **API (GraphQL)**: A modular API layer using **HotChocolate Type Extensions**. The schema is split into feature-based modules (Courses, Lessons, Enrollments, Progress, Users) for maximum maintainability.

## ✨ Features

-   **GraphQL Powered**: Flexible and efficient data fetching with a single `/graphql` endpoint.
-   **Modular Design**: Type Extensions allow features to be 100% independent.
-   **User Authentication**: Secure Login and Registration using JWT tokens and GraphQL mutations.
-   **Course Management**: Create and manage courses with multiple lessons.
-   **Enrollment System**: Track student progress and course enrollments.
-   **Clean & Scalable**: Decoupled architecture using the Repository, Unit of Work, and CQRS patterns.

## 🛠️ Tech Stack

-   **Runtime**: .NET 10
-   **API Layer**: HotChocolate (GraphQL)
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
    Update the connection string in `LearnCraft.API/appsettings.json`.

3.  **Run Migrations & Seed**:
    The application automatically applies migrations and seeds initial data (Admin/Instructor users) on startup.

4.  **Run the application**:
    ```bash
    dotnet run --project LearnCraft.API
    ```

### Accessing the API

-   **Banana Cake Pop (GraphQL IDE)**: Access `https://localhost:5001/graphql` to explore the schema and test queries.
-   **Swagger UI**: Access `https://localhost:5001/swagger` for any remaining utility endpoints.

## 🧪 Testing

Run unit and integration tests using:
```bash
dotnet test
```

## 📜 License

This project is licensed under the MIT License.
