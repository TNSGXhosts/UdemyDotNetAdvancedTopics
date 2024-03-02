namespace TestProcessors;

public class ValueTupleExtensionMethodsTestProcessor : ITestProcessor
{
    public void Execute()
    {
        var date = (1, 1, 2024).Dmy();
        var list = (new List<int> { 1, 2, 3 }, new List<int> { 4, 5, 6 }).Merge();
    }
}

public static class ValueTupleExtensionMethods
{
    public static DateTime Dmy(this (int d, int m, int y) data)
    {
        return new DateTime(data.y, data.m, data.d);
    }

    public static List<T> Merge<T>(this (IList<T> first, IList<T> second) lists)
    {
        var result = new List<T>();
        result.AddRange(lists.first);
        result.AddRange(lists.second);

        return result;
    }
}