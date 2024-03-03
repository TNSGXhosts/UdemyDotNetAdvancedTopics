using System.Diagnostics;
using TestProcessors;

namespace Section6;

public class DisposeTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("IDisposable demonstration: ");

        using(new SimpleTimer())
        {
            Thread.Sleep(1000);
        }

        var timer = new Stopwatch();
        using(Disposable.Create(() => timer.Start(), () => timer.Stop()))
        {
            Thread.Sleep(1000);
        }

        Console.WriteLine("Disposable time: " + timer.Elapsed);
    }
}

public class SimpleTimer : IDisposable
{
    private readonly Stopwatch _stopwatch;

    public SimpleTimer()
    {
        _stopwatch = new Stopwatch();
        _stopwatch.Start();
    }
    public void Dispose()
    {
        _stopwatch.Stop();
        Console.WriteLine($"SimpleTimer time: {_stopwatch.Elapsed}");
    }
}

public class Disposable : IDisposable
{
    private readonly Action _end;

    public Disposable(Action start, Action end)
    {
        this._end = end;
        start();
    }

    public void Dispose()
    {
        _end();
    }

    public static Disposable Create(Action start, Action end)
    {
        return new Disposable(start, end);
    }
}