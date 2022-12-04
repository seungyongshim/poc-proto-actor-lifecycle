using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Proto;

namespace ConsoleApp1;

public class SampleActor : IActor
{
    public Task ReceiveAsync(IContext context) => context.Message switch
    {
        Started => Task.Run(() =>
        {
            Console.WriteLine("Received Started");
        }),
        Stopping => Task.Run(() =>
        {
            Console.WriteLine("Received Stopping");
        }),
        Restarting => Task.Run(() =>
        {
            Console.WriteLine("Received Restarting");
        }),
        Stopped => Task.Run(async () =>
        {
            Console.WriteLine("Received Stopped");
            await context.Get<AsyncServiceScope>().DisposeAsync();

        }),
        string msg => Task.Run(() =>
        {
            Console.WriteLine($"Received Message {msg}");

            var sp = context.Get<AsyncServiceScope>().ServiceProvider;
            var singleItem = sp.GetRequiredService<AkkaDiFixture.ISingletonDependency>();
            var scopedItem = sp.GetRequiredService<AkkaDiFixture.IScopedDependency>();
            var transientItem = sp.GetRequiredService<AkkaDiFixture.ITransientDependency>();

            return msg switch
            {
                "stop" or "poison" or "suicide" => Task.Run(() => context.Poison(context.Self)),

                "crash" => Task.Run(() => throw new Exception("crash")),

                _ => Task.CompletedTask
            };
        }),
        _ => Task.CompletedTask
    };
}
