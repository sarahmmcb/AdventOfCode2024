// See https://aka.ms/new-console-template for more information
var pathToFile = @"C:\Dev\Advent2024\Advent2024Day2\input.txt";

if (File.Exists(pathToFile))
{
    var lines = File.ReadAllLines(pathToFile);
    var result = new List<string>();

    foreach (var line in lines)
    {
        var report = line.Split(" ").Select(s => Int32.Parse(s.Trim())).ToList();
        var differences = report.Zip(report.Skip(1), (x, y) => y-x);

        if (differences.Any(x => Math.Abs(x) > 3) ||
            !differences.All(x => x > 0) &&
            !differences.All(x => x < 0)
        )
        {
            result.Add(ApplyProblemDampener2(report));
            continue;
        }

        result.Add("safe");
    }

    var safeCount = result.Where(x => String.Equals(x, "safe")).Count();

    Console.WriteLine($"There are {safeCount} safe reports");
}

string ApplyProblemDampener2(List<int> report)
{
    for (int i = 0; i < report.Count; i++)
    {
        var newList = new List<int>(report);
        newList.RemoveAt(i);

        var result = EvaluateReport(newList);

        if (string.Equals(result, "safe"))
        {
            return result;
        }
    }

    return "unsafe";
}

string ApplyProblemDampener(List<int> report, IEnumerable<int> differences)
{
    var prevDiff = report[1] - report[0];
    for (int i = 1; i < report.Count; i++)
    {
        if (Math.Abs(prevDiff) > 3 || prevDiff == 0)
        {
            var tempReport = new List<int>(report);
            tempReport.RemoveAt(i);
            var result = EvaluateReport(tempReport);
            if (string.Equals(result, "unsafe"))
            {
                var tempReport2 = new List<int>(report);
                tempReport2.RemoveAt(i - 1);
                return EvaluateReport(tempReport2);
            }
            else
            {
                return result;
            }
        }

        var currDiff = report[i + 1] - report[i];
        if ((prevDiff < 0 && currDiff > 0) || (prevDiff > 0 && currDiff < 0))
        {
            var tempReport = new List<int>(report);
            tempReport.RemoveAt(i);
            var result = EvaluateReport(tempReport);
            if (string.Equals(result, "unsafe"))
            {
                var tempReport2 = new List<int>(report);
                tempReport2.RemoveAt(i + 1);
                return EvaluateReport(tempReport2);
            }
            else
            {
                return result;
            }
        }

        prevDiff = currDiff;
    }

    return "safe";
}

string EvaluateReport(List<int> report)
{
    var newDifferences = report.Zip(report.Skip(1), (x, y) => y - x);

    if (newDifferences.Any(x => Math.Abs(x) > 3) ||
        !newDifferences.All(x => x > 0) &&
        !newDifferences.All(x => x < 0)
    )
    {
        return "unsafe";
    }

    return "safe";
}

bool IsMonotonic(IEnumerable<int> differences)
{
    return differences.All(x => x > 0) || differences.All(x => x < 0);
}