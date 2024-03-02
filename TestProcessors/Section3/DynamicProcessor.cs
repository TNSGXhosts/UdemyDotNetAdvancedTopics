using System.Dynamic;
using Microsoft.CSharp.RuntimeBinder;

namespace TestProcessors;

public class DynamicProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Dynamic demonstration:");

        //late binding
        dynamic d = "hello";

        try {
            var t = d.Area;
        } catch (RuntimeBinderException ex) {
            Console.WriteLine(ex.Message);
        }

        var widget = new Widget() as dynamic;

        Console.WriteLine(widget.Area);
        Console.WriteLine(widget[6]);
        widget.WhatIsThis();
    }
}

public class Widget : DynamicObject
{
    public void WhatIsThis()
    {
        //Console.WriteLine(this.Something); // doesn't work because this - still is static binging
        Console.WriteLine(This.Something); // but this is late binding and it works
    }

    public dynamic This => this;

    public override bool TryGetMember(GetMemberBinder binder, out object? result)
    {
        result = binder.Name;
         
        return true;
    }

    public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object? result)
    {
        if (indexes.Length == 1)
        {
            result = new string('*', (int)indexes[0]);
            return true;
        }

        result = null;
        return false;
    }
}