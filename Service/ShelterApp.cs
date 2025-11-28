using Pitomnik.Models;
using System.Runtime.InteropServices;
using System.Text.Json;

namespace Pitomnik.Service;

    public class ShelterApp
    {
        private readonly Shelter _animalShelter = new();

        public void Run()
        {
            while (true)
            {
                Console.WriteLine("1. Add Animal.\n2. Show All.\n3. Listen to all.\n4. Delete animal\n5. Show by type\n6. Show JSON\n7. Feed all animals\n8. Feed specific animal\n0. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Add(); break;
                    case "2": ShowAll(); break;
                    case "3": _animalShelter.Sound(); break;
                    case "4": Delete(); break;
                    case "5": ShowByType(); break;
                    case "6": ShowJSON(); break;
                    case "7": _animalShelter.FeedAll(); break;
                    case "8": FeedSpecific(); break;
                    case "0": return;
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
            "bird" => new Bird(),
            _ => throw new ArgumentException("Invalid animal type")
            };
            animal.Name = name;
            animal.Age = age; 
            return animal;
        }
        private void Add()
        {
            Console.Write("Animal: ");
            var _type = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(_type))
            {
                Console.WriteLine("Type is required.");
                return;
            }
            Console.Write("Name: ");
            var _name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(_name))
            {
                Console.WriteLine("Name is required: ");
                return;
            }
            Console.Write("Age: ");
            var ageInput = Console.ReadLine();
            if(!int.TryParse(ageInput, out int _age) || _age < 0)
            {
                Console.WriteLine("Invalid age.");
                return;
            }

            try
            {
                var animal = CreateAnimal(_type, _name, _age);
                _animalShelter.AddAnimal(animal);
                Console.WriteLine("Animal added successfully.");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to add animal: {ex.Message}");
            }
        }

        private void ShowAll()
        {
            var animals = _animalShelter.GetAll();
            foreach (var a in animals)
            {
                Console.WriteLine($"{a.Name} - {a.Age}");   
            }
        }   

        private void Delete()
        {
            Console.Write("Enter name to delete: ");
            var _name = Console.ReadLine()?.Trim();
            if (string.IsNullOrWhiteSpace(_name))
            {
                Console.WriteLine("The name is required.");
                return;
            }
            try
            {
                bool removed = _animalShelter.RemoveAnimal(_name);
                Console.WriteLine(removed ? "Animal deleted successfully." : "Animal not found.");
            }
            catch (Exception ex)
            {    
                Console.WriteLine($"Error: {ex.Message}");
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
            try
            {
                string jsonString = JsonSerializer.Serialize(animals);
                File.WriteAllText(fileName, jsonString);
                Console.WriteLine(File.ReadAllText(fileName));
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private void FeedSpecific()
        {
            Console.Write("Enter animal name: ");
            var name = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name is required.");
                    return;
                }
            _animalShelter.FeedAnimal(name);   
        }
    }