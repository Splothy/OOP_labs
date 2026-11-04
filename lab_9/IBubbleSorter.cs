namespace lab_9;

public interface IBubbleSorter
{
    void Sort(int[] array, SortComparison comparison);
    int[] SortCopy(int[] array, SortComparison comparison);
}
