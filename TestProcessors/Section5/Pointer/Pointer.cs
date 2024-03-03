namespace TestProcessors;

public struct Pointer
{
    public double X { get;set; }
    public double Y { get;set; }

    private static Pointer s_origin = new Pointer();
    //will be returned the same ref every time
    public static ref readonly Pointer Origin => ref s_origin;

    public Pointer(double a, double b)
    {
        X = a;
        Y = b;
    }

    public override string ToString()
    {
        return $"({X}, {Y})";
    }
}