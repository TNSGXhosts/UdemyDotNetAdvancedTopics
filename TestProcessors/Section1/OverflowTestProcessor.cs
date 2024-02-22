namespace TestProcessors;

public class OverflowTestProcessor : ITestProcessor
{
    public void Execute()
    {
        unchecked {
            var temp = int.MinValue - 1;

            Console.WriteLine("Calculations in unchecked mode:");
            Console.WriteLine($"{int.MinValue} - 1 = {temp}");
        }
    }
}