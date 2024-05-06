using RestVetClinic.Models;

namespace RestVetClinic.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAll();
    Animal? Get(int id);
    bool Add(Animal animal);
    bool Edit(int id, Animal animal);
    bool Delete(int id);
}