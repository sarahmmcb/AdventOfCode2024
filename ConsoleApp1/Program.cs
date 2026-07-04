// See https://aka.ms/new-console-template for more information
var solution = new Solution();

var output = solution.Convert("PAYPALISHIRING", 3);

Console.WriteLine(output);


public class Solution
{
    public string Convert(string s, int numRows)
    {
        var chars = new List<char>();

        for (int i = 0; i < numRows; i++)
        {
            printRow(s, i, numRows, chars);
        }

        return string.Concat(chars);
    }

    private void printRow(string s, int startingIndex, int numRows, List<char> chars)
    {
        if (numRows == 1)
        {
            chars.Add(s[startingIndex]);
            return;
        }

        var spacer = 0;
        var remainingRows = numRows - 1 - startingIndex;
        while (true)
        {
            var charIndex = startingIndex + spacer * (2 * numRows - 2);
            if (charIndex >= s.Length)
                return;
            chars.Add(s[charIndex]);
            if (startingIndex > 0 && startingIndex < numRows - 1) {
                // Add middle character
                var middleIndex = charIndex + (2 * remainingRows);
                if (middleIndex >= s.Length)
                return;
                chars.Add(s[middleIndex]);
            }
            spacer++;
        }
    }
}