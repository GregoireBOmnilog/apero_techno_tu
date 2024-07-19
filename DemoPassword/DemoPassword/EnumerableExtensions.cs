using System;
using System.Collections.Generic;

public static class EnumerableExtensions
{
    public static void ForEachPair<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Action<T1, T2> action)
    {
        if (first == null) throw new ArgumentNullException(nameof(first));
        if (second == null) throw new ArgumentNullException(nameof(second));
        if (action == null) throw new ArgumentNullException(nameof(action));

        using (var enumerator1 = first.GetEnumerator())
        using (var enumerator2 = second.GetEnumerator())
        {
            while (enumerator1.MoveNext() && enumerator2.MoveNext())
            {
                action(enumerator1.Current, enumerator2.Current);
            }
        }
    }
}
