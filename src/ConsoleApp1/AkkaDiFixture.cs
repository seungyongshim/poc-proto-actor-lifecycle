using Microsoft.Extensions.DependencyInjection;

namespace ConsoleApp1;

public class AkkaDiFixture : IDisposable
{
    public interface IDependency : IDisposable
    {
        string Name { get; }

        bool Disposed { get; }
    }

    public interface ITransientDependency : IDependency
    {
    }

    public class Transient : ITransientDependency
    {
        public Transient() : this("t" + Guid.NewGuid().ToString())
        {
        }
        public Transient(string name)
        {
            "Created TransientItem".ToConsole(ConsoleColor.Blue);
            Name = name;
        }
        public void Dispose()
        {
            "Disposed TransientItem".ToConsole(ConsoleColor.Blue);
            Disposed = true;
        }

        public string Name { get; }

        public bool Disposed { get; private set; }

        
    }

    public interface IScopedDependency : IDependency
    {
    }

    public class Scoped : IScopedDependency
    {
        public Scoped() : this("s" + Guid.NewGuid().ToString())
        {
        }

        public Scoped(string name)
        {
            "Created ScopedItem".ToConsole(ConsoleColor.Green);
            Name = name;
        }

        public void Dispose()
        {
            "Disposed ScopedItem".ToConsole(ConsoleColor.Green);
            Disposed = true;
        }

        public bool Disposed { get; private set; }
        public string Name { get; }
    }

    public interface ISingletonDependency : IDependency
    {
    }

    public class Singleton : ISingletonDependency
    {
        public Singleton() : this("singleton")
        {
        }

        public Singleton(string name)
        {
            "Created SingletonItem".ToConsole(ConsoleColor.Red);
            Name = name;
        }

        public void Dispose()
        {
            "Disposed SingletonItem".ToConsole(ConsoleColor.Red);
            Disposed = true;
        }

        public bool Disposed { get; private set; }
        public string Name { get; }
    }

    public AkkaDiFixture()
    {
        var services = new ServiceCollection();
        _ = services.AddSingleton<ISingletonDependency, Singleton>()
                .AddScoped<IScopedDependency, Scoped>()
                .AddTransient<ITransientDependency, Transient>();

        Provider = services.BuildServiceProvider();
    }

    public IServiceProvider? Provider { get; private set; }

    public void Dispose() => Provider = null;
}
