using System.Reflection;

namespace TestProcessors;

public class DelegatsEventsProcessor : ITestProcessor
{
    public event EventHandler<int> Event;

    public void Handle(object sender, int arg)
    {
        Console.WriteLine($"I have got {arg}");
    }

    public void Execute()
    {
        Console.WriteLine("Delegats and Events:");

        var eventInfo = typeof(DelegatsEventsProcessor).GetEvents().First();
        var handleMethod = typeof(DelegatsEventsProcessor).GetMethod("Handle");

        Delegate handler = () => { };
        if (eventInfo != null && handleMethod != null)
            handler = Delegate.CreateDelegate(eventInfo.EventHandlerType, null, handleMethod);

        eventInfo?.AddEventHandler(this, handler);

        Event?.Invoke(this, 1);
    }
}