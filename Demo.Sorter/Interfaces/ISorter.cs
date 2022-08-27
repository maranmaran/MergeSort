namespace Demo.Sorter.Interfaces;

/// <summary>
/// Sorts arrays
/// </summary>
public interface ISorter<T> : ISortAlgorithm<T>
    where T : IComparable<T>
{
    /// <summary>
    /// Applies algorithm to the sorter
    /// </summary>
    ISorter<T> WithAlgorithm(ISortAlgorithm<T> algorithm);
}