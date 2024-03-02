using System.Text;

namespace TestProcessors;

public class VisitorPatternDLRTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Visitor pattern demonstration:");

        var expression = new Addition(new Addition(new Literal(1), new Literal(2)), new Literal(3));
        var sb = new StringBuilder();
        ExpressionPrinter.Print(expression, sb);
        System.Console.WriteLine(sb);
    }

    private abstract class Expression
    {

    }

    private class Literal : Expression
    {
        public double Value { get; }

        public Literal(double value)
        {
            Value = value;
        }
    }

    private class Addition : Expression
    {
        public Expression Left { get; }
        public Expression Right { get; }

        public Addition(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }
    }

    private class ExpressionPrinter
    {
        public static void Print(Literal literal, StringBuilder sb)
        {
            sb.Append(literal.Value);
        }

        public static void Print(Addition addition, StringBuilder sb)
        {
            sb.Append("(");
            Print((dynamic)addition.Left, sb);  //used dynamic cast to determinating overload at runtime
            sb.Append(" + ");
            Print((dynamic)addition.Right, sb); //but it would be a runtime exception if the first overload was deleted
            sb.Append(")");

            //it is not cheap. You pay performance for flexibility
        }
    }
}