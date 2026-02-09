
// Contains Duplicat

public class Solution
{
    public bool ContainsDuplicate(int[] numbers)
    {
        Array.Sort(numbers);
        int length = numbers.Length;

        for (int i = 1; i < length; i++)
        {
            if (numbers[i] == numbers[i - 1])
                return true;
        }

        return false;
    }
}
