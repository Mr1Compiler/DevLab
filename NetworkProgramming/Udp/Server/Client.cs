using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Udp.Server;

public class Client
{
    public void Send(string msg)
    {
        // set up the client 
        UdpClient client = new UdpClient();
        byte[] msgByte = Encoding.ASCII.GetBytes(msg);
        
        // Sending data to the server 
        client.Send(msgByte, msg.Length, "127.0.0.1", 432);
        
        // Receiving the data from the server
        IPEndPoint serverEp = new IPEndPoint(IPAddress.Any, 0);
        byte[] responseBytes = client.Receive(ref serverEp);
        string response = Encoding.ASCII.GetString(responseBytes);
        Console.WriteLine($"--Client--\n{response}\nServerIp: {serverEp.Address}:{serverEp.Port}");
    }
}