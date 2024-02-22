namespace CustomAttributes;

public class RepeatAttribute : Attribute
{
    public int Times { get; set; }

    public RepeatAttribute(int times)
    {
        Times = times;
    }
}