namespace Pitomnik.Models
{
    public class Dog : Animal
    {
        public Dog()
        {
            Type = "Dog";
        }

        public override void MakeSound() => Console.WriteLine("Woof");
        
    }
}