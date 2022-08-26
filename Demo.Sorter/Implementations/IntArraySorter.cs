using Demo.Sorter.Interfaces;

namespace Demo.Sorter.Implementations;

/// <inheritdoc cref="ArraySorter{T}"/>
public sealed class IntArraySorter : ArraySorter<int>
{
    public IntArraySorter(ISortAlgorithm<int> algorithm = null) : base(algorithm)
    {
    }
}