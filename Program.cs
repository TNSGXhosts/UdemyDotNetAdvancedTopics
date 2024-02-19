
using TestProcessors;
using Microsoft.Extensions.DependencyInjection;
using Tools;
using System.Reflection;


Console.WriteLine("Here is some practical cases:");

//Section 1:
var serviceProvider = new ServiceCollection()
    .AddTransient<IFloatGenerator, FloatGenerator>();

var types = Assembly.GetExecutingAssembly().GetTypes()
    .Where(t => typeof(ITestProcessor).IsAssignableFrom(t) && t.IsClass);

foreach (var type in types)
{
    serviceProvider.AddTransient(typeof(ITestProcessor), type);
}

var services = serviceProvider.BuildServiceProvider().GetServices<ITestProcessor>();

foreach (var service in services)
{
    service.Execute();
}



