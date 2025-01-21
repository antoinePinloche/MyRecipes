using MyRecipes.Authentification.Application.Extensions;
using MyRecipes.Recipes.Application.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(Program).Assembly));

builder.Services.AddEndpointsApiExplorer();
string? connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//Extension lié a l'authentification
builder.Services.AddAuthentificationEx(connectionString);

//Extension lié a l'application
builder.Services.AddRecipesEx(connectionString);

WebApplication app = builder.Build();

//Extension DB Auth
app.AuthentificationDataBaseCreateOrUpdate();
app.RecipeDataBaseCreateOrUpdate();

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