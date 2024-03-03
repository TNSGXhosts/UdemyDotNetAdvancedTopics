using System.Collections;
using TestProcessors;

namespace Section6;

public class DuckTypingMixinTestProcessor : ITestProcessor
{
    //duck typing

    //GetEnumerator - foreach (IEnumerable<T>)
    //Dispose - using (IDisposable)

    //mixin
    public void Execute()
    {
        Console.WriteLine("Duck typing mixin demonstration:");

        var myClass = new MyClass();
        //myClass.Dispose(); - doesn't work, because MyClass not implemented IDisposable itself. u have to cast it to IEnumerable
        using(var foo = new Foo()) { }  //but this works
    }
}

interface IMyDisposable<T> : IDisposable
{
    void IDisposable.Dispose()
    {
        Console.WriteLine($"Disposing {nameof(T)}");
    }
}

class MyClass : IMyDisposable<MyClass>
{ }

ref struct Foo
{
    //ref struct cannot be stored in a heap
    //struct cannot implement interface
    //but this method will work in an using block
    public void Dispose()
    {
        Console.WriteLine("Foo is disposing");
    }
}