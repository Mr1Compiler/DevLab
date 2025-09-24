using System;
using System.Threading.Tasks;
using Udp.Server;
using Udp.Client;


Task.Run(() =>
{
    UdpServer udpServer = new UdpServer();
    udpServer.Connect();
    udpServer.Connect();
});


while (true)
{
    var msg = Console.ReadLine();
    Client client = new Client();
    client.Send(msg);
}