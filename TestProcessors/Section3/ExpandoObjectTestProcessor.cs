using System.Dynamic;

namespace TestProcessors;

public class ExpandoObjectTestProcessor : ITestProcessor
{
    public void Execute()
    {
        Console.WriteLine("ExpandoObject demonstration:");

        dynamic person = new ExpandoObject();   //just a dict inside
        person.Name = "John";
        person.Age = 21;

        person.Address = new ExpandoObject();   //making a tree
        person.Address.City = "London";
        person.Address.Country = "UK";

        person.Hello = new Action(() => Console.WriteLine("Hello"));    //Methods - no problem
        person.Hello();

        person.FallsIll = null;
        person.FallsIll += new EventHandler<dynamic>((sender, args) => Console.WriteLine($"Need a doctor for {args}"));
        
        EventHandler<dynamic> e = person.FallsIll;
        e?.Invoke(person, person.Name);

        var dict = (IDictionary<string, object>) person;    //cast to dict
        Console.WriteLine(dict["Name"]);

    }

}