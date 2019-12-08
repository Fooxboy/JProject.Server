﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.Core.Helpers
{
    public class SocketHelper
    {
        private readonly Socket _socket;
        public SocketHelper(Socket socket)
        {
            _socket = socket;
        }

        public void Send(string message)
        {
            try
            {
                var data = Encoding.Unicode.GetBytes(message);
                _socket.Send(data);
            }catch(Exception e)
            {
                Console.WriteLine($"Произошла ошибка при отправки данных: {e}");
            }
            
        }
    }
}
}
