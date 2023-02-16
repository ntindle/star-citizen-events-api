using SCEAPI;
using SCEAPI.Data;
using SCEAPI.Repository;
using SCEAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Configure Logging
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.Console()
    .WriteTo.File("Logs/log.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
builder.Host.UseSerilog();


// Configure DB
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddSerilog()));
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"), options =>
    {
        options.EnableRetryOnFailure();
        options.UseDateOnlyTimeOnly();
    }).EnableDetailedErrors();
});

// Configure Repositories
builder.Services.AddScoped<IEventRepository, EventRepository>();

// Configure AutoMapper
builder.Services.AddAutoMapper(typeof(MappingConfig));

// Add the controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
