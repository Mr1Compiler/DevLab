using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.Use(async (HttpContext context, RequestDelegate next) =>
{
    if (context.Request.ContentType?.Contains("application/json") == true)
    {
        context.Request.EnableBuffering();
        using var reader = new StreamReader(context.Request.Body, leaveOpen: true);
        var body = await reader.ReadToEndAsync();
        context.Request.Body.Position = 0;

        var dict = JsonSerializer.Deserialize<Dictionary<string, object>>(body);

        
        if (dict != null && dict.TryGetValue("Name", out var value) && value?.ToString() == "Ayman") 
        {
            context.Items["Name"] = value;
            Console.WriteLine($"The name is {value}");
            await context.Response.WriteAsync("Welcome Ayman");
            return;
        }

        context.Response.StatusCode = 400;
        await context.Response.WriteAsync("Invalid name");
        return;
    }
    await next(context); 
});

app.MapPost("/test", async (context) =>
{
    await context.Response.WriteAsync("Only for accessing the middleware when hit");
});

app.Run();