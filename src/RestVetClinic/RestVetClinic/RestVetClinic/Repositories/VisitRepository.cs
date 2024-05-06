using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public class VisitRepository : IVisitRepository
{
    private readonly IAnimalRepository _animalRepository;
    private readonly List<Visit> _visits = [];

    public VisitRepository(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }

    public bool Add(Visit visit)
    {
        if (_animalRepository.Get(visit.AnimalId) == null) return false;
        _visits.Add(visit);
        return true;
    }

    public IEnumerable<Visit>? GetForAnimal(int animalId)
    {
        return _animalRepository.Get(animalId) == null ? null : _visits.FindAll(visit => visit.AnimalId == animalId);
    }
}