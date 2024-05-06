using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public interface IVisitRepository
{
    bool Add(IAnimalRepository animalRepository, Visit visit);
    IEnumerable<Visit>? GetForAnimal(IAnimalRepository animalRepository, int animalId);
}