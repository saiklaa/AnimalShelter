namespace Pitomnik.Models
{
    public class Bird : Animal
    {
        public Bird()
        {
            Type = "Bird";
        }

        public override void MakeSound() => Console.WriteLine("Tweet");
        
    }
}