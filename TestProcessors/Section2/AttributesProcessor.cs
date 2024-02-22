using CustomAttributes;

namespace TestProcessors;

public class AttributesProcessor : ITestProcessor
{
    public void Execute()
    {
        //Attributes is not a way for metaprogramming
        Console.WriteLine("Testing attributes:");

        var myType = typeof(AttributesProcessor);
        var testMethod = myType.GetMethod("TestMethod");

        if (testMethod != null)
        {
            foreach(var attr in testMethod.GetCustomAttributes(true))
            {
                if (attr is RepeatAttribute ra)
                    Console.WriteLine($"Need to repeate {ra.Times} times");
            }
        }
    }

    [Repeat(3)]
    public void TestMethod()
    {
        throw new NotImplementedException();
    }
}