using System.Diagnostics;
using System.Numerics;

namespace TestProcessors;

public class BigIntTestProcessor : ITestProcessor
{
    private readonly short _iterationsCount = 10000;


    public void Execute()
    {
        Console.WriteLine($"Testing int and BigInt as a loop counter ({_iterationsCount} iterations):");
        LoopTestWithIntCounter();
        LoopTestWithBigIntCounter();   
    }

    private void LoopTestWithIntCounter()
    {
        var timer = new Stopwatch();
        timer.Start();

        for(int i = 0; i <= _iterationsCount; i++)
        {
            TrashProcess();
        }

        timer.Stop();
        Console.WriteLine($"Int time: {timer.Elapsed}");
    }

    private void LoopTestWithBigIntCounter()
    {
        var timer = new Stopwatch();
        timer.Start();

        for(BigInteger i = 0; i <= _iterationsCount; i++)
        {
            TrashProcess();
        }

        timer.Stop();
        Console.WriteLine($"BigInt time: {timer.Elapsed}");
    }

    private void TrashProcess()
    {
        var temp = 1;
    }
}