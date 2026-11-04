namespace lab_9;

public readonly record struct SortStatistics(int Comparisons, int Swaps);

public class BubbleSorter : IBubbleSorter
{
   
    public static bool Ascending(int left, int right) => left > right;
    public static bool Descending(int left, int right) => left < right;
    public static bool ByAbsoluteValueAscending(int left, int right) => Math.Abs(left) > Math.Abs(right);
    public static bool EvenFirst(int left, int right)
    {
        var leftIsEven = Math.Abs(left) % 2 == 0;
        var rightIsEven = Math.Abs(right) % 2 == 0;

        if (leftIsEven && !rightIsEven)
        {
            return false;
        }

        if (!leftIsEven && rightIsEven)
        {
            return true;
        }

        return left > right;
    }
    
    public void Sort(int[] array, SortComparison comparison)
    {
        _ = SortInternal(array, comparison);
    }
    
    public void Sort(int[] array, Func<int, int, bool> comparison)
    {
        ArgumentNullException.ThrowIfNull(comparison);
        Sort(array, new SortComparison(comparison));
    }
    
    public int[] SortCopy(int[] array, SortComparison comparison)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(comparison);

        var copy = (int[])array.Clone();
        Sort(copy, comparison);
        return copy;
    }
    
    public int[] SortCopy(int[] array, Func<int, int, bool> comparison)
    {
        ArgumentNullException.ThrowIfNull(comparison);
        return SortCopy(array, new SortComparison(comparison));
    }
    
    public SortStatistics SortWithStatistics(int[] array, SortComparison comparison)
    {
        return SortInternal(array, comparison);
    }

    public void Sort<T>(T[] array, Func<T, T, bool> shouldSwap)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(shouldSwap);

        if (array.Length <= 1)
        {
            return;
        }

        for (var i = 0; i < array.Length - 1; i++)
        {
            var swapped = false;

            for (var j = 0; j < array.Length - 1 - i; j++)
            {
                if (shouldSwap(array[j], array[j + 1]))
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapped = true;
                }
            }

            if (!swapped)
            {
                break;
            }
        }
    }
    
    private static SortStatistics SortInternal(int[] array, SortComparison comparison)
    {
        ArgumentNullException.ThrowIfNull(array);
        ArgumentNullException.ThrowIfNull(comparison);

        var comparisonsCount = 0;
        var swapsCount = 0;

        if (array.Length <= 1)
        {
            return new SortStatistics(comparisonsCount, swapsCount);
        }

        for (var i = 0; i < array.Length - 1; i++)
        {
            var swapped = false;
            
            for (var j = 0; j < array.Length - 1 - i; j++)
            {
                comparisonsCount++;
                
                if (comparison(array[j], array[j + 1]))
                {
                    (array[j], array[j + 1]) = (array[j + 1], array[j]);
                    swapsCount++;
                    swapped = true;
                }
            }
            
            if (!swapped)
            {
                break;
            }
        }

        return new SortStatistics(comparisonsCount, swapsCount);
    }
}
