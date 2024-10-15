using Capstone.Collector;
using Capstone.Collector.Services;
using Capstone.Common;
using Capstone.Common.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var configs = builder.Configuration.GetSection("ConnectionStrings");
var dbConnectionString = builder.Configuration.GetConnectionString("Database");
Console.WriteLine($"Database connection string: {dbConnectionString}");
builder.SetupDatabase(dbConnectionString);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<CongressMemberRepository>();
builder.Services.AddScoped<CongressRepository>();
builder.Services.AddScoped<CongressApiService>();

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