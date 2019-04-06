using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace HuntingSimulatorServer
{
    public class Server
    {
        public IPAddress Address{ get; private set; }
        public int Port { get; private set; }
        private TcpListener listener;

        public Server(string address, int port)
        {
            Address = IPAddress.Parse(address);
            Port = port;
        }

        public async Task StartAsync()
        {
            listener = new TcpListener(Address, Port);
            listener.Start();
            Console.WriteLine("Server Start");
            Console.WriteLine($"Address:{Address.ToString()} Port:{Port}");

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                
                Console.WriteLine("Accept client");
                Console.WriteLine($"Address:{client.Client.AddressFamily} Port:{client.Client.LocalEndPoint}");
                
                var handler = new ClientHanlder(client);

                var msg = await handler.Receive();
                Console.WriteLine($"Received message:{msg}");
                await handler.Send(msg + " from server");
                
            }
        }

        public void Stop()
        {
            listener.Stop();
            Console.WriteLine("Server stop");
        }
    }
}
