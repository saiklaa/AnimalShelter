namespace Pitomnik.Models
{
    public abstract class Animal
    {
        public string Name { get; set; } = "Unnamed";
        public int Age { get; set; }
        public string Type { get; protected set; } = "Generic";

        public abstract void MakeSound();
    }
}