using BraveHeartBackend.Data;
using BraveHeartBackend.Models;
using BraveHeartBackend.Services;
using DotNetEnv;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Serilog;
using CloudinaryDotNet;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env
if (builder.Environment.IsDevelopment())
{
    DotNetEnv.Env.Load(".env.local");
    Console.WriteLine("Loading .env.local ----------------------------------");
    Console.WriteLine(Environment.GetEnvironmentVariable("DB_HOST"));
}


// ✅ Use Cloud Run env vars, no fallback to localhost
string dbHost = Environment.GetEnvironmentVariable("DB_HOST")!;
string dbPort = Environment.GetEnvironmentVariable("DB_PORT")!;
string dbName = Environment.GetEnvironmentVariable("DB_NAME")!;
string dbUser = Environment.GetEnvironmentVariable("DB_USER")!;
string dbPass = Environment.GetEnvironmentVariable("DB_PASS")!;
string sslMode = Environment.GetEnvironmentVariable("DB_SSL_MODE") ?? "Disable";

Console.WriteLine($"🧪 Connecting to DB at {dbHost}:{dbPort}");

string dbConnStr = $"Host={dbHost};Port={dbPort};Database={dbName};Username={dbUser};Password={dbPass};SSL Mode={sslMode}";

// Register PostgreSQL DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(dbConnStr));

// Add Identity with roles
builder.Services.AddDefaultIdentity<ApplicationUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

// Load JWT settings from .env
string jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER")!;
string jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE")!;
string jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET_KEY")!;
string jwtExpiry = Environment.GetEnvironmentVariable("JWT_EXPIRY_MINUTES") ?? "60";

// Configure JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret))
    };
});

// Add services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register JWT Service
builder.Services.AddScoped<JwtService>();

// Add CORS policy
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            policy => policy.WithOrigins("http://localhost:3000", "http://localhost:3001", "http://localhost:5173", "http://localhost:8080")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials());
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            policy => policy.WithOrigins("https://dallss.github.io/")
                              .AllowAnyHeader()
                              .AllowAnyMethod()
                              .AllowCredentials());
    });
}

// Register CloudinaryService
builder.Services.AddSingleton<CloudinaryService>(sp =>
{
    var config = new CloudinaryConfig
    {
        CloudName = Environment.GetEnvironmentVariable("CLOUDINARY_CLOUD_NAME")!,
        ApiKey = Environment.GetEnvironmentVariable("CLOUDINARY_API_KEY")!,
        ApiSecret = Environment.GetEnvironmentVariable("CLOUDINARY_API_SECRET")!
    };
    return new CloudinaryService(config);
});

// Configure Serilog for logging
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .CreateLogger();

// Replace default logger
builder.Host.UseSerilog();

var app = builder.Build();

// Swagger UI for dev
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();
app.UseCors("AllowFrontend"); // Enable CORS before authentication
app.UseAuthentication(); // ✅ JWT Auth
app.UseAuthorization();

app.MapControllers();

// Create Admin account on startup
await CreateAdminAccount(app);

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
app.Urls.Add($"http://*:{port}");

app.Run();

async Task CreateAdminAccount(WebApplication app)
{
    using var scope = app.Services.CreateScope();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

    string adminEmail = Environment.GetEnvironmentVariable("ADMIN_EMAIL") ?? "admin@braveheart.com";
    string adminPassword = Environment.GetEnvironmentVariable("ADMIN_PASSWORD") ?? "BraveheartAdmin123!";
    string adminRole = "Admin";

    if (!await roleManager.RoleExistsAsync(adminRole))
    {
        await roleManager.CreateAsync(new IdentityRole(adminRole));
        Console.WriteLine($"Admin Email: {adminEmail}, Password: {adminPassword}");
    }

    var adminUser = await userManager.FindByEmailAsync(adminEmail);
    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = adminEmail,
            Email = adminEmail,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(adminUser, adminPassword);
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, adminRole);
            Console.WriteLine($"✅ Admin user created: {adminEmail}");
        }
        else
        {
            Console.WriteLine($"❌ Failed to create admin: {string.Join(", ", result.Errors.Select(e => e.Description))}");
        }
    }
    else if (!await userManager.IsInRoleAsync(adminUser, adminRole))
    {
        await userManager.AddToRoleAsync(adminUser, adminRole);
        Console.WriteLine($"ℹ️ Admin role assigned to existing user: {adminEmail}");
    }
}

public class CloudinaryConfig
{
    public string CloudName { get; set; } = string.Empty;
    public string ApiKey { get; set; } = string.Empty;
    public string ApiSecret { get; set; } = string.Empty;
}

// Add Google.Apis.Auth for Google Sign-In support
// dotnet add package Google.Apis.Auth
