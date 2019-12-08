using JProject.Server.Core.Helpers;
using JProject.Server.Core.Interfaces;
using JProject.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.Core.Commands
{
    public class Login
    {
        private readonly IDatabase _database;
        public Login(IDatabase database)
        {
            _database = database;
        }
        public User Start(string login, string password)
        {
            return _database.Login(login, password);
        }
    }
}
