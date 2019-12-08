using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JProject.Server.Core
{
    public class Server
    {
        private readonly string _ip;
        private readonly int _port;

        public Server(string ip, int port)
        {
            _ip = ip;
            _port = port;
        }

        public void Start()
        {
            var listener = new SocketListener(_ip, _port);
            listener.NewConnectEvent += NewConnect;
            listener.Run();
        }

        private void NewConnect(string request, Socket socket)
        {
            Task.Run(() =>
            {

            });
        }
    }
}
