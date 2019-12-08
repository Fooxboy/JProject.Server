using JProject.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.Core
{
    public class Lobby
    {
        public Socket Socket{ get; }
        public User User { get; }
        public Lobby(Socket socket, User user)
        {
            Socket = socket;
            User = user;
        }

        public void Start()
        {

        }
    }
}
