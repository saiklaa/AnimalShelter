using Pitomnik.Models;

public class Shelter
    {
        private readonly List<Animal> _animals = new();
        public void AddAnimal(Animal animal) => _animals.Add(animal);
        public List<Animal> GetAll() => _animals;

        public void Sound()
        {
            foreach ( var animal in _animals)
            {
                Console.Write($"{animal.Name} says: ");
                animal.MakeSound();
            }
        }
    }