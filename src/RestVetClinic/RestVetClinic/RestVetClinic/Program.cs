using RestVetClinic.Models;
using RestVetClinic.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IAnimalRepository, AnimalRepository>();
builder.Services.AddSingleton<IVisitRepository, VisitRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/animals", (IAnimalRepository animalRepository, Animal animal) => animalRepository.Add(animal)
    ? Results.Created()
    : Results.BadRequest($"Animal with id {animal.Id} already exists.")).WithName("AddAnimal").WithOpenApi();


app.MapGet("/animals", (IAnimalRepository animalRepository) => Results.Ok(animalRepository.GetAll()))
    .WithName("GetAnimals")
    .WithOpenApi();

app.MapGet("/animals/{id:int}", (IAnimalRepository animalRepository, int id) =>
{
    var animal = animalRepository.Get(id);
    return (animal == null) ? Results.NotFound($"Animal with id {id} was not found.") : Results.Ok(animal);
}).WithName("GetAnimal").WithOpenApi();

app.MapDelete("/animals/{id:int}", (IAnimalRepository animalRepository, int id) => animalRepository.Delete(id)
    ? Results.NoContent()
    : Results.NotFound($"Animal with id {id} was not found.")).WithName("DeleteAnimal").WithOpenApi();

app.MapPut("/animals/{id:int}", (IAnimalRepository animalRepository, int id, Animal animal) =>
        animalRepository.Edit(id, animal)
            ? Results.NoContent()
            : Results.BadRequest(
                $"Animal with id {id} was not found or the id in url segment doesn't match the id in http body."))
    .WithName("EditAnimal").WithOpenApi();
    
app.MapPost("/visits", (IAnimalRepository animalRepository, IVisitRepository visitRepository, Visit visit) => visitRepository.Add(animalRepository, visit)
    ? Results.Created()
    : Results.NotFound($"Animal with id {visit.AnimalId} was not found.")).WithName("AddVisit").WithOpenApi();

app.MapGet("/visits/{animalId:int}", (IAnimalRepository animalRepository,  IVisitRepository visitRepository, int animalId) =>
{
    var visit = visitRepository.GetForAnimal(animalRepository, animalId);
    return (visit == null) ? Results.NotFound($"Animal with id {animalId} was not found.") : Results.Ok(visit);
}).WithName("GetVisit").WithOpenApi();

app.Run();