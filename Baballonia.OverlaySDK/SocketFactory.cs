﻿using System.Net;
using System.Net.Sockets;

namespace OverlaySDK;

public static class SocketFactory
{
    /// <summary>
    /// Creates a connected client socket.
    /// </summary>
    public static Socket CreateClient(string host, int port)
    {
        var socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Connect(host, port);
        socket.Blocking = false;

        return socket;
    }

    /// <summary>
    /// Creates a server socket and accepts a single client connection.
    /// Returns the accepted client socket (non-blocking).
    /// </summary>
    public static Socket CreateServer(string host, int port)
    {
        var listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        var ipAddress = IPAddress.Parse(host);

        listener.Bind(new IPEndPoint(ipAddress, port));
        listener.Listen(1);

        var clientSocket = listener.Accept();
        clientSocket.Blocking = false;

        listener.Close();

        return clientSocket;
    }
}
