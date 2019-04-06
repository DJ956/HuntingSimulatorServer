using System;
using System.Threading.Tasks;

namespace HuntingSimulatorServer
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var server = new Server("127.0.0.1", 6000);
            await server.StartAsync();
            

            Console.ReadKey();
        }
    }
}
