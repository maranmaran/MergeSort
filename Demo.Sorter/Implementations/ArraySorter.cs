using Demo.Sorter.Algorithms;
using Demo.Sorter.Interfaces;

namespace Demo.Sorter.Implementations;

/// <inheritdoc cref="ISorter{T}"/>
public abstract class ArraySorter<T> : ISorter<T>
    where T : IComparable<T>
{
    private ISortAlgorithm<T> _algorithm;

    protected ArraySorter(ISortAlgorithm<T> algorithm = null)
    {
        _algorithm = algorithm ?? new MergeSortAlgorithm<T>();
    }

    /// <summary>
    /// Applies algorithm to the sorter
    /// </summary>
    public ArraySorter<T> WithAlgorithm(ISortAlgorithm<T> algorithm)
    {
        _algorithm = algorithm;
        return this;
    }

    /// <summary>
    /// Sorts input array
    /// </summary>
    /// <returns>Sorted array</returns>
    /// <example>
    ///     <code>
    ///         var sorter = new Sorter();
    ///         sorter.Sort(arr);
    ///     </code>
    /// </example>
    /// <example>
    ///     <code>
    ///         var sorter = new Sorter().WithAlgorithm(new SortAlgorithm());
    ///         sorter.Sort(arr);
    ///     </code>
    /// </example>
    public T[] Sort(T[] arr)
    {
        if (arr is null) throw new ArgumentNullException(nameof(arr));

        if (arr.Length == 0) return arr;

        // do business logic

        return _algorithm.Sort(arr);
    }
}