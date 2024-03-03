using System.Diagnostics;

namespace TestProcessors;

public class ExtensionMethodTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Extension method demonstration:");

        var foo = new FooChild();
        Foo fooChild = foo;
        Console.WriteLine($"{foo.GetNum()} {foo.Name}");
        Console.WriteLine($"{fooChild.GetNum()} {fooChild.Name}");   //different polymorphic behavior unlike ordinary methods

        var me = ("Vit", 24).ToPerson();
    }
}

public static class ExtensionMethods
{
    //no way to replays build-in object methods by extension methods

    public static string ToBinary(this int value)
    {
        return Convert.ToString(value, 2);
    }

    public static int GetNum(this Foo foo)
    {
        return 1;
    }

    public static int GetNum(this FooChild foo)
    {
        return 2;
    }

    //tuple extension method
    public static Person ToPerson(this (string name, int age) data)
    {
        return new Person
        {
            Name = data.name,
            Age = data.age
        };
    }

    //generic as well
    public static int Measure<T,TU>(this Tuple<T, TU> tuple)
    {
        return tuple.Item1.ToString().Length;
    }

    public static Stopwatch Measure(this Func<int> func)
    {
        var timer = new Stopwatch();
        timer.Start();
        func();
        timer.Stop();
        return timer;
    }

    //for all methods
    public static void DeepCopy<T>(this T source) where T: new() //some restrictions
    {
        //implementation
    }
}

public class Foo
{
    public string Name => "Foo";
}

public class FooChild : Foo
{
    public string Name => "Child";
}

public class Person
{
    public string Name { get;set; }
    public int Age { get;set; }
    public IEnumerable<Person> Childrens { get;set; } = new List<Person>();
    public IEnumerable<Person> Parents { get;set; } = new List<Person>();
}