using TestProcessors;

namespace Section6;

public class LocalInversionControlTestProcessor : ITestProcessor
{
    public void Execute()
    {
        var list1 = new List<int>();
        var list2 = new List<int>();

        24.AddTo(list1, list2);

        var opcode = "AND";
        if (opcode.IsOneOf("AND", "OR")) { }

        var person = new Person();
        if (person.HasNo(p => p.Childrens)) { }

        if (person.HasNo(p => p.Childrens).And.HasNo(p => p.Parents)) { }
    }
}

public static class ExtensionMethods
{
    //usually list controls adding elements but now it's element controls adding
    public static T AddTo<T>(this T self, params ICollection<T>[] colls)
    {
        foreach (var coll in colls)
        {
            coll.Add(self);
        }
        return self;
    }

    //similar in this method
    public static bool IsOneOf<T>(this T self, params T[] values)
    {
        return values.Contains(self);
    }

    public static BoolMarker<TSubject> HasNo<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
    {
        return new BoolMarker<TSubject>(! props(self).Any(), self);
    }

    public static BoolMarker<TSubject> HasSome<TSubject, T>(this TSubject self, Func<TSubject, IEnumerable<T>> props)
    {
        return new BoolMarker<TSubject>(props(self).Any(), self);
    }

    public static BoolMarker<T> HasNo<T, TU>(this BoolMarker<T> marker, Func<T, IEnumerable<TU>> props)
    {
        if (marker.PendingOperation == BoolMarker<T>.Operation.And && !marker.Result)
        {
            return marker;
        }

        return new BoolMarker<T>(!props(marker.Self).Any(), marker.Self);
    }

    public struct BoolMarker<T>
    {
        public bool Result;
        public T Self;

        public enum Operation
        {
            None,
            And,
            Or
        }

        internal Operation PendingOperation;
        internal BoolMarker(bool result, T self, Operation pendingOperation)
        {
            Result = result;
            Self = self;
            PendingOperation = pendingOperation;
        }
        public BoolMarker(bool result, T self) : this(result, self, Operation.None) { }
        public BoolMarker<T> And => new BoolMarker<T>(Result, Self, Operation.And);

        public static implicit operator bool(BoolMarker<T> marker)
        {
            return marker.Result;
        }
    }
}