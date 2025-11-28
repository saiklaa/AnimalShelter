using Pitomnik.Models;

public class Shelter
    {
        private readonly List<Animal> _animals = new();
        public void AddAnimal (Animal animal) => _animals.Add(animal);
        public IReadOnlyList<Animal> GetAll() => _animals.AsReadOnly();

        public void Sound()
        {
            foreach ( var animal in _animals)
            {
                Console.Write($"{animal.Name} says: ");
                animal.MakeSound();
            }
        }
        public bool RemoveAnimal(string name)
        {
            var animal = _animals.FirstOrDefault(a => !string.IsNullOrEmpty(a.Name) && 
                                                    a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (animal != null)
            {
                _animals.Remove(animal);
                return true;
            }
            return false;
        }
    }