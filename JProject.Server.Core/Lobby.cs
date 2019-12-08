using JProject.Server.Core.Helpers;
using JProject.Server.Core.Models;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JProject.Server.Core
{
    public class Lobby:SocketHelper
    {
        public Socket Socket{ get; }
        public User User { get; }
        public bool UserIsConnected { get; set; }

        private readonly CommandProccessor _proccessor;
        public Lobby(Socket socket, User user, CommandProccessor proccessor):base(socket)
        {
            Socket = socket;
            User = user;
            _proccessor = proccessor;
        }

        public void Start()
        {
            Task.Run(() => ListerNewRequest());
        }

        public void ListerNewRequest()
        {
            while (UserIsConnected)
            {
                if (Socket.Available == 0) return;
                var builder = new StringBuilder();
                int bytes = 0;
                byte[] data = new byte[256];
                do
                {
                    bytes = Socket.Receive(data);
                    builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                }
                while (Socket.Available > 0);
                var message = builder.ToString();
                var response = _proccessor.Run(message);
                if (response != null) this.Send(response);
            }
        }
    }
}
