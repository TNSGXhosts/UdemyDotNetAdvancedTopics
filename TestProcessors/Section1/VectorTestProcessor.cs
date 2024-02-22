using System.Diagnostics;
using System.Numerics;

namespace TestProcessors;

public class VectorTestProcessor : ITestProcessor
{
    public void Execute()
    {
        if (Vector.IsHardwareAccelerated)
        {
            var array1 = Enumerable.Range(1, 128)
                .Select(x => (byte) x).ToArray();
            var array2 = Enumerable.Range(4, 128)
                .Select(x => (byte) x).ToArray();
            var result = new byte[128];

            int size = Vector<byte>.Count;

            for (int i = 0; i < array1.Length; i += size)
            {
                var va = new Vector<byte>(array1, i);
                var vb = new Vector<byte>(array2, i);
                var vc = va + vb;
                vc.CopyTo(result, i);
            }

            PrintSizes();
        } else {
            Console.WriteLine("Vector not supported, will be work but slover");
        }
    }

    private void PrintSizes()
    {
        Console.WriteLine($"Size of byte Vector on my machine is {Vector<byte>.Count}");
        Console.WriteLine($"Size of short Vector on my machine is {Vector<short>.Count}");
        Console.WriteLine($"Size of int Vector on my machine is {Vector<int>.Count}");
        Console.WriteLine($"Size of long Vector on my machine is {Vector<long>.Count}");
  }

    public void ExecuteSpeedTest()
    {
        var matrix = GetMatrix();

        if (matrix != null)
        {
            Console.WriteLine($"Vector testing, sum of {matrix.Length}th Vectors/arrays");

            ScalarTest(matrix);
            VectorTest(matrix);
        }
    }

    private void ScalarTest(byte[][] matrix)
    {
        var timer = new Stopwatch();
        var scalarResult = new byte[matrix.First().Count()];

        timer.Start();
        for (int i = 0; i < matrix.Count(); i++)
        {
            for (int j = 0; j < matrix[i].Count(); j++)
            {
                scalarResult[j] = (byte)(scalarResult[j] + matrix[i][j]);
            }
        }
        timer.Stop();
        Console.WriteLine($"Scalar time: {timer.Elapsed}");
    }

    private void VectorTest(byte[][] matrix)
    {
        var timer = new Stopwatch();

        timer.Start();

        Vector<byte> vectorResult = new Vector<byte>();
        for (int i = 0; i < matrix.Count(); i++)
        {
            var addition = new Vector<byte>(matrix[0]);
            vectorResult += addition;
        }
        timer.Stop();
        Console.WriteLine($"Vector time: {timer.Elapsed}");
    }

    private byte[][] GetMatrix()
    {
        const short matrixSize = 10000;

        var matrix = new byte[matrixSize][];

        for (int i = 0; i < matrixSize; i++)
        {
            matrix[i] = new byte[Vector<byte>.Count];

            Random random = new Random();
            for (int j = 0; j < Vector<byte>.Count; j++)
            {
                matrix[i][j] = (byte)random.Next();
            }
        }

        return matrix;
    }
}