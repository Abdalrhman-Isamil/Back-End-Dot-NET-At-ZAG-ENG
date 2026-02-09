
//( Longest Common Prefix )

public class Solution
{
    public string LongestCommonPrefix(string[] words)
    {
        Array.Sort(words);
        int total = words.Length;
        string firstWord = words[0];
        string lastWord = words[total - 1];
        string prefix = "";

        for (int index = 0; index < firstWord.Length; index++)
        {
            if (firstWord[index] == lastWord[index])
            {
                prefix += firstWord[index];
            }
            else
            {
                break;
            }
        }

        return prefix;
    }
}