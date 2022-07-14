namespace Node
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("enter port:");
            int port = Int32.Parse(Console.ReadLine());
            var server = new ServerToClient("/Users/annakomova/Desktop/TechProg/Nodes", port);
            server.Start();
        }
    }
}