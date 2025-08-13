# Project Repository

[![.NET](https://img.shields.io/badge/.NET-8.0-blueviolet)](https://dotnet.microsoft.com/)
[![Angular](https://img.shields.io/badge/Angular-20-red)](https://angular.io/)
[![Docker](https://img.shields.io/badge/Docker-MySQL-blue)](https://www.docker.com/)

This repository contains a **.NET Core API** and an **Angular 20** front-end application, integrated with a **MySQL database** managed via Docker. Follow the steps below to set up and run the projects.

## Repository Structure

- `TaskManagement/` - .NET Core API solution
- `TaskManager-UI/` - Angular 20 application
- `docker-compose.yml` - Docker configuration for MySQL

## Prerequisites

Ensure you have the following installed:

- [Docker](https://www.docker.com/get-started/) and [Docker Compose](https://docs.docker.com/compose/install/)
- [.NET Core SDK 8.0 or later](https://dotnet.microsoft.com/download)
- [Node.js 20.x](https://nodejs.org/) and [Angular CLI 20.x](https://angular.io/cli) (`npm install -g @angular/cli`)
- (Optional) [MySQL Workbench](https://www.mysql.com/products/workbench/) for database management

## Setup Instructions

### 1. Clone the Repository

```bash
git clone https://github.com/your-username/your-repo.git
cd your-repo
```

### 2. Set Up MySQL with Docker

1. Ensure Docker is running.
2. Launch the MySQL container using the provided `docker-compose.yml`:

   ```bash
   docker-compose up -d
   ```
3. Verify the container is running:

   ```bash
   docker ps
   ```

### 3. Run the .NET Core API

1. Navigate to the API directory:

   ```bash
   cd TaskManagement
   ```

2. Restore dependencies:

   ```bash
   dotnet restore
   ```

4. Apply database migrations (if using Entity Framework):

   ```bash
   dotnet ef database update
   ```

5. Start the API:

   ```bash
   dotnet run
   ```

   The API will be available at `https://localhost:44310` (confirm port in `api/Properties/launchSettings.json`).

### 4. Run the Angular 20 Application

1. Navigate to the frontend directory:

   ```bash
   cd TaskManager-UI
   ```

2. Install dependencies:

   ```bash
   npm install
   ```

3. Start the Angular development server:

   ```bash
   npm start
   ```

   The app will be available at `http://localhost:4200`.

## Accessing the Application

- **API**: `https://localhost:44310` (e.g., `/swagger` for API docs if enabled)
- **Frontend**: `http://localhost:4200`
- **MySQL**: Connect at `localhost:3307` with the credentials above

## Stopping the Application

- **Angular**: Press `Ctrl+C` in the `ng serve` terminal.
- **API**: Press `Ctrl+C` in the `dotnet run` terminal.
- **MySQL**: Stop the container:

   ```bash
   docker-compose down
   ```

## Troubleshooting

- **Database Connection**: Ensure the MySQL container is running (`docker ps`) and the connection string in `appsettings.json` is correct.
- **API Call Errors**: Confirm the `apiUrl` in `environment.ts` matches the API’s URL and the API is running.
- **Port Conflicts**: Update `launchSettings.json` (API) or `angular.json` (Angular) if ports `44310` or `4200` are in use.

## Notes

- Ensure CORS is configured in the .NET Core API to allow requests from `http://localhost:4200`.
- For production, secure connection strings, API URLs, and Docker settings.
