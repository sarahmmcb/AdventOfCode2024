using System.Formats.Asn1;
using System.Text;
using Advent2024Day5;

var pathToInput = @"C:\Dev\Advent2024\Advent2024Day5\input.txt";

if (File.Exists(pathToInput))
{
    var lines = File.ReadAllLines(pathToInput);

    var rules = new List<OrderingRule>();
    var updates = new List<string>();

    foreach(var line in lines)
    {
        var tokens = line.Split('|');
        if (tokens.Length == 2)
        {
            rules.Add(new OrderingRule
            {
                FirstNumber = Int32.Parse(tokens[0]),
                SecondNumber = Int32.Parse(tokens[1]),
            });
        }
        else
        {
            if (string.IsNullOrWhiteSpace(tokens[0]))
                continue;

            updates.Add(tokens[0]);
        }
    }

    var middlePagesOfCorrectUpdates = new List<int>();
    var middlePagesOfCorrectedUpdates = new List<int>();

    foreach(var update in updates)
    {
        // find relevant rules
        var pagesToUpdate = update.Split(',').ToList().Select(p => Int32.Parse(p)).ToList();
        var relevantRules = rules.Where(r => pagesToUpdate.Contains(r.FirstNumber) && pagesToUpdate.Contains(r.SecondNumber)).ToList();
        
        if (KeepsAllRules(pagesToUpdate, relevantRules))
        {
            int middleIndex = pagesToUpdate.Count / 2;
            middlePagesOfCorrectUpdates.Add(pagesToUpdate.ElementAt(middleIndex)); 
        }
        else
        {
            middlePagesOfCorrectedUpdates.Add(GetMiddlePageOfCorrectedUpdate(pagesToUpdate, relevantRules));
        }
    }

    var result = middlePagesOfCorrectUpdates.Sum();
    var resultCorrected = middlePagesOfCorrectedUpdates.Sum();
    Console.WriteLine($"Sum of middle pages for valid updates: {result}");
    Console.WriteLine($"Sum of middle pages for corrected updates: {resultCorrected}");
}

bool KeepsAllRules(List<int> pagesToUpdate, List<OrderingRule> relevantRules)
{
    foreach(var rule in relevantRules)
    {
        var firstIndex = pagesToUpdate.IndexOf(rule.FirstNumber);
        var secondIndex = pagesToUpdate.IndexOf(rule.SecondNumber);

        if (firstIndex < 0
            || secondIndex < 0
            || firstIndex >= secondIndex)
            return false;
    }

    return true;
}

int GetMiddlePageOfCorrectedUpdate(List<int> pages, List<OrderingRule> rules)
{
    var correctedUpdate = GetCorrectedUpdate(pages, rules);

    return correctedUpdate.ElementAt(correctedUpdate.Count / 2);
}

List<int> GetCorrectedUpdate(List<int> pages, List<OrderingRule> rules)
{
    var correctedUpdate = new List<int>();

    foreach(var page in pages)
    {
        InsertPage(page, correctedUpdate, rules);
    }

    return correctedUpdate;
}

void InsertPage(int page, List<int> correctedUpdate, List<OrderingRule> rules)
{
    if (correctedUpdate.Count == 0)
        correctedUpdate.Add(page);
    else
    {
        var relevantRules = rules.Where(r => (r.FirstNumber == page || r.SecondNumber == page) &&
        (correctedUpdate.Contains(r.FirstNumber) || correctedUpdate.Contains(r.SecondNumber))).ToList();

        var idx = 0;
        correctedUpdate.Insert(idx, page);

        while(!KeepsAllRules(correctedUpdate, relevantRules))
        {
            correctedUpdate.RemoveAt(idx);
            correctedUpdate.Insert(++idx, page);
        }
    }
}
