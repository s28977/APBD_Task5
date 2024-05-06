using RestVetClinic.Models;
using RestVetClinic.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();   
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/animals", (IAnimalRepository animalRepository, Animal animal) => animalRepository.Add(animal)
    ? Results.StatusCode(StatusCodes.Status201Created)
    : Results.BadRequest($"Animal with id {animal.Id} already exists")).WithName("AddAnimal").WithOpenApi();


app.MapGet("/animals", (IAnimalRepository animalRepository) => Results.Ok(animalRepository.GetAll()))
    .WithName("GetAnimals")
    .WithOpenApi();

app.MapGet("/animals/{id:int}", (IAnimalRepository animalRepository, int id) =>
{
    var animal = animalRepository.Get(id);
    return (animal == null) ? Results.NotFound($"Animal with id {id} was not found") : Results.Ok(animal);
}).WithName("GetAnimal").WithOpenApi();



app.Run();