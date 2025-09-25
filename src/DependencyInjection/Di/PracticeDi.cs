using Dumpify;

namespace Di;

public class PracticeDi
{
    public void Run()
    {
        // Just adding services to Dependencies
        // without creating instances of them (Register them)
        var container = new DependencyContainer();
        container.AddDependency(typeof(ServiceConsumer));
        container.AddDependency<HelloService>();
        container.AddDependency<MessageService>();

        // Using resolver for getting the services
        var resolver = new DependencyResolver(container);

        var service = resolver.GetService<ServiceConsumer>();
        service.Print();
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

        var constructors = dependency.GetConstructors().Single();
        var parameters = constructors.GetParameters().ToArray();
        var parametersImplementation = new object[parameters.Length];

        if (parameters.Length > 0)
        {
            for (int i = 0; i < parameters.Length; i++)
            {
                // Recursive to assign all the services (inside services) 
                parametersImplementation[i] = GetService(parameters[i].ParameterType);
            }

            return Activator.CreateInstance(dependency, parametersImplementation);
        }

        return Activator.CreateInstance(dependency); 
    }
}

public class DependencyContainer
{
    List<Type> _dependinces = new List<Type>();

    public void AddDependency(Type type)
    {
        _dependinces.Add(type);
    }
    
     public void AddDependency<T>()
    {
        _dependinces.Add(typeof(T));
    }

    public Type GetDependency(Type type)
    {
        return _dependinces.First(x => x.Name == type.Name);
    }
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
    
    public HelloService(MessageService message)
    {
        _message = message;
    }
    
    public void Print()
    {
        $"Hello World {_message.Message()}".Dump();
    }
}

public class MessageService
{
    public string Message()
    {
        return "Yo";
    }
}