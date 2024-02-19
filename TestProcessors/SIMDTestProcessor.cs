using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace TestProcessors;

public class SIMDTestProcessor : ITestProcessor
{
    public void Execute()
    {
        if (Sse.IsSupported)
        {
            var matrix = GetMatrix();

            if (matrix != null)
            {
                Console.WriteLine($"SIMD testing, sum of {matrix.Length}th Intrinsics-Vectors/arrays");
                VectorTest(matrix);
                ScalarTest(matrix);
            }
        } else {
            Console.WriteLine("SIMD not supported");
        }
    }

    private void VectorTest(float[][] matrix)
    {
        if (matrix != null)
        {
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start(); 

            var vectorResult = Vector128.Create(matrix[0]);
            for(int i = 1; i < matrix.Length; i++)
            {
                var addition = Vector128.Create(matrix[i]);
                vectorResult = Sse.Add(vectorResult, addition);
            }
            timer.Stop();

            Console.WriteLine($"Vector operation time: {timer.Elapsed}");
        }
    }

    private void ScalarTest(float[][] matrix)
    {
        if (matrix != null)
        {
            var timer = new System.Diagnostics.Stopwatch();
            timer.Start();

            var scalarResult = new float[matrix.First().Count()];

            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Count(); j++)
                {
                    scalarResult[j] = (byte)(scalarResult[j] + matrix[i][j]);
                }
            }

            timer.Stop();
            Console.WriteLine($"Scalar operation time: {timer.Elapsed}");
        }
    }

    private float[][] GetMatrix()
    {
        const short matrixSize = 10000;
        const byte columnSize = 16;

        var matrix = new float[matrixSize][];

        for (int i = 0; i < matrixSize; i++)
        {
            matrix[i] = new float[columnSize];

            Random random = new Random();
            for (int j = 0; j < columnSize; j++)
            {
                matrix[i][j] = (byte)random.Next();
            }
        }

        return matrix;
    }
}