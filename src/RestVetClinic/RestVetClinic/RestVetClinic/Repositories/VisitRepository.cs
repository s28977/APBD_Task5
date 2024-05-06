using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly List<Visit> _visits = [];

    public void Add(Visit visit)
    {
        _visits.Add(visit);
    }

    public IEnumerable<Visit>? GetForAnimal(IAnimalRepository animalRepository, int animalId)
    {
        return animalRepository.Get(animalId) == null ? null : _visits.FindAll(visit => visit.Animal.Id == animalId);
    }
}