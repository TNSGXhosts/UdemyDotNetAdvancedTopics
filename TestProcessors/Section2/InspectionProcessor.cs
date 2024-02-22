namespace TestProcessors;

public class InspectionProcessor : ITestProcessor
{
    public void Execute()
    {
        var type = typeof(Guid);
        var constructors = type.GetConstructors(); 
        foreach(var constructor in constructors)
        {
            Console.Write("- Guid(");
            var pars = constructor.GetParameters();
            for(var i = 0; i < pars.Length; i++)
            {
                Console.Write($"{pars[i].ParameterType.Name} {pars[i].Name}");

                if (i < pars.Length - 1)
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(")");
        }
    }
}