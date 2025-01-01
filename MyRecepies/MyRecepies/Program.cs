using Microsoft.Extensions.DependencyInjection;
using MyRecipes.Authentification.Application.Extensions;
using Microsoft.AspNetCore.Identity;
using MyRecipes.Authentification.Domain.Entities;
using MyRecipes.Authentification.Repository.EF.DbContext;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Ajout des Services lié a la solution

//builder.Services.AddRecipiesEx(builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Extension lié a l'authentification
builder.Services.AddAuthentificationEx(connectionString);

WebApplication app = builder.Build();

app.AddMapIdentityApi();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();