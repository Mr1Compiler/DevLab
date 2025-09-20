using System.Net.WebSockets;
using System.Net;
using System.Text;
using WsDemo;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();


app.UseWebSockets(new WebSocketOptions
{
    KeepAliveInterval = TimeSpan.FromSeconds(30)
});

app.MapGet("/hi", async context =>
{
    await context.Response.WriteAsync("welcome again"); 
    try
    {
        await Task.Delay(5000, context.RequestAborted);
        // Delay completed normally, client still connected
        await context.Response.WriteAsync("Still connected", context.RequestAborted);
    }
    catch (OperationCanceledException)
    {
        // The client disconnected during the delay
        Console.WriteLine("Client aborted the request.");
    }

});

app.MapGet("/ws", async context =>
{
    if (!context.WebSockets.IsWebSocketRequest)
    {
        context.Response.StatusCode = 400;
        return;
    }

    await Server.Connect(context);
});

app.Run();
