# Portafolio WebApi

> [JWT.io](https://jwt.io/) <br>
> [Generate SHA256 Key](https://tools.keycdn.com/sha256-online-generator)

## Dependencies
### ApplicationClassLibrary
```
Microsoft.AspNetCore.Cryptography.KeyDerivation
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
  ├── WebApi/                        (eb API)
  │   ├── Controllers/
  │   ├── Program.cs
  │   └── appsettings.json
  ├── Application/                   (ApplicationClassLibrary)
  │   ├── DTOs/
  │   ├── Entities/
  │   ├── Filters/
  │   ├── Interfaces/
  │   └── Services/
  ├── Domain/                        (Proyecto Class Library)
  │   ├── Entities/
  │   └── ValueObjects/
  ├── Infrastructure/                (Proyecto Class Library)
      ├── Data/
      ├── Repositories/
      └── ExternalServices/
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

// Servicio Conexion -------------------------------------------------
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaSQL"))
);

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

// Swagger as Default  -----------------------------------------------
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

// Cors --------------------------------------------------------------
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
    options.SetIsOriginAllowed(origin => true);
});

// Use JWT -----------------------------------------------------------
app.UseAuthentication();
// -------------------------------------------------------------------
app.UseAuthorization();

```

# Tests

## Structure
```
WebApiTest/
│
├── GlobalUsings.cs
│
├── Controllers/
│   ├── AuthControllerTests.cs
│   └── AnotherControllerTests.cs
│
├── Services/
│   ├── AuthServiceTests.cs
│   └── AnotherServiceTests.cs
│
└── UnitTest1.cs
```

# Obs

### Program.cl
```
// Transient IDbConnection (abre una nueva conexión para cada operación)
services.AddTransient<IDbConnection>(sp => new SqlConnection(Configuration.GetConnectionString("DefaultConnection")));

// Scoped para AuthService si se necesita una instancia por petición HTTP
services.AddScoped<IAuthPassword, AuthPassword>();

// Scoped o Transient AuthService, según tu caso
services.AddScoped<AuthService>();
```