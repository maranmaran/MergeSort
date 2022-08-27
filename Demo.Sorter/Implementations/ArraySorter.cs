using Demo.Sorter.Algorithms;
using Demo.Sorter.Interfaces;

namespace Demo.Sorter.Implementations;

/// <inheritdoc cref="ISorter{T}"/>
public sealed class ArraySorter<T> : ISorter<T>
    where T : IComparable<T>
{
    private ISortAlgorithm<T> _algorithm;
    private readonly ISortAlgorithm<T> _default = new MergeSortAlgorithm<T>();

    public static ISorter<T> Instance => Factory();

    private ArraySorter(ISortAlgorithm<T> algorithm = null)
    {
        _algorithm = algorithm ?? _default;
    }

    /// <summary>
    /// Produces instance of <see cref="ArraySorter{T}"/>
    /// </summary>
    private static ISorter<T> Factory() => new ArraySorter<T>();

    /// <inheritdoc cref="ISorter{T}.WithAlgorithm"/>
    public ISorter<T> WithAlgorithm(ISortAlgorithm<T> algorithm)
    {
        if (algorithm is null)
        {
            return this;
        }

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