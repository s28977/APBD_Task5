using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public interface IVisitRepository
{
    bool Add(Visit visit);
    IEnumerable<Visit>? GetForAnimal(int animalId);
}