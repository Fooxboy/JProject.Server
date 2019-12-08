using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace JProject.Server.Core
{
    public delegate void NewConnectDelegate(string request, Socket socket);
}
