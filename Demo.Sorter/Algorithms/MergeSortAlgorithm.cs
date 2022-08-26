using Demo.Sorter.Interfaces;

namespace Demo.Sorter.Algorithms;

/// <summary>
/// Merge sort implementation
/// </summary>
/// <para>Time complexity O(nlogn) - we divide in two halves and have linear merge</para>
/// <para>Space complexity O(n) - because we need to store elements</para>
/// TODO: Room at add strategy for the approaches, top-down, bottom-up.. etc
internal sealed class MergeSortAlgorithm<T> : ISortAlgorithm<T>
    where T : IComparable<T>
{
    /// <inheritdoc cref="ISortAlgorithm{T}.Sort"/>
    public T[] Sort(T[] arr)
    {
        if (arr is null) throw new ArgumentNullException(nameof(arr));

        if (arr.Length == 0) return arr;

        return MergeSort(arr, 0, arr.Length - 1);
    }

    private static T[] MergeSort(T[] arr, int start, int end)
    {
        if (end - start + 1 <= 1)
        {
            // Already sorted
            return arr;
        }

        var mid = start + (end - start) / 2;

        MergeSort(arr, start, mid);
        MergeSort(arr, mid + 1, end);

        Merge(ref arr, start, mid, end);

        return arr;
    }

    private static void Merge(ref T[] arr, int start, int mid, int end)
    {
        var left = start;
        var right = mid + 1;

        var idx = 0;
        var sortedArray = new T[end - start + 1];

        while (left <= mid && right <= end)
        {
            if (arr[left].CompareTo(arr[right]) < 0)
            {
                sortedArray[idx++] = arr[left++];
            }
            else
            {
                sortedArray[idx++] = arr[right++];
            }
        }

        while (left <= mid) sortedArray[idx++] = arr[left++];
        while (right <= end) sortedArray[idx++] = arr[right++];

        // map sorted onto original array
        for (var i = start; i <= end; i++)
        {
            arr[i] = sortedArray[i - start];
        }
    }
}