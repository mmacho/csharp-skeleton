# C# Hexagonal ASEME

# Create folders structure:
1. Create folders into solution
2. Add new project with the same folder from step 1.
    - app: ASP.NET Core Web Api
    - src: Biblioteca de clases
    - test:Biblioteca de clases

## Starting the server
* Local using:
    * `dotnet run --project apps/HubSupplier/Backend/Backend.csproj`
* Docker using:
    * `docker-compose up` TODO
    
And then going to http://localhost:8030/health-check to check all is ok.