# Portafolio WebApi

> [JWT.io](https://jwt.io/) <br>
> [Generate SHA256 Key](https://tools.keycdn.com/sha256-online-generator)

## Dependencies
### ClassLibraryApplication
```
Dapper
Microsoft.AspNetCore.Cryptography.KeyDerivation
Microsoft.Extensions.Logging.Abstractions
```
### WebApi
```
Dapper
Microsoft.AspNetCore.Authentication.JwtBearer
Microsoft.EntityFrameworkCore.SqlServer
Swashbuckle.AspNetCore
```

### WebApiTest
```
Moq.Dapper
```

## Structure
* **WebApi:** handles HTTP requests.
* **Application:** business logic and services.
* **Domain:** entities and business rules.
* **Infrastructure:** Interacts with databases and external services.
```
MySolution.sln
  ├── WebApi/                       (Web API)
  │   ├── Controllers/              (Controladores de la API)
  │   ├── Program.cs                (Configuración de la aplicación)
  │   └── appsettings.json          (Configuraciones)
  ├── Application/                  (Aplicación Class Library)
  │   ├── DTOs/                     (Objetos de Transferencia de Datos)
  │   ├── Entities/                 (Entidades de dominio)
  │   ├── Filters/                  (Filtros para la lógica de aplicación)
  │   ├── Interfaces/               (Interfaces de servicios)
  │   └── Services/                 (Implementaciones de servicios)
  ├── Domain/                       (Proyecto Class Library)
  │   ├── Entities/                 (Entidades del dominio)
  │   └── ValueObjects/             (Objetos de valor del dominio)
  ├── Infrastructure/               (Proyecto Class Library)
      ├── Data/                     (Manejo de datos)
      ├── Repositories/             (Repositorios)
      └── ExternalServices/         (Servicios externos)
```

## Loggers
* Use in Services and Controllers
```
private readonly ILogger<MyClass> _logger;

_logger.LogInformation("Iniciando la recuperación de grupos de URLs.");
_logger.LogWarning("Error al recuperar grupos de URLs: {StatusCode}", response.StatusCode);
_logger.LogError(ex, "Ocurrió un error al procesar la solicitud.");

_logger.LogCritical("Error crítico: La aplicación no puede continuar.");
_logger.LogDebug("Este es un mensaje de depuración.");
```

## AuthPassword Class
```
public class AuthPassword
{
    public (string Hash, string Salt) HashPassword(string password)
    {
        byte[] saltBytes = RandomNumberGenerator.GetBytes(16);
        string salted = Convert.ToBase64String(saltBytes);
        string hashed = NewHash(password, saltBytes);

        return (hashed, salted);
    }

    public bool VerifyPassword(string password, string hashedPassword, string salt)
    {
        byte[] saltBytes = Convert.FromBase64String(salt);
        string hashed = NewHash(password, saltBytes);

        return hashed == hashedPassword;
    }

    private string NewHash(string password, byte[] salt)
    {
        return Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 32));
    }
}
```

## 
```
"ConnectionStrings": {
    "RutaWebSQL": "Data Source=localhost; Initial Catalog=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True;",
    "RutaSQL": "Server=localhost; Database=db_testing; User ID=testing; Password=testing; TrustServerCertificate=True; Trusted_Connection=True;"
}
```

## Program.cs
* Inject AuthPassword
* Inject IDbConnection to conect Dapper
```
// Dependency injection ----------------------------------------------
builder.Services.AddScoped<AuthPassword>();
// -------------------------------------------------------------------

// Servicio Conexion -------------------------------------------------
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaSQL"))
);
// -------------------------------------------------------------------

// JWT Service -------------------------------------------------------
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtOptions =>
    {
        JwtOptions.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]!)),
        };
    });
// -------------------------------------------------------------------

// Modify Swagger Service --------------------------------------------
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "Portafolio .NETCore7", Version = "v1" });

    options.AddSecurityDefinition(
        name: JwtBearerDefaults.AuthenticationScheme,
        securityScheme: new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Description = "Ingrese Token Bearer",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        }
    );

    options.OperationFilter<SwaggerApiPadLockFilter>();
});
// -------------------------------------------------------------------

// swagger as Default  -----------------------------------------------
app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});
// -------------------------------------------------------------------

// Cors --------------------------------------------------------------
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
    options.SetIsOriginAllowed(origin => true);
});
// -------------------------------------------------------------------

// Use JWT -----------------------------------------------------------
app.UseAuthentication();
// -------------------------------------------------------------------
app.UseAuthorization();

```

## Interface and Services
* Base Interface format
```
public interface InterfaceName
{
    Task<ResponseApiDTO<IEnumerable<DTOClass>>> GetAllAsync(CancellationToken cancellationToken);
    Task<ResponseApiDTO<DTOClass>> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<ResponseApiDTO<DTOClass>> InsertAsync(DTOClass dtoClass, CancellationToken cancellationToken);
    Task<ResponseApiDTO<DTOClass>> UpdateAsync(DTOClass dtoClass, CancellationToken cancellationToken);
    Task<ResponseApiDTO<DTOClass>> DeleteAsync(int id, CancellationToken cancellationToken);
}
```
* Base Service format, Implementing Interface
```
public class Service Name : InterfaceName
{
    private readonly IDbConnection _connection;

    public UrlGrpService(IDbConnection connection)
    {
        _connection = connection;
    }

    public async Task<ResponseApiDTO<IEnumerable<UrlGrpDTO>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var result = await _connection.QueryAsync<DTOClass>(
                new CommandDefinition(
                    $"PA_Strored_Procedure",
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));

            return new ResponseApiDTO<IEnumerable<DTOClass>>
            {
                StatusCode = 200,
                Message = "",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseApiDTO<IEnumerable<DTOClass>>
            {
                StatusCode = 500,
                Message = "Error en la operación de base de datos: " + ex.Message
            };
        }
    }

    public async Task<ResponseApiDTO<DTOClass>> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _connection.QueryFirstOrDefaultAsync<DTOClass>(
                new CommandDefinition(
                    $"PA_Strored_Procedure",
                    new { Id = id  },
                    commandType: CommandType.StoredProcedure,
                    transaction: default,
                    cancellationToken: cancellationToken
            ));

            if (result == null)
                return new ResponseApiDTO<DTOClass>
                {
                    StatusCode = 404,
                    Message = "Registro No Encontrado."
                };

            return new ResponseApiDTO<DTOClass>
            {
                StatusCode = 200,
                Message = "",
                Data = result
            };
        }
        catch (Exception ex)
        {
            return new ResponseApiDTO<DTOClass>
            {
                StatusCode = 500,
                Message = "Error en la operación de base de datos: " + ex.Message
            };
        }
    }

       public async Task<ResponseApiDTO<DTOClass>> InsertAsync(DTOClass dto, CancellationToken cancellationToken)
        {
            try
            {
                var result = await _connection.QueryFirstAsync<ResponseSqlDTO>(
                    new CommandDefinition(
                        $"PF_EnlaceGrp_Insert",
                        new { dto.Name, dto.Status },
                        commandType: CommandType.StoredProcedure,
                        transaction: default,
                        cancellationToken: cancellationToken
                ));

                dto.Id = Convert.ToInt32(result.Id);

                return new ResponseApiDTO<DTOClass>
                {
                    StatusCode = result.StatusCode,
                    Message = result.Msge,
                    Data = dto
                };
            }
            catch (Exception ex)
            {
                return new ResponseApiDTO<DTOClass>
                {
                    StatusCode = 500,
                    Message = "Error en la operación de base de datos: " + ex.Message
                };
            }
        }

    public Task<ResponseApiDTO<UrlGrpDTO>> UpdateAsync(UrlGrpDTO urlGrpDTO, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseApiDTO<UrlGrpDTO>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
```

## Cloud ANd DevObs
### Docker
* Create Dockerfile
```
# Usa la imagen base de .NET
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["ClassLibraryApplication/ClassLibraryApplication.csproj", "ClassLibraryApplication/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "WebApi.csproj" -c Release -o /app/publish

# Genera la imagen final
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "WebApi.dll"]
```
* Create .dockerignore
```
bin/
obj/
.vscode/
.git/
```
* Build the Docker image
```
docker build -t nombre-de-tu-imagen .
```

### docker-compose
```
version: '3.8'

services:
  mssql:
    image: mcr.microsoft.com/mssql/server
    container_name: mssql_container    
    environment:
      - MSSQL_SA_PASSWORD=S!cretPa55
      - ACCEPT_EULA=Y
      - MSSQL_PID=Developer
    ports:
      - "1433:1433"
    volumes:
      - mssql_data:/var/opt/mssql

  webapi:
    build: .
    container_name: dotnet_webapi_container
    ports:
      - "8080:80"
    environment:
      ConnectionStrings__RutaWebSQL: "Server=mssql;Database=db_testing;User ID=testing;Password=testing;TrustServerCertificate=True;"
    depends_on:
      - mssql

volumes:
  mssql_data:
```
* [http://localhost:8080](http://localhost:8080)

* Build Compose
```
docker-compose up --build
```

### SQL Server 
* [SQL Server file](https://github.com/TheNefelin/Portafolio_.NETCore7/MSSQL.sql)
```
SELECT 
	NAME AS LoginName, 
	TYPE_DESC AS AccountType, 
	create_date, 
	modify_date,
	TYPE
FROM sys.server_principals
WHERE TYPE IN ('S', 'U', 'G');
GO

CREATE LOGIN testing WITH PASSWORD = 'testing', CHECK_POLICY = OFF;
GO
CREATE DATABASE db_testing
GO
USE db_testing
GO
CREATE USER testing FOR LOGIN testing;
GO
EXEC sp_addrolemember 'db_owner', 'testing';
```

## Tests

## Obs

### Final Structure
```
Portafolio_.NETCore7.sln
   ├── ClassLibraryApplication/
   │   ├── DTOs/
   │   │   ├── CoreDTO.cs
   │   │   ├── LanguageDTO.cs
   │   │   ├── LoginDTO.cs
   │   │   ├── ProjectDTO.cs
   │   │   ├── ProjectLanguageDTO.cs
   │   │   ├── ProjectsDTO.cs
   │   │   ├── ProjectTechnologyDTO.cs
   │   │   ├── RegisterDTO.cs
   │   │   ├── ResponseApiDTO.cs
   │   │   ├── ResponseSqlDTO.cs
   │   │   ├── TechnologyDTO.cs
   │   │   ├── UrlDTO.cs
   │   │   ├── UrlGrpDTO.cs
   │   │   ├── UrlsGrpsDTO.cs
   │   │   ├── UserDTO.cs
   │   │   └── YoutubeDTO.cs
   │   ├── Entities/
   │   │   └── UserEntity.cs
   │   ├── Filters/
   │   ├── Interfaces/
   │   │   ├── IAuthService.cs
   │   │   ├── IBaseCRUDService.cs
   │   │   ├── ICoreService.cs
   │   │   ├── IPasswordService.cs
   │   │   ├── IPublicService.cs
   │   │   └── ISimpleCRUDService.cs
   │   └── Services/
   │       ├── AuthService.cs
   │       ├── CoreService.cs
   │       ├── LanguageService.cs
   │       ├── PasswordService.cs
   │       ├── ProjectLanguageService.cs
   │       ├── ProjectService.cs
   │       ├── ProjectTechnologyService.cs
   │       ├── PublicService.cs
   │       ├── TechnologyService.cs
   │       ├── UrlGrpService.cs
   │       ├── UrlService.cs
   │       └── YoutubeService.cs
   ├── WebApi/
   │   ├── Controllers/
   │   │   ├── AuthService.cs
   │   │   ├── CoreController.cs
   │   │   ├── LanguageController.cs
   │   │   ├── ProjectController.cs
   │   │   ├── ProjectLanguageController.cs
   │   │   ├── ProjectTechnologyController.cs
   │   │   ├── PublicController.cs
   │   │   ├── TechnologyController.cs
   │   │   ├── UrlController.cs
   │   │   ├── UrlGrpController.cs
   │   │   └── YoutubeController.cs
   │   ├── Filters/
   │   │   └── SwaggerApiPadLockFilter.cs
   │   ├── appsettings.json
   │   └── Program.cs
   └── WebApiTests/
       ├── Controllers/
       │   └── AuthControllerTests.cs
       ├── Services/
       │   └── AuthServiceTests.cs
       └── GlobalUsings.cs

```

### Program.cl
```
// Transient IDbConnection (abre una nueva conexión para cada operación)
services.AddTransient<IDbConnection>(sp => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

// Scoped para AuthService si se necesita una instancia por petición HTTP
services.AddScoped<IAuthPassword, AuthPassword>();

// Scoped o Transient AuthService, según tu caso
services.AddScoped<AuthService>();
```