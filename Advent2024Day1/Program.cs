// See https://aka.ms/new-console-template for more information
using Advent2024Day1;

var arr = new int[] { 4, 33, 7, 9, 10, 0, 2, 5 };

var result = MergeSort.Sort(arr);

foreach(var item in result)
{
    Console.WriteLine(item);
}

var pathToFile = @"C:\Dev\Advent2024\Advent2024Day1\input.txt";

if (File.Exists(pathToFile))
{
    var lines = File.ReadAllLines(pathToFile).ToList<string>();

    var list1 = new List<int>();
    var list2 = new List<int>();

    foreach(var line in lines)
    {
        var values = line.Split(' ');
        list1.Add(Int32.Parse(values[0].Trim()));
        list2.Add(Int32.Parse(values[3].Trim()));
    }

    var sortedList1 = MergeSort.Sort(list1);
    var sortedList2 = MergeSort.Sort(list2);

    var list3 = new List<int>();
    for(int i = 0; i < Math.Min(sortedList1.Length, sortedList2.Length); i++)
    {
        list3.Add(Math.Abs(sortedList1[i] - sortedList2[i]));
    }

    var finalResult = list3.Sum(x => x);
    Console.WriteLine($"Distance Score: {finalResult}");

    // Similarity score
    var similarityScore = new List<int>();

    foreach (var idx in sortedList1)
    {
        similarityScore.Add(idx * sortedList2.Where(x => x == idx).Count());
    }

    var total = similarityScore.Sum(x => x);
    Console.WriteLine($"Similarity Score: {total}");
}

