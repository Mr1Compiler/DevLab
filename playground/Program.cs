using System.Net;
using System.Net.Sockets;
using Udp.Server;

namespace playground;

public static class Program
{
    static void Main()
    {
        Task.Run(() =>
        {
            UdpServer udpServer = new UdpServer();
            udpServer.Connect();
        });


        while (true)
        {
            var msg = Console.ReadLine();
            Client client = new Client();
            client.Send(msg);
        }
    }
} 