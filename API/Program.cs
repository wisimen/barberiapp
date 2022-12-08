using Barberiapp;

var builder = WebApplication.CreateBuilder(args);

//builder.WebHost
//    .UseContentRoot(Directory.GetCurrentDirectory())
//    .UseWebRoot("wwwroot");
var startup = new Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.Configure(app, app.Environment);

app.Run();