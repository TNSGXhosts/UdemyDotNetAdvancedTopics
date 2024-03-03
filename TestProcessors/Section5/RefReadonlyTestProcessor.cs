namespace TestProcessors;

public class RefReadonlyTestProcessor : ITestProcessor
{
    public void Execute()
    {
        var p1 = Pointer.Origin; // by-value copy

        //ref var p2 = ref Pointer.Origin; don't work because it's readonly

        ref readonly var p3 = ref Pointer.Origin; // by-reference copy
    }
}