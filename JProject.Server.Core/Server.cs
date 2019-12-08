using JProject.Server.Core.Commands;
using JProject.Server.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JProject.Server.Core
{
    public class Server
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly IDatabase _database;
        public List<Lobby> Lobbies { get;}

        public Server(string ip, int port, IDatabase database)
        {
            _ip = ip;
            _port = port;
            _database = database;
            Lobbies = new List<Lobby>();
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
                var userIp = ((IPEndPoint)socket.RemoteEndPoint).Address;
                Console.WriteLine($"Новое подключение с ip {userIp}");

                if(request.Split(';')[0] == "login")
                {
                    var login = new Login(_database);
                    var user = login.Start(request.Split(';')[1], request.Split(';')[2]);
                    if(user is null)
                    {
                        var data = Encoding.Unicode.GetBytes("error;1");
                        socket.Send(data);
                    }else
                    {
                        var lobby = new Lobby(socket, user);
                        Lobbies.Add(lobby);
                        var data = Encoding.Unicode.GetBytes($"lobby;{user.Name}");
                        socket.Send(data);
                    }
                }else if(request.Split(';')[0] == "register")
                {
                    var user = _database.Register(request.Split(';')[1], request.Split(';')[2]);

                    if (user is null)
                    {
                        var data = Encoding.Unicode.GetBytes("error;2");
                        socket.Send(data);
                    }
                    else
                    {
                        var lobby = new Lobby(socket, user);
                        Lobbies.Add(lobby);
                        var data = Encoding.Unicode.GetBytes($"lobby;{user.Name}");
                        socket.Send(data);
                    }
                }else
                {
                    var data = Encoding.Unicode.GetBytes($"error;5");
                    socket.Send(data);
                }
            });
        }
    }
}
