using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly List<Visit> _visits = [];

    public bool Add(IAnimalRepository animalRepository, Visit visit)
    {
        if (animalRepository.Get(visit.AnimalId) == null) return false;
        _visits.Add(visit);
        return true;
    }

    public IEnumerable<Visit>? GetForAnimal(IAnimalRepository animalRepository, int animalId)
    {
        return animalRepository.Get(animalId) == null ? null : _visits.FindAll(visit => visit.AnimalId == animalId);
    }
}