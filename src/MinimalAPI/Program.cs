using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//Example of delegate and lambda when get is called
app.MapGet("/",
    ([FromHeader]string accept) => $"Header {accept}");

app.Run();
