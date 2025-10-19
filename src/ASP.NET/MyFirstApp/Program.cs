var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("MyCors", policy =>
    {
        policy
            .WithOrigins("http://localhost:5288")
            // .WithMethods("GET", "POST")
            .WithHeaders("my-a", "my-b");
        // .WithExposedHeaders("X-Custom-Header")                    
        // .AllowCredentials();                                          
    });
});

var app = builder.Build();

// ** Custom middleware to accept cors requests with custom headers **
// app.Use(async (ctx, next) =>
// {
//     ctx.Response.Headers["Access-Control-Allow-Origin"] = "http://localhost:5288";
//
//     if (HttpMethods.IsOptions(ctx.Request.Method))
//     {
//         ctx.Response.Headers["Access-Control-Allow-headers"] = "my-a, my-b";
//         await ctx.Response.CompleteAsync();
//     }
//
//     await next();
// });

app.MapGet("/", () => "Hello Ayman");

app.UseCors("MyCors");
app.Run();