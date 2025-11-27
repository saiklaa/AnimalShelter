namespace Pitomnik.Models
{
    public class Cat : Animal
    {
        public Cat()
        {
            Type = "Cat";
        }

        public override void MakeSound() => Console.WriteLine("Meow");
        
    }
}