using System.Diagnostics;
using Tools;

namespace TestProcessors;

public class FloatDecimalComparisonTestProcessor(IFloatGenerator generator) : ITestProcessor
{
    private readonly short _iterationsCount = 10000;

    public void Execute()
    {
        Console.WriteLine($"Comparing float, decimal performance ({_iterationsCount} iterations):");

        var data = generator.Generate(_iterationsCount);
        TestFloatComparison(data);
        TestFloatToleranceComparison(data);
        TestDecimalComparison(data.Select(x => (decimal)x).ToArray());
    }

    private void TestFloatComparison(float[] data)
    {
        var timer = new Stopwatch();
        timer.Start();

        for(short i = 0; i < _iterationsCount; i++)
        {
            if (data[i] == 0.2f)
            {
                //float equals to 0.2
            }
        }

        timer.Stop();
        Console.WriteLine($"Float time: {timer.Elapsed}");
    }

    private void TestFloatToleranceComparison(float[] data)
    {
        var timer = new Stopwatch();
        timer.Start();

        for(short i = 0; i < _iterationsCount; i++)
        {
            if (Math.Abs(data[i] - 0.2f) <= 1e-8)
            {
                //float equals to 0.2
            }
        }

        timer.Stop();
        Console.WriteLine($"Float with tolerance value time: {timer.Elapsed}");
    }

    private void TestDecimalComparison(decimal[] data)
    {
        var timer = new Stopwatch();
        timer.Start();

        for(short i = 0; i < _iterationsCount; i++)
        {
            if (data[i] == 0.2m)
            {
                //decimal equals to 0.2
            }
        }

        timer.Stop();
        Console.WriteLine($"Decimal time: {timer.Elapsed}");
    }
}