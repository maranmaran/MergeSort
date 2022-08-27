using Demo.Sorter.Implementations;
using Demo.Sorter.Interfaces;

namespace Demo.Sorter.Extensions;

public static class SortExtensions
{
    /// <summary>
    /// Sorts input array
    /// </summary>
    /// <param name="sorter">Sorter implementation you wish to use</param>
    /// <returns>Sorted array</returns>
    public static T[] Sort<T>(this T[] arr, ISorter<T> sorter = null)
        where T : IComparable<T>
        => (sorter ?? ArraySorter<T>.Instance).Sort(arr);
}