using _01.EntityFramework;
using _02.MinimalAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string connection = "Server=(localdb)\\mssqllocaldb;Database=applicationdb;Trusted_Connection=True;";
builder.Services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connection));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseDefaultFiles();
app.UseStaticFiles();

app.MapGet("/api/users", async (ApplicationContext db) => await db.Users.ToListAsync());

app.MapGet("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "İstifadəçi aşkar edilmədi" });

    return Results.Json(user);
});

app.MapDelete("/api/users/{id:int}", async (int id, ApplicationContext db) =>
{
    User user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);

    if (user == null) return Results.NotFound(new { message = "İstifadəçi aşkar edilmədi" });

    db.Users.Remove(user);
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.MapPost("/api/users", async (User user, ApplicationContext db) =>
{
    await db.Users.AddAsync(user);
    await db.SaveChangesAsync();
    return user;
});

app.MapPut("/api/users", async (User userData, ApplicationContext db) =>
{
    var user = await db.Users.FirstOrDefaultAsync(u => u.Id == userData.Id);

    if (user == null) return Results.NotFound(new { message = "İstifadəçi aşkar edilmədi" });

    user.Age = userData.Age;
    user.Name = userData.Name;
    await db.SaveChangesAsync();
    return Results.Json(user);
});

app.Run();