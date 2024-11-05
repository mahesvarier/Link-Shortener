# Running the Link Shortener Project

Follow these steps to run the Link Shortener project:

## Prerequisites
- **.NET SDK**: Ensure you have the .NET 6 SDK installed.

## Steps
1. **Clone the Repository**: If you haven't already, clone the repository to your local machine.
2. **Restore Dependencies**: Navigate to the project directory and restore the dependencies.
3. **Build the Project**: Build the project to ensure all dependencies are correctly installed and the project compiles.
4. **Run the Project**: Use the `dotnet run` command to start the application.

   By default, the application will be available at:
   - [https://localhost:7277](https://localhost:7277)
   - [http://localhost:5016](http://localhost:5016)

5. **Access the Application**: Open your web browser and navigate to the launch URL specified in the `launchSettings.json` file, which is:
   - [https://localhost:7277/swagger](https://localhost:7277/swagger)
   - [http://localhost:5016/swagger](http://localhost:5016)

## Additional Information
- **Configuration**: The application settings are configured in `appsettings.json`.
- **Database Migrations**: If you need to apply database migrations, use the following command:
  ```bash
  dotnet ef database update
  ```
- **Testing**: To run the tests, navigate to the `Tests` directory and use the following command:
  ```bash
  dotnet test
  ```

## Troubleshooting
- Ensure that your environment variables are set correctly as specified in the `launchSettings.json` file.
- If you encounter any issues, check the output logs for detailed error messages.

## Database Setup

This project requires a SQL Server database. You can easily set it up using Docker. Follow the steps below to run the SQL Server container:

### Prerequisites

- [Docker](https://www.docker.com/get-started) must be installed and running on your machine.

### Pulling the SQL Server Image

Before running the SQL Server container, you need to pull the SQL Server image from Docker Hub. You can do this by executing the following command in your terminal or command prompt:

```bash
docker pull mcr.microsoft.com/azure-sql-edge
```

### Running SQL Server in Docker

After the image has been pulled, run the following command to start a SQL Server container:

```bash
docker run -e 'ACCEPT_EULA=1' \
           -e 'MSSQL_SA_PASSWORD=MyStrongPassword!' \
           -e 'MSSQL_PID=Developer' \
           -e 'MSSQL_USER=SA' \
           -p 1433:1433 \
           -d --name=sql \
           mcr.microsoft.com/azure-sql-edge

```
Environment Variables:
ACCEPT_EULA=1: Accepts the SQL Server EULA.
MSSQL_SA_PASSWORD=MyStrongPassword!: Sets the SA (System Administrator) password.
MSSQL_PID=Developer: Specifies the edition of SQL Server to run.
MSSQL_USER=SA: Specifies the SQL Server administrator user.