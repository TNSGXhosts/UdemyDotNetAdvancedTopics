namespace Tools;

public class FloatGenerator : IFloatGenerator
{
    public float[] Generate(short size)
    {
        var data = new float[size];
        Random random = new Random();

        for (int i = 0; i < size; i++)
        {
            data[i] = (float)random.NextDouble();
        }

        return data;
    }
}