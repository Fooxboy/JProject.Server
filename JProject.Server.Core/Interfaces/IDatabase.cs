using JProject.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace JProject.Server.Core.Interfaces
{
    public interface IDatabase
    {
        User Login(string nick, string password);
        User Register(string nick, string password);
    }
}
