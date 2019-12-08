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
    public class CommandProccessor
    {
        public List<ICommand> Commands { get; set; }

        public CommandProccessor()
        {
            Commands = new List<ICommand>();
        }

        public CommandProccessor SetCommands(params ICommand[] commands)
        {
            Commands = commands.ToList();
            return this;
        }


        public string Run(string message)
        {
            ICommand command;
            try
            {
                command = Commands.Single(c => c.Trigger == message.Split(';')[0]);
            }
            catch
            {
                return "error;3";
            }

            try
            {
                var result = command.Execute(message);
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Произошла ошибка при выполнении команды {command.Trigger}: \n {e}");
                return $"error;4;{e.Message}";
            }
        }
    }
}
