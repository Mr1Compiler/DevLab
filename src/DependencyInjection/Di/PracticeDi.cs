using System.Security.AccessControl;
using Dumpify;

namespace Di;

public class PracticeDi
{
    public void Run()
    {
        // Just adding services to Dependencies
        // without creating instances of them (Register them)
        var container = new DependencyContainer();
        container.AddTransient<ServiceConsumer>();
        container.AddTransient<HelloService>();
        container.AddSingleton<MessageService>();

        // Using resolver for getting the services
        var resolver = new DependencyResolver(container);

        var service1 = resolver.GetService<ServiceConsumer>();
        service1.Print();
        
        var service2 = resolver.GetService<ServiceConsumer>();
        service2.Print();
        
        var service3 = resolver.GetService<ServiceConsumer>();
        service3.Print();
    }
}

public class DependencyResolver
{
    DependencyContainer _container;

    public DependencyResolver(DependencyContainer container)
    {
        _container = container;
    }

    public T GetService<T>()
    {
        return (T)GetService(typeof(T));
    }

    public object GetService(Type type)
    {
        var dependency = _container.GetDependency(type);

        var constructors = dependency.Type.GetConstructors().Single();
        var parameters = constructors.GetParameters().ToArray();
        var parametersImplementation = new object[parameters.Length];

        if (parameters.Length > 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                // Recursive to assign all the services (inside services) 
                parametersImplementation[i] = GetService(parameters[i].ParameterType);
            }

            return CreateImplementation(dependency, t => Activator.CreateInstance(t, parametersImplementation));
        }

        return CreateImplementation(dependency, t => Activator.CreateInstance(t));
    }

    public object CreateImplementation(Dependency dependency, Func<Type, object> factory)
    {
         if (dependency.Implemented)
        {
            return dependency.Implementation;
        }

        var implementation = factory(dependency.Type);

        if (dependency.LifeTime == DependencyLifeTime.Singleton)
        {
            dependency.AddImplementation(implementation);
        }

        return implementation;
    }
}

public class DependencyContainer
{
    private List<Dependency> _dependinces;

    public DependencyContainer()
    {
        _dependinces = new List<Dependency>();
    }

    public void AddSingleton<T>()
    {
        _dependinces.Add(new Dependency(typeof(T), DependencyLifeTime.Singleton));
    }
    
     public void AddTransient<T>()
    {
        _dependinces.Add(new Dependency(typeof(T), DependencyLifeTime.Transient));
    }

    public Dependency GetDependency(Type type)
    {
        return _dependinces.First(x => x.Type.Name == type.Name);
    }
}

public class Dependency
{
    public Dependency(Type type, DependencyLifeTime lifeTime)
    {
        Type = type;
        LifeTime = lifeTime;
    }
    public Type Type { get; set; }
    public DependencyLifeTime LifeTime { get; set; }
    public object Implementation { get; set; }
    public bool Implemented { get; set; }

    public void AddImplementation(object obj)
    {
        Implementation = obj;
        Implemented = true;
    }
}

public enum DependencyLifeTime
{
    Singleton = 0,
    Transient = 1,
}

public class ServiceConsumer
{
    HelloService _hello;
    
    public ServiceConsumer(HelloService hello)
    {
        _hello = hello;
    }
    
    public void Print()
    {
        _hello.Print();
    }
}

public class HelloService
{
    MessageService _message;
    int _random;
    
    public HelloService(MessageService message)
    {
        _message = message;
        _random = new Random().Next();
    }
    
    public void Print()
    {
        $"Hello World #{_random} {_message.Message()}".Dump();
    }
}

public class MessageService
{
    int _random;
    public MessageService()
    {
        _random = new Random().Next();
    }
    public string Message()
    {
        return $"Yo #{_random}";
    }
}