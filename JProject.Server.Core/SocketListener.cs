using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.Core
{
    public class SocketListener
    {
        private Socket _serverSocket;
        private IPEndPoint _ipPoint;
        public event NewConnectDelegate NewConnectEvent;
        public SocketListener(string ip, int port)
        {
            var address = ip == "localhost" ? Dns.GetHostEntry(ip).AddressList[0] : IPAddress.Parse(ip);
            _ipPoint = new IPEndPoint(address, port);
            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        public void Run()
        {
            try
            {
                _serverSocket.Bind(_ipPoint);
                _serverSocket.Listen(10);
                StartListenRequests();
            }
            catch (Exception e)
            {
                //_logger.Error($"Произошла ошибка при старте SocketServerListener: {e}");
            }
        }

        private void StartListenRequests()
        {
            while (true)
            {
                try
                {
                    var handler = _serverSocket.Accept();
                    var builder = new StringBuilder();
                    int bytes = 0;
                    byte[] data = new byte[256];

                    do
                    {
                        bytes = handler.Receive(data);
                        builder.Append(Encoding.Unicode.GetString(data, 0, bytes));
                    }
                    while (handler.Available > 0);

                    var message = builder.ToString();
                    NewConnectEvent?.Invoke(message, handler);
                }
                catch (Exception e)
                {
                    //_logger.Error($"Произошла ошибка при получении запроса: {e}");
                }

            }
        }
    }
}
