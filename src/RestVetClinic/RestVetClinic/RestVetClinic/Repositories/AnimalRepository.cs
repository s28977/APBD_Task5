using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly List<Animal> _animals = [];
    public bool Add(Animal animal)
    {
        if (_animals.Exists(a => a.Id == animal.Id)) return false;
        _animals.Add(animal);
        return true;
    }

    public bool Delete(int id)
    {
        var animalToDelete = _animals.FirstOrDefault(a => a.Id == id);
        if (animalToDelete == null) return false;
        _animals.Remove(animalToDelete);
        return true;
    }

    public bool Edit(int id, Animal animal)
    {
        if (id != animal.Id) return false;
        var animalToEdit = _animals.FirstOrDefault(a => a.Id == id);
        if (animalToEdit == null) return false;
        _animals.Remove(animalToEdit);
        _animals.Add(animal);
        return true;
    }

    public Animal? Get(int id)
    {
        var specificAnimal = _animals.FirstOrDefault(a => a.Id == id);
        return specificAnimal;
    }

    public IEnumerable<Animal>GetAll()
    {
        return _animals.AsEnumerable();
    }
}