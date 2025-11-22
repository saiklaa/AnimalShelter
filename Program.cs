using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;
using Microsoft.VisualBasic.FileIO;
using System.Text.Json;



namespace Pitomnik
{
    public class Animal
    {
        public string Name {get; set;} = "Unnamed"; 
        public int Age {get; set;}
        virtual public void BaseSound() {
            Console.WriteLine("Some generic animal sound");
        }
    }

    class Dog : Animal
    {
        public override void BaseSound() => Console.WriteLine("Woof"); 
    }

    class Cat : Animal
    {
        public override void BaseSound() => Console.WriteLine("Meow");
    }
    class Bird : Animal
    {
        public override void BaseSound() => Console.WriteLine("Tweet");
    }

    public class Shelter
    {
        private readonly List<Animal> _animals = new();
        public void AddAnimal(Animal animal) => _animals.Add(animal);
        public List<Animal> Show() => _animals;

        public void Sound()
        {
            foreach ( var animal in _animals)
            {
                Console.Write($"{animal.Name} says: ");
                animal.BaseSound();
            }
        }
    }

    public class ShelterApp
    {
        private readonly Shelter AnimalShelter = new();

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
                    case "3": AnimalShelter.Sound(); break;
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
            AnimalShelter.AddAnimal(animal);
            Console.WriteLine("Animal added successfully.");
        }

        private void ShowAll()
        {
            var animal = AnimalShelter.Show();
            animal.ForEach(a => Console.Write($"{a.Name} - {a.Age}\n")); 
        }

        private void Delete()
        {
            Console.Write("Enter name to delete: ");
            var name = Console.ReadLine();
            Animal FoundAnimal = null;

            foreach (var animal in AnimalShelter.Show())
            {
                if (animal.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
                {
                    FoundAnimal = animal;
                    break;
                }
            }
            if (FoundAnimal != null)
            {
                AnimalShelter.Show().Remove(FoundAnimal);
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
            var animals = AnimalShelter.Show().Where(a => a.GetType().Name.Equals(type, StringComparison.OrdinalIgnoreCase)).ToList();
            animals.ForEach(a => Console.WriteLine($"{a.Name} - {a.Age}"));
        }

        private void ShowJSON()
        {
            var animals = AnimalShelter.Show();
            string fileName = "Animals.json";
            string jsonString = JsonSerializer.Serialize(animals);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine(File.ReadAllText(fileName));
        }
    }

    class Program
    {
        static void Main()
        {
            var app = new ShelterApp();
            app.Run();
        }
    }
}