namespace Pitomnik.Models
{
    public class Dog : Animal
    {
        public Dog()
        {
            Type = "Dog";
        }

        public override void MakeSound() => Console.WriteLine("Woof");
        public override void Feed() => Console.WriteLine($"{Name} eats dog food and wags tail!");
    }
}