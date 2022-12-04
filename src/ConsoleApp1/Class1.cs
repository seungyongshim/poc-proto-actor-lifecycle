using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace ConsoleApp1;

public class SampleActor : IActor
{
    public Task ReceiveAsync(IContext context) => context.Message switch
    {
        Started => Task.Run(() => { }),
        Stopping => Task.Run(() => { }),
        Restarting => Task.Run(() => { }),
        Stopped => Task.Run(() => { }),

        _ => Task.CompletedTask
    };
}
