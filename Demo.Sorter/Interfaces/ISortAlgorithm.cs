namespace Demo.Sorter.Interfaces;

/// <summary>
/// Defines sorting algorithm
/// </summary>
public interface ISortAlgorithm<T> where T : IComparable<T>
{
    /// <summary>
    /// Sorts input array
    /// </summary>
    /// <param name="arr">Array to sort</param>
    /// <returns>Sorted input array</returns>
    T[] Sort(T[] arr);
}