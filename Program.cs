using Pitomnik.Service;

namespace Pitomnik
{
    class Program
    {
        static void Main()
        {
            try
            {
                var app = new ShelterApp();
                app.Run();
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }   
    }
}