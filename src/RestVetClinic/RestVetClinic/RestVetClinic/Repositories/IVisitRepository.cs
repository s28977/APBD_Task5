using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public interface IVisitRepository
{
    void Add(Visit visit);
    IEnumerable<Visit>? GetForAnimal(IAnimalRepository animalRepository, int animalId);
}