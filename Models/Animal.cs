using Pitomnik.Interfaces;

namespace Pitomnik.Models
{
    public abstract class Animal : IFeedable
    {
        public string Name { get; set; } = "Unnamed";
        public int Age { get; set; }
        public string Type { get; protected set; } = "Generic";

        public abstract void MakeSound();
        public abstract void Feed();
    }
}