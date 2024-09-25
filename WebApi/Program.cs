using ApplicationClassLibrary.Interfaces;
using ApplicationClassLibrary.Services;
using BibliotecaPortafolio.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Data;
using System.Text;
using WebApi.Services;
using WebApi.Services.Imp;
using WebApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Dependency injection ----------------------------------------------
builder.Services.AddScoped<IPasswordService, PasswordService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ISecretService, SecretService>();

// Servicio Conexion -------------------------------------------------
builder.Services.AddTransient<IDbConnection>(options =>
    new SqlConnection(builder.Configuration.GetConnectionString("RutaWebSQL"))
);
// -------------------------------------------------------------------

builder.Services.AddTransient<IBaseService<EnlaceGrpDTO_Get, EnlaceGrpDTO_PostPut>, EnlaceGrpService>();
builder.Services.AddTransient<IBaseService<EnlaceDTO_Get, EnlaceDTO_PostPut>, EnlaceService>();
builder.Services.AddTransient<IBaseService<YoutubeDTO_Get, YoutubeDTO_PostPut>, YoutubeService>();
builder.Services.AddTransient<IBaseService<ProyectoDTO_Get, ProyectoDTO_PostPut>, ProyectoService>();
builder.Services.AddTransient<IBaseService<TecnologiaDTO_Get, TecnologiaDTO_PostPut>, TecnologiaService>();
builder.Services.AddTransient<IBaseService<LenguajeDTO_Get, LenguajeDTO_PostPut>, LenguajeService>();
builder.Services.AddTransient<ISingleService<ProTecDTO>, ProTecService>();
builder.Services.AddTransient<ISingleService<ProLengDTO>, ProLengService>();


// Servicio JWT ------------------------------------------------------
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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// Modificar Servicio Swagger ----------------------------------------
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// swagger as Default  -----------------------------------------------
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("./swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseSwagger(options =>
{
    options.SerializeAsV2 = true;
});

// Cors --------------------------------------------------------------
app.UseCors(options =>
{
    options.AllowAnyHeader();
    options.AllowAnyMethod();
    options.AllowCredentials();
    options.SetIsOriginAllowed(origin => true);
});
// -------------------------------------------------------------------

app.UseHttpsRedirection();

// Usar JWT ----------------------------------------------------------
app.UseAuthentication();
// -------------------------------------------------------------------
app.UseAuthorization();

app.MapControllers();

app.Run();
