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
