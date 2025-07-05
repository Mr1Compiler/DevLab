using System.Reflection.Metadata.Ecma335;

var app = WebApplication.Create();

app.MapGet("/hi/{n1}/{n2}", (int n1, int n2) =>
   n1 + n2
);

 app.Run();