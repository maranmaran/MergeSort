using System.Collections;
using System.Reflection;
using Demo.Sorter.Algorithms;
using Demo.Sorter.Extensions;
using Demo.Sorter.Implementations;
using Demo.Sorter.Interfaces;
using Xunit;

namespace Demo.SorterTests;

public class ArraySorterTests
{
    [Fact]
    public void Sorter_NullInput_ThrowsArgumentNullException()
    {
        var sut = ArraySorter<int>.Instance;

        Assert.Throws<ArgumentNullException>(() => sut.Sort(null));
    }

    [Fact]
    public void Sorter_BasicInput_Sorts()
    {
        var resultArr = new[] { 4, 3, 2, 1 }.Sort();

        AssertResult(new[] { 1, 2, 3, 4 }, resultArr, false);
    }

    [Theory]
    [ClassData(typeof(IntSorterTests))]
    public void Sorter_Default_Sorts(int[] testArr)
    {
        var resultArr = testArr.Sort();

        AssertResult(testArr, resultArr);
    }

    [Fact]
    public void Sorter_BigArray_Sorts()
    {
        var bigArr = SeedArray();

        var resultArr = bigArr.Sort();

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

    [Fact]
    public void Sorter_Default_IsMergeSort()
    {
        var mergeSortAlgorithmType = typeof(MergeSortAlgorithm<>).GetGenericTypeDefinition();

        var sut = ArraySorter<int>.Instance;

        var prop = sut.GetType().GetField("_algorithm", BindingFlags.Instance | BindingFlags.NonPublic);
        var value = prop?.GetValue(sut)?.GetType();

        Assert.NotNull(value);
        Assert.True(value!.IsGenericType);
        Assert.Equal(mergeSortAlgorithmType, value.GetGenericTypeDefinition());
    }

    [Fact]
    public void ArraySorter_CustomSorter_Sorts()
    {
        var whoCameFirst = new IQuestion[]
        {
            new Egg(),
            new Chicken()
        };

        var sorter = ArraySorter<IQuestion>.Instance.WithAlgorithm(new QuestionableTestAlgorithm());

        whoCameFirst.Sort(sorter);

        Assert.Equal(typeof(Chicken), whoCameFirst.First().GetType());
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

public interface IQuestion : IComparable<IQuestion>
{ }

public class Chicken : IQuestion
{
    public int CompareTo(IQuestion other) => other.GetType().Name == "Chicken" ? 1 : -1;
}

public class Egg : IQuestion
{
    public int CompareTo(IQuestion other) => other.GetType().Name == "Chicken" ? 1 : -1;
}

public class QuestionableTestAlgorithm : ISortAlgorithm<IQuestion>
{
    public IQuestion[] Sort(IQuestion[] arr)
    {
        for (var i = 0; i < arr.Length - 1; i++)
        {
            if (arr[i].CompareTo(arr[i + 1]) > 0)
            {
                (arr[i + 1], arr[i]) = (arr[i], arr[i + 1]);
            }
        }

        return arr;
    }
}