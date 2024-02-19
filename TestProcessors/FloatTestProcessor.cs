using System.Diagnostics;
using Tools;

namespace TestProcessors;

public class FloatTestProcessor(IFloatGenerator generator) : ITestProcessor
{
    private readonly short iterationsCount = 10000;
    public void Execute()
    {
        Console.WriteLine($"Float, decimal operations performance ({iterationsCount} iterations):");

        var data = generator.Generate(iterationsCount);

        TestFloatPerformance(data);
        TestDecimalPerformance(data.Select(x => (decimal)x).ToArray());
    }

    private void TestFloatPerformance(float[] data)
    {
        var timer = new Stopwatch();
        timer.Start();

        var result = 0.0f;
        foreach(var el in data)
        {
            result += el;
        }
        timer.Stop();
        Console.WriteLine($"Float time: {timer.Elapsed}");
    }

    private void TestDecimalPerformance(decimal[] data)
    {
        var timer = new Stopwatch();
        timer.Start();

        var result = 0.0m;
        foreach(var el in data)
        {
            result += el;
        }
        timer.Stop();
        Console.WriteLine($"Decimal time: {timer.Elapsed}");
    }
}