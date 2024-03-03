using System.Runtime.InteropServices;

namespace TestProcessors;

public class SpanTestProcessor : ITestProcessor
{
    //Types of memory
    //*Managed memory (new operator):
    //Small objects <85k (generational part of managed heap)
    //Large objects >85k (Large Object Heap, LOH) - GB would be a bit slower
    //*Unmanaged memory:
    //Allocated on unmanaged heap with Marshal.AllocHGlobal/CoTaskMem
    //Released with Marshal.FreeHGlobal/CoTaskMem
    //GC is not involved
    //*Stack memory
    //Vary fast allocation/deallocation
    //Vary small (<1Mb), overalocate and you get SO/process dies
    //Method scoped: method ends, stack unwinds
    public void Execute()
    {
        Console.WriteLine("Span<T> demonstration:");

        unsafe
        {
            byte* ptr = stackalloc byte[100];
            Span<byte> memory = new Span<byte>(ptr, 100);

            IntPtr unmanagedPtr = Marshal.AllocHGlobal(123);
            Span<byte> unmanagedMemory = new Span<byte>(unmanagedPtr.ToPointer(), 123);
            Marshal.FreeHGlobal(unmanagedPtr);
        }

        char[] staff = "hello".ToCharArray();
        Span<char> arrayMemory = staff;

        ReadOnlySpan<char> readOnlyMemory = "hi there".AsSpan();
        
        arrayMemory.Fill('x');
        Console.WriteLine(staff);
        arrayMemory.Clear();
        Console.WriteLine($"{staff} - {staff.Length}");
    }
}