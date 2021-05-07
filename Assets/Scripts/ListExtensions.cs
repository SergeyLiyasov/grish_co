using System;
using System.Collections.Generic;
using System.Linq;

public static class ListExtensions
{
    public static void Resize<T>(this List<T> list, int size, T value)
    {
        int current = list.Count;
        if (size < current)
            list.RemoveRange(size, current - size);
        else if (size > current)
        {
            if (size > list.Capacity)
                list.Capacity = size;
            list.AddRange(Enumerable.Repeat(value, size - current));
        }
    }

    public static void Resize<T>(this List<T> list, int size)
        => Resize(list, size, default);

    public static void InsertWithResize<T>(this List<T> list, int index, T value)
    {
        if (index < 0) throw new IndexOutOfRangeException();
        if (index >= list.Count) list.Resize(index + 1);
        list[index] = value;
    }
}