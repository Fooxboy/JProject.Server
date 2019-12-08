using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.TestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            int port = 2021;
            string address = "127.0.0.1";


            var point = new IPEndPoint(IPAddress.Parse(address), port);
            var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(point);

            while(true)
            {
                string message = Console.ReadLine();
                byte[] data = Encoding.Unicode.GetBytes(message);
                socket.Send(data);

                // получаем ответ
                data = new byte[256];
                StringBuilder builder = new StringBuilder();
                int bytes = 0;

                do
                {
                    bytes = socket.Receive(data, data.Length, 0);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (socket.Available > 0);
                Console.WriteLine("ответ сервера: " + builder.ToString());
            }
           

        }
    }
}
