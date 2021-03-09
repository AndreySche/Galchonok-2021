using System;
using System.Collections.Generic;
using System.Linq;

public static partial class Utilites
{
    public static List<T> RandomList<T>(this IEnumerable<T> list, int elementsCount)
    {
        return list.OrderBy(arg => Guid.NewGuid()).Take(elementsCount).ToList();
    }
}