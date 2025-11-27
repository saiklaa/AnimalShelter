    using Pitomnik.Models;
    using System.Text.Json;
    namespace Pitomnik.Service;

    public class ShelterApp
    {
        private readonly Shelter _animalShelter = new();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Animal.\n2. Show All.\n3. Listen to all.\n4. Delete animal\n5. Show by type\n6. Show JSON\n7. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Add(); break;
                    case "2": ShowAll(); break;
                    case "3": _animalShelter.Sound(); break;
                    case "4": Delete(); break;
                    case "5": ShowByType(); break;
                    case "6": ShowJSON(); break;
                    case "7": return;
                    default: Console.WriteLine("Unknown option."); break;
                }
            }
        }


        private Animal CreateAnimal(string type, string name, int age)
        {
            Animal animal = type.ToLower() switch
            {
                "dog" => new Dog(),
                "cat" => new Cat(),
                "bird" => new Bird()
            };
            animal.Name = name!;
            animal.Age = age!;
            return animal;
        }
        private void Add()
        {
            Console.Write("Animal: ");
            var _type = Console.ReadLine();
            Console.Write("Name: ");
            var _name = Console.ReadLine();
            Console.Write("Age: ");
            int _age = int.Parse(Console.ReadLine());
            var animal = CreateAnimal(_type, _name, _age);
            _animalShelter.AddAnimal(animal);
            Console.WriteLine("Animal added successfully.");
        }

        private void ShowAll()
        {
            var animal = _animalShelter.GetAll();
            animal.ForEach(a => Console.Write($"{a.Name} - {a.Age}\n")); 
        }

        private void Delete()
        {
            Console.Write("Enter name to delete: ");
            var name = Console.ReadLine();
            Animal FoundAnimal = null;

            foreach (var animal in _animalShelter.GetAll())
            {
                if (animal.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    FoundAnimal = animal;
                    break;
                }
            }
            if (FoundAnimal != null)
            {
                _animalShelter.GetAll ().Remove(FoundAnimal);
                Console.WriteLine("Animal deleted successfully.");
            }
            else
            {
                Console.WriteLine("Animal not found.");
            }
        }

        private void ShowByType()
        {
            Console.WriteLine("Enter animal type (Dog, Cat, Bird): ");
            var type = Console.ReadLine();
            var animals = _animalShelter.GetAll().Where(a => a.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            animals.ForEach(a => Console.WriteLine($"{a.Name} - {a.Age}"));
        }

        private void ShowJSON()
        {
            var animals = _animalShelter.GetAll();
            string fileName = "Animals.json";
            string jsonString = JsonSerializer.Serialize(animals);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine(File.ReadAllText(fileName));
        }
    }