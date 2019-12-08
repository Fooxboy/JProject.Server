using System;
using System.Collections.Generic;
using System.Text;

namespace JProject.Server.Core.Interfaces
{
    public interface ICommand
    {
        string Trigger { get; }
        string Execute(string msg);
    }
}
