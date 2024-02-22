namespace TestProcessors;

public class ConstructionProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Some tests of creating objects with reflection:");

        var boolType = typeof(bool);
        var boolInstance = Activator.CreateInstance(boolType); //Activator.CreateInstance<bool>()

        var arrType = Type.GetType("System.Collections.ArrayList");
        var arrConstructor = arrType?.GetConstructor(Array.Empty<Type>());
        var arr = arrConstructor?.Invoke(Array.Empty<object>());
        Console.WriteLine(arr);

        var strType = typeof(string);
        var strConstructor = strType.GetConstructor(new[] { typeof(char[]) });
        var str = strConstructor?.Invoke(new object[] { new[] { 't', 'e', 's', 't' } });
        Console.WriteLine(str);

        var listType = Type.GetType("System.Collections.Generic.List`1");
        var listOfIntType = listType?.MakeGenericType(typeof(int));
        var intListCtor = listOfIntType?.GetConstructor(Array.Empty<Type>());
        var list = intListCtor?.Invoke(Array.Empty<object>());

        var charType = typeof(char);
        var charArray = Array.CreateInstance(charType, 4);
        var charArrayType = charType.MakeArrayType();
        var charCtor = charArrayType.GetConstructor(new[] { typeof(int) });
        var charArray2 = charCtor?.Invoke(new object[] { 7 });
    }
}