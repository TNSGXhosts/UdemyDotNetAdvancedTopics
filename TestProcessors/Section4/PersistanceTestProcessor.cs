namespace TestProcessors;

public class PersistanceTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Persistance demonstration:");

        var str = "new string";
        str.SetTag(23);
        Console.WriteLine(str.GetTag());

        var num = 1;
        num.SetTag(21);
        Console.WriteLine(num.GetTag());    //No result because it's value type and can't find it in dictionary
    }
}

public static class PersistanceExtensionMethods
{
    //u can use weak ref to have possibility to manage object but u don't want prevent deletion by GC
    //so if all strong refs are deleted, GB would remove object despire weakRefs in this class
    private static Dictionary<WeakReference, object> s_data = new Dictionary<WeakReference, object>();

    public static object? GetTag(this object o)
    {
        var key = s_data.Keys.FirstOrDefault(k => k.IsAlive && k.Target == o);
        return key != null ? s_data[key] : null;
    }

    public static void SetTag(this object o, object tag)
    {
        var key = s_data.Keys.FirstOrDefault(k => k.IsAlive && k.Target == o);
        if (key != null)
        {
            s_data[key] = tag;
        }
        else
        {
            s_data.Add(new WeakReference(o), tag);
        }
    }
}