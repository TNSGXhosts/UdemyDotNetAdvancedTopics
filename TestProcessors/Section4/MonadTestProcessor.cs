namespace TestProcessors;

public class MonadTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("Monad demonstration:");

        var employee = new Employee();
        string postCode = employee.With(e => e.Address).With(e => e.Postcode);  //null safety

        postCode = employee
            .If(HasMedicalRecord)
            .With(e => e.Address)
            .Do(CheckAddress)
            .Return(a => a.Postcode, "UNKNOWN");
    }

    private bool HasMedicalRecord(Employee employee)
    {
        //logic
        return true;
    }

    private void CheckAddress(Address employee)
    {
        //logic
    }
}

public static class Monad
{
    public static TResult With<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator)
        where TInput : class
        where TResult : class
    {
        if (o == null) return null;

        return evaluator(o);
    }

    public static TInput If<TInput>(this TInput o, Func<TInput, bool> evaluator) where TInput : class
    {
        if (o == null) return null;

        return evaluator(o) ? o : null;
    }

    public static TInput Do<TInput>(this TInput o, Action<TInput> action) where TInput : class
    {
        if (o == null) return null;
        action(o);
        return o;
    }

    public static TResult Return<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator, TResult failureValue) where TInput : class
    {
        if (o == null) return failureValue;
        return evaluator(o);
    }

    public static TResult WithValue<TInput, TResult>(this TInput o, Func<TInput, TResult> evaluator) where TInput : struct
    {
        return evaluator(o);
    }
}

public class Employee
{
    public Address Address { get; set; }
}

public class Address
{
    public string Postcode { get;set; }
}