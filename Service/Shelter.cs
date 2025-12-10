using System.Runtime.InteropServices;
using Pitomnik.Models;
namespace Pitomnik.Service
{
public class Shelter
    {
        private readonly List<Animal> _animals = new();
        public void AddAnimal (Animal animal)
        {
            if (animal == null)
                throw new ArgumentNullException(nameof(animal));
            
            _animals.Add(animal);
        } 
        
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

        public void FeedAll()
        {
            foreach (var animal in _animals)
            {
                Console.Write($"Feeding {animal.Name}: ");
                animal.Feed();
            }
        }

        public void FeedAnimal(string name)
        {
            var animal = _animals.FirstOrDefault(a => a.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if(animal != null)
            {
                Console.Write($"Feeding {animal.Name}: ");
                animal.Feed();
            }
            else Console.WriteLine($"Animal with '{name}' not found.");
        }
    }
}