using JProject.Server.ShellConsole.Models;
using System;

namespace JProject.Server.ShellConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Старт сервера по адресу 127.0.0.1:2021");
            var server = new Core.Server("127.0.0.1", 2021, new Database());
            server.Start();


            //защита от дурака.
            while(true)
            {
                Console.ReadLine();
            }
        }
    }
}
