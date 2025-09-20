using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Udp.Server;

 public class UdpServer
 {
    public void Connect()
    {
        // Settings up the server  
        UdpClient udpServer = new UdpClient(4321);
        Console.WriteLine("server started, servicing on port: 4321");
        
        IPEndPoint ipEndPoint = new IPEndPoint(IPAddress.Any, 0);
    
        while (true)
        {
            byte[] receivedData = udpServer.Receive(ref ipEndPoint); // Receiving the data 
            string data = Encoding.ASCII.GetString(receivedData); // Convert the array byte to string to read it 
            Console.WriteLine($"--server--\nmsg: {data}\nip: {ipEndPoint.Address}:{ipEndPoint.Port}");
            byte[] sentData = Encoding.ASCII.GetBytes($"msg: {data}");
            udpServer.Send(sentData, sentData.Length, ipEndPoint);
        }
    }   
}