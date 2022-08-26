using System.Collections;
using System.Reflection;
using Demo.Sorter.Algorithms;
using Demo.Sorter.Implementations;
using Xunit;

namespace Demo.SorterTests;

public class ArraySorterTests
{
    [Fact]
    public void Sorter_Default_IsMergeSort()
    {
        var mergeSortAlgorithmType = typeof(MergeSortAlgorithm<>).GetGenericTypeDefinition();

        var sut = new IntArraySorter();

        var prop = sut.GetType().BaseType?.GetField("_algorithm", BindingFlags.Instance | BindingFlags.NonPublic);
        var value = prop?.GetValue(sut)?.GetType();

        Assert.NotNull(value);
        Assert.True(value!.IsGenericType);
        Assert.Equal(mergeSortAlgorithmType, value.GetGenericTypeDefinition());
    }

    [Fact]
    public void Sorter_NullInput_ThrowsArgumentNullException()
    {
        var sut = new IntArraySorter();

        Assert.Throws<ArgumentNullException>(() => sut.Sort(null));
    }

    [Fact]
    public void Sorter_BasicInput_Sorts()
    {
        var sut = new IntArraySorter();

        var resultArr = sut.Sort(new[] { 4, 3, 2, 1 });

        AssertResult(new[] { 1, 2, 3, 4 }, resultArr, false);
    }

    [Theory]
    [ClassData(typeof(IntSorterTests))]
    public void Sorter_Default_Sorts(int[] testArr)
    {
        var sut = new IntArraySorter();

        var resultArr = sut.Sort(testArr);

        AssertResult(testArr, resultArr);
    }

    [Fact]
    public void Sorter_BigArray_Sorts()
    {
        var bigArr = SeedArray();

        var sut = new IntArraySorter();

        var resultArr = sut.Sort(bigArr);

        Array.Sort(bigArr);

        AssertResult(bigArr, resultArr);
    }

    private static int[] SeedArray(int max = 5000)
    {
        var rand = new Random(Guid.NewGuid().GetHashCode());

        var arr = new int[max];

        for (var i = 0; i < max; i++)
        {
            arr[i] = rand.Next(int.MinValue, int.MaxValue);
        }

        return arr;
    }

    private static void AssertResult(int[] expected, int[] actual, bool evaluateRef = true)
    {
        Array.Sort(actual);

        Assert.Equal(expected, actual);

        if (evaluateRef)
        {
            Assert.Equal(expected?.GetHashCode(), actual?.GetHashCode());
        }
    }
}

public class IntSorterTests : IEnumerable<object[]>
{
    private readonly List<object[]> _data = new()
    {
        new object[] { new [] { 5, 1, 3, 9 } },
        new object[] { new [] { 7, 1, 5, 3} },
        new object[] { new [] { 0 } },
        new object[] { new [] { -5, -6, -7, 1000} }
    };

    public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}