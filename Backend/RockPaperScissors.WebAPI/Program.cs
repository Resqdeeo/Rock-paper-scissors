using RockPaperScissors.Persistence.MigrationTools;
using RockPaperScissors.WebAPI.Configurations;

var builder = WebApplication.CreateBuilder(args);

MongoConfiguration.AddMongoDb(builder.Services, builder.Configuration);
AuthConfiguration.AddAuth(builder.Services, builder.Configuration);
PostgreConfiguration.AddDatabase(builder.Services, builder.Configuration);
DependencyInjectionConfiguration.AddApplicationServices(builder.Services);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using var scoped = app.Services.CreateScope();
var migrator = scoped.ServiceProvider.GetRequiredService<Migrator>();
await migrator.MigrateAsync();


app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();

app.UseHttpsRedirection();

app.UseCors(b => b
    .WithOrigins("http://localhost:5173") 
    .AllowAnyMethod()                     
    .AllowAnyHeader()                      
    .AllowCredentials());

app.Run();
