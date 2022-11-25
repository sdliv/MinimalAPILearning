var builder = WebApplication.CreateBuilder(args);
//Configure our service
var app = builder.Build();
app.Urls.Add("https://localhost:7777");
app.Urls.Add("https://localhost:8888");
app.Run();
