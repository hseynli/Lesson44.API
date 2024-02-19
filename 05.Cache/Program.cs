using _04.GenericRepository.Models;
using _05.Cache.Services;
using DbContexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

string connection = "Server=(localdb)\\mssqllocaldb;Database=GenericRepository;Trusted_Connection=True;";
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));

builder.Services.AddTransient<UserService>();
// Keşləmə xidmətini əlavə edirik
builder.Services.AddMemoryCache();

builder.Services.AddControllers();
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

app.MapGet("/user/{id}", async (int id, UserService userService) =>
{
    User user = await userService.GetUser(id);
    if (user != null) return $"User {user.Name}  Id={user.Id}";
    return "User not found";
});
app.MapGet("/", () => "Hello World!");

app.Run();
