using System.Text;
using System.Text.Json.Serialization;
using LearningTogether.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var myCorsPolicy = "MyCorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// enabling CORS requests - Lea
builder.Services.AddCors(options =>
{
    options.AddPolicy(myCorsPolicy,
        policy  =>
        {
            policy.WithOrigins("http://localhost:3000") //AllowAnyOrigin() 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// disables possible reference loops when sending JSON files
builder.Services.AddControllersWithViews().AddJsonOptions(options =>
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles
    );
//builder.Services.AddMvc();

// setting up the token authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();     
}

// setting the response header

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;
app.UseAuthorization();

/*
 * Returns the index.html file from the frontend if the requested route does not
 * start with /api
 *                                      Mihael
*/
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.UseCors(myCorsPolicy);        // CORS enabling

// Data initialization - Lea
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        // filling the db with data specified in Data/DbInitializer
        var context = services.GetRequiredService<ApplicationDbContext>();
        DbInitializer.Initialize(context); 
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while seeding the database.");
    }
}

app.Run();


/*
- VS migrations: 1. add-migration name    2. update-database
- for VS in appsettings:     "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=LearningTogether;Trusted_Connection=True;MultipleActiveResultSets=true"
- for Linux in appsetings: "DefaultConnection": "Server=localhost,1433;Database=LearningTogether;User Id=;Password=;Trusted_Connection=False;MultipleActiveResultSets=true"
- reverting migrations: dotnet ef database update "MigrationNameYouWantToRevertTo"
*/