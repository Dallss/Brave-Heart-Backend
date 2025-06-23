# BraveHeartBackend

BraveHeartBackend is an ASP.NET Core Web API project designed for robust backend development, featuring authentication, role management, logging, and a modular architecture for products and users. It uses PostgreSQL for data storage and supports JWT-based authentication.

## Features

- ASP.NET Core 9 Web API
- Entity Framework Core with PostgreSQL
- JWT Authentication & Role-based Authorization
- User and Product management endpoints
- Serilog logging (console & file)
- Swagger/OpenAPI documentation
- Environment-based configuration

## Project Structure

```
BraveHeartBackend/
  Controllers/         # API controllers
  Data/                # Entity Framework DbContext
  DTOs/                # Data Transfer Objects
  Logs/                # Log files (Serilog)
  Migrations/          # EF Core migrations
  Models/              # Entity models
  Properties/          # Launch settings
  Program.cs           # Application entry point
  appsettings.json     # App configuration
  BraveHeartBackend.csproj # Project file
```

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- (Optional) [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio

### Setup

1. **Clone the repository:**
   ```bash
   git clone <repo-url>
   cd BraveHeartBackend
   ```

2. **Create a `.env` file in the project root:**

   ```
   DB_HOST=localhost
   DB_PORT=5432
   DB_NAME=braveheartdb
   DB_USER=your_db_user
   DB_PASS=your_db_password

   JWT_ISSUER=BraveHeartAPI
   JWT_AUDIENCE=BraveHeartClient
   JWT_SECRET_KEY=your_super_secret_key
   JWT_EXPIRY_MINUTES=60

   ADMIN_EMAIL=admin@example.com
   ADMIN_PASSWORD=Admin123!
   ```

   > The application loads environment variables from `.env` for database and JWT configuration.

3. **Install dependencies:**
   ```bash
   dotnet restore
   ```

4. **Apply database migrations:**
   ```bash
   dotnet ef database update
   ```

5. **Run the application:**
   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:5001` (or as configured).

### API Documentation

- Swagger UI is available at `/swagger` in development mode.

### Logging

- Logs are written to the `Logs/` directory with daily rolling files.

## Development Notes

- The admin user is created automatically on startup if it does not exist, using credentials from the `.env` file.
- JWT settings can be found in `appsettings.json` and overridden by environment variables.
- For development-specific settings, see `appsettings.Development.json`.


## License

[MIT](LICENSE) (or specify your license) 