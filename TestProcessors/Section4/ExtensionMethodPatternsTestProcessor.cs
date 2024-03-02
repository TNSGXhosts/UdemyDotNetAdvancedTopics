using System.Text;

namespace TestProcessors;

public class ExtensionMethodPatternsTestProcessor : ITestProcessor
{
    public void Execute()
    {
        
    }
}

public static class ExtensionMethodsForPatternDemo
{
    //just a little demo of possible extension methods

    public static StringBuilder Al(this StringBuilder sb, string text)
    {
        return sb.AppendLine(text);
    }

    public static StringBuilder AppendFormatLine(this StringBuilder sb, string format, params object[] args)
    {
        return sb.AppendLine(string.Format(format, args));
    }

    public static ulong Xor(this IList<ulong> nums)
    {
        var first = nums.FirstOrDefault();
        foreach(var num in nums.Skip(1))
        {
            first ^= num;
        }

        return first;
    }

    public static void AddRange<T>(this IList<T> list, params T[] objects)
    {
        list.AddRange(objects);
    }

    public static string F(string str, params object[] args)
    {
        return string.Format(str, args);
    }

    public static DateTime June(this int day, int year)
    {
        return new DateTime(year, 6, day);
    }
}