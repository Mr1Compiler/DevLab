using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace WsDemo;

static class Server
{
    public static async Task Connect(HttpContext context)
    {
        using var socket = await context.WebSockets.AcceptWebSocketAsync();
        var socketId = Guid.NewGuid().ToString();
        
        Console.WriteLine("Connected");
        var buffer = new byte[4 * 1024]; // Receiving data 
        
        while (socket.State == WebSocketState.Open)
        {
            var result = await socket.ReceiveAsync(buffer, context.RequestAborted);
            if (result.MessageType == WebSocketMessageType.Close)
            {
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Bye", context.RequestAborted);
                Console.WriteLine("Disconnected");
            }

            var msg = Encoding.UTF8.GetString(buffer, 0, result.Count);
            var echo = Encoding.UTF8.GetBytes($"echo : {msg}");
            await socket.SendAsync(echo, WebSocketMessageType.Text, endOfMessage: true,
                context.RequestAborted);
        }
    }

    public static async Task SendMessage(HttpContext context)
    {
        
    }
}