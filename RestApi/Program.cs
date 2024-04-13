using Microsoft.OpenApi.Models;
using System.Text.Json;

var animals = new List<Animal>();
var visits = new List<Visit>();
var nextAnimalId = 1;
var nextVisitId = 1;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "clinic API", Version = "January2000" });
});

var app = builder.Build();
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "clinic API");
});

app.MapGet("/animals", () =>
{
    var json = JsonSerializer.Serialize(animals);
    return Results.Ok(json);
});

app.MapGet("/animals/{id}", (int id) =>
{
    var animal = animals.Find(a => a.Id == id);
    if (animal != null)
    {
        var json = JsonSerializer.Serialize(animal);
        return Results.Ok(json);
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPost("/animals", async (HttpContext context) =>
{
    var animal = await JsonSerializer.DeserializeAsync<Animal>(context.Request.Body);
    animal.Id = nextAnimalId++;
    animals.Add(animal);
    return Results.Created($"/animals/{animal.Id}", JsonSerializer.Serialize(animal));
});

app.MapPut("/animals/{id}", async (int id, HttpContext context) =>
{
    var animalIndex = animals.FindIndex(a => a.Id == id);
    if (animalIndex != -1)
    {
        var updatedAnimal = await JsonSerializer.DeserializeAsync<Animal>(context.Request.Body);
        updatedAnimal.Id = id;
        animals[animalIndex] = updatedAnimal;
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapDelete("/animals/{id}", (int id) =>
{
    var animalIndex = animals.FindIndex(a => a.Id == id);
    if (animalIndex != -1)
    {
        animals.RemoveAt(animalIndex);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapGet("/animals/{id}/visits", (int id) =>
{
    var animalVisits = visits.FindAll(v => v.AnimalId == id);
    var json = JsonSerializer.Serialize(animalVisits);
    return Results.Ok(json);
});

app.MapPost("/visits", async (HttpContext context) =>
{
    var visit = await JsonSerializer.DeserializeAsync<Visit>(context.Request.Body);
    visit.Id = nextVisitId++;
    visits.Add(visit);
    return Results.Created($"/visits/{visit.Id}", JsonSerializer.Serialize(visit));
});

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "clinic API");
});

app.MapControllers();


app.Run();

public record Animal
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Category { get; set; }
    public float Weight { get; set; }
    public string FurColor { get; set; }
}

public record Visit
{
    public int Id { get; set; }
    public DateTime VisitDate { get; set; }
    public int AnimalId { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
}
