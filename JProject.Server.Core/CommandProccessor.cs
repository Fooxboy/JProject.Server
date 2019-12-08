using JProject.Server.Core.Helpers;
using JProject.Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JProject.Server.Core
{
    public class CommandProccessor:SocketHelper
    {
        public List<ICommand> Commands { get; set; }

        public CommandProccessor(Socket socket):base(socket)
        {
            Commands = new List<ICommand>();
        }

        public CommandProccessor SetCommands(params ICommand[] commands)
        {
            Commands = commands.ToList();
            return this;
        }


        public CommandProccessor Run(string message)
        {
            Task.Run(() =>
            {
                ICommand command;
                try
                {
                    command = Commands.Single(c => c.Trigger == message.Split(';')[0]);
                }catch
                {
                    Send("error;3");
                    return;
                }

                try
                {
                    var result = command.Execute(message);
                    Send(result);
                }catch(Exception e)
                {
                    Send($"error;4;{e.Message}");
                    Console.WriteLine($"Произошла ошибка при выполнении команды {command.Trigger}: \n {e}");
                    return;
                }

                
            });
            return this;
        }
    }
}
