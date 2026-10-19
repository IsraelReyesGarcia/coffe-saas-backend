using System.Text;
using Amazon.Runtime;
using Amazon.S3;
using Cafeteria.Constants;
using Cafeteria.Models;
using Cafeteria.Models.Enums;
using Cafeteria.Options;
using Cafeteria.Repository;
using Cafeteria.Repository.IRepository;
using Cafeteria.Services;
using Cafeteria.Services.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using Microsoft.Extensions.Options;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi(options =>
{
    options.AddDocumentTransformer((document, context, cancellationToken) =>
    {
        document.Components ??= new OpenApiComponents();
        document.Components.SecuritySchemes ??= new Dictionary<string, IOpenApiSecurityScheme>();
        document.Components.SecuritySchemes["Bearer"] = new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.Http,
            Scheme = "bearer",
            BearerFormat = "JWT",
            Description = "Ingresa el token JWT generado en el login."
        };
        return Task.CompletedTask;
    });
});
builder.Services.AddEndpointsApiExplorer(); //
var dbConectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseNpgsql(dbConectionString));

//Configuración para generar respuestas de caché
builder.Services.AddResponseCaching(options =>
{
    options.MaximumBodySize = 1024 * 1024;
    options.UseCaseSensitivePaths = true;
});

builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
builder.Services.AddScoped<IProductCategorieRepository, ProductCategorieRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ITableRepository, TableRepository>();
builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IMenuTimeRepository, MenuTimeRepository>();
builder.Services.AddScoped<IMenuRepository, MenuRepository>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<IUnitOfMeasurementRepository, UnitOfMeasurementRepository>();
builder.Services.AddScoped<IRawMaterialRepository, RawMaterialRepository>();
builder.Services.AddScoped<IInputMaterialCategoryRepository, InputMaterialCategoryRepository>();
builder.Services.AddScoped<IProductImageRepository, ProductImageRepository>();
builder.Services.Configure<RustFsOptions>(builder.Configuration.GetSection("RustFs"));
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var options = sp.GetRequiredService<IOptions<RustFsOptions>>().Value;

    if (string.IsNullOrWhiteSpace(options.ServiceUrl) ||
        string.IsNullOrWhiteSpace(options.AccessKey) ||
        string.IsNullOrWhiteSpace(options.SecretKey))
    {
        throw new InvalidOperationException("La configuración de RustFs está incompleta (ServiceUrl, AccessKey o SecretKey)");
    }

    var config = new AmazonS3Config
    {
        ServiceURL = options.ServiceUrl,     // http://<ip-droplet>:9000
        ForcePathStyle = true,               // URLs tipo http://host/bucket/archivo
        AuthenticationRegion = "us-east-1",  // RustFS la ignora, pero la firma la necesita
        RequestChecksumCalculation = RequestChecksumCalculation.WHEN_REQUIRED,
        ResponseChecksumValidation = ResponseChecksumValidation.WHEN_REQUIRED
    };

    var credentials = new BasicAWSCredentials(options.AccessKey, options.SecretKey);
    return new AmazonS3Client(credentials, config);
});
builder.Services.AddSingleton<IImageProcessor, ImageSharpProcessor>();
builder.Services.AddSingleton<IObjectStorage, RustFsStorage>();
builder.Services.AddScoped<IProductImageService, ProductImageService>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

// Registro del servicio de Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>()
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

var secretKey = builder.Configuration.GetValue<string>("ApiSettings:SecretKey");

if (string.IsNullOrEmpty(secretKey))
{
    throw new InvalidOperationException("La secretKey no está configurada");
}
//Agregar el servicio de autenticación
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false; // No usa https (pruebas) no para producción
    options.SaveToken = true; // Guarda el token en el contexto de autenticación

    //Parámetros de validación del Token
    options.TokenValidationParameters = new TokenValidationParameters
    {
      ValidateIssuerSigningKey = true,
      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),  // Verificamos que el token esté firmada con la secretKey que definimos
      ValidateIssuer = false, // Define si se valida el emisor del token
      ValidateAudience = false // Define si se debe restringir a ciertos clientes
    };
});

// Agregamos configuraciones de perfiles de caché
builder.Services.AddControllers(option =>
{
    option.CacheProfiles.Add(CacheProfiles.Default10, CacheProfiles.Profile10);

    option.CacheProfiles.Add(CacheProfiles.Default20, CacheProfiles.Profile20);
});

//Definimos reglas de CORS
builder.Services.AddCors(options =>
    {
        options.AddPolicy(PolicyNames.AllowEspecificOrigin,
        builder => {
            builder.WithOrigins("http://localhost:3000").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
        }
        );
    }
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

//app.UseHttpsRedirection(); // Comentado temporalmente para desarrollo
var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.UseCors(PolicyNames.AllowEspecificOrigin);

app.UseResponseCaching();

// Definirmos el middleware de autenticación
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
