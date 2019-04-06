using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace HuntingSimulatorServer
{
    public class ClientHanlder
    {
        private Encoding Encoding = Encoding.UTF8;

        public TcpClient Client { get; private set; }
        public ClientHanlder(TcpClient client)
        {
            Client = client;
        }


        public async Task Send(string msg)
        {
            var stream = Client.GetStream();
            var data = Encoding.GetBytes(msg);

            await stream.WriteAsync(data, 0, data.Length); ;
        }

        public async Task<string> Receive()
        {                        
            using (var memory = new MemoryStream())
            {
                var stream = Client.GetStream();
                var bufer = new byte[4096];
                do
                {
                    await stream.ReadAsync(bufer, 0, bufer.Length);
                    await memory.WriteAsync(bufer, 0, bufer.Length);
                } while (stream.DataAvailable);
                var result = Encoding.GetString(memory.GetBuffer());
                return result;
            }
        }
    }
}
