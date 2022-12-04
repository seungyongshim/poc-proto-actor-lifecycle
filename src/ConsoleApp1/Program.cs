using System.Diagnostics;
using ConsoleApp1;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Proto;

var sp = new AkkaDiFixture().Provider;

var app = Host.CreateDefaultBuilder()
              .ConfigureLogging(builder =>
              {
                  builder.ClearProviders();
              })
              .ConfigureServices(services =>
              {
              })
              .Build();

var actorSystem = new ActorSystem();

var root = actorSystem.Root;

var pid = root.Spawn(Props.FromProducer(() => new SampleActor()), ctx => ctx.Set(sp.CreateAsyncScope()));


await app.StartAsync();

while (true)
{
    var input = Console.ReadLine();

    root.Send(pid, input);
}

await app.StopAsync();
