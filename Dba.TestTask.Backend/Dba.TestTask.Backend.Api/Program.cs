using Dba.TestTask.Backend.Application;
using Dba.TestTask.Backend.Persistence;
using Dba.TestTask.Backend.Persistence.Factories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks();

builder.Services.AddApplication()
    .AddPersistence(builder.Configuration);

var serviceProvider = builder.Services.BuildServiceProvider();

var factory = serviceProvider.GetService<IDbConnectionFactory>();
DbInitializer.InitializeDb(factory!.CreateConnection());
//DbInitializer.CreateTestRows(factory!.CreateConnection());

var app = builder.Build();

app.MapHealthChecks("/healthcheck");

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