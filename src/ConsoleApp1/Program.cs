using System.Diagnostics;
using Microsoft.Extensions.Hosting;
using Proto;

var app = Host.CreateDefaultBuilder()
              .Build();


var actorSystem = new ActorSystem();





await app.RunAsync();
