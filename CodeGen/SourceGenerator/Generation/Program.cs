using MyEntities;
using JavaWeb;

namespace Generation
{
    public class Program
    {
        static void Main(string[] args)
        {
            var cat = new Cat();
            var owner = new Owner();
            var client = new GeneratedClient();
            Console.WriteLine(client.FindCatById(1));
        }
    }
}