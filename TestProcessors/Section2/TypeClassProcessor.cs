namespace TestProcessors;

public class TypeClassProcessor : ITestProcessor
{
    public void Execute()
    {
        var type = typeof(int);

        var assembly = type.Assembly;
        var types = assembly.GetTypes();

        var listType = Type.GetType("System.Collections.Generic.List`1"); //1 - list, 2 - dictionary 

        var list = new List<string>() { "" };
        var listType2 = list.GetType();
        var elementType = listType2.GetGenericArguments().First();
    }
}