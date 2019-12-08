using JProject.Server.Core.Interfaces;
using JProject.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JProject.Server.ShellConsole.Models
{
    public class Database : IDatabase
    {
        public User Login(string nick, string password)
        {
            return new User();
        }

        public User Register(string nick, string password)
        {
            return new User();
        }
    }
}
