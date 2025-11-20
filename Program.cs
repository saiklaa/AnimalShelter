using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Reflection;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic;


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
                Console.WriteLine("1. Add Animal.\n2. Show All.\n3. Listen to all.\n4. Exit");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1": Add(); break;
                    case "2": ShowAll(); break;
                    case "3": AnimalShelter.Sound(); break;
                    case "4": return;
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
            Console.WriteLine("Animal:");
            var _type = Console.ReadLine();
            Console.WriteLine("Name:");
            var _name = Console.ReadLine();
            Console.WriteLine("Age: ");
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