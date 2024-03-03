using System.Drawing;

namespace TestProcessors;

public class InTestProcessor : ITestProcessor
{
    public void Execute()
    {
        var p1 = new Pointer(0, 0);
        var p2 = new Pointer(3, 4);
        var distance = MeasureDistance(in p1, in p2);
    }

    double MeasureDistance(in Pointer p1, in Pointer p2)  //here will be a copy of pointers without in (now passed by ref)
    {
        //method cannot be overloaded with same parameters but without 'in'
        //cannot modify pointers due to 'in'
        var dx = p1.X - p2.X;
        var dy = p1.Y - p2.Y;

        return Math.Sqrt(dx * dx + dy * dy);
    }
}