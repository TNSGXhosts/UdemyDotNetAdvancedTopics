using System.Numerics;

namespace TestProcessors;

public class PassStyleTestProcessor : ITestProcessor
{
    public void Execute()
    {
        var solver = new QuadraticEquationSolver();
        Tuple<Complex, Complex> result;
        var flag = solver.Start(1, 2, 1, out result);
    }
}

public enum WorkflowStatus
{
    Success,
    Failure
}

public class QuadraticEquationSolver
{
    // ax^2 + bx + c = 0
    public WorkflowStatus Start(double a, double b, double c, out Tuple<Complex, Complex> result)
    {
        var disc = b*b-4*a*c;
        if (disc < 0)
        {
            result = null;
            return WorkflowStatus.Failure;
        }
        else
        {
            SolveSimple(a, b, disc, out result);
            return WorkflowStatus.Success;
        }
    }

    private WorkflowStatus SolveComplex(double a, double b, double c, double disc, out Tuple<Complex, Complex> result)
    {
        var rootDisc = Complex.Sqrt(new Complex(disc, 0));
        result = Tuple.Create(
            (-b + rootDisc)/(2*a),
            (-b - rootDisc)/(2*a)
        );
        
        return WorkflowStatus.Success;
    }

    private WorkflowStatus SolveSimple(double a, double b, double disc, out Tuple<Complex, Complex> result)
    {
        var rootDisc = Math.Sqrt(disc);
        result = Tuple.Create(
            new Complex((-b + rootDisc)/(2*a), 0),
            new Complex((-b - rootDisc)/(2*a), 0)
        ); 

        return WorkflowStatus.Success;
    }
}