
namespace FileSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            var server = new MainServer();
            // while (true)
            // {
            //     server.ChooseCommand(Console.ReadLine());
            // }
            server.ChooseCommand("/exec /Users/annakomova/Desktop/TechProg/FilesNodes/commands2.txt");
        }
    }
}