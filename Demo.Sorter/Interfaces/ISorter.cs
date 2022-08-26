namespace Demo.Sorter.Interfaces;

/// <summary>
/// Sorts arrays
/// </summary>
internal interface ISorter<T> : ISortAlgorithm<T>
    where T : IComparable<T>
{ }