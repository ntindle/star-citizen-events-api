using SCEAPI;
using SCEAPI.Data;
using SCEAPI.Repository;
using SCEAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;
using kDg.FileBaseContext;
using kDg.FileBaseContext.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


// Configure DB
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseFileBaseContextDatabase("events", Directory.GetCurrentDirectory()), ServiceLifetime.Singleton);

// Configure Repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Add the controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Version = "v1",
        Title = "Star Citizen Events API",
        Description = "An API that returns a historical list of Star Citizen Events",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Contact via GitHub",
            Url = new Uri("https://github.com/ntindle/star-citizen-events-api")
        },
        License = new Microsoft.OpenApi.Models.OpenApiLicense
        {
            Name = "CHECK GITHUB FOR LICENSE",
            Url = new Uri("https://github.com/ntindle/star-citizen-events-api/")
        }

    });
    options.EnableAnnotations();

}
);

var app = builder.Build();

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "SCEAPI v1");
    options.RoutePrefix = "";
});
// }

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
