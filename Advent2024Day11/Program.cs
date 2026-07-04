var input = "773 79858 0 71 213357 2937 1 3998391";
//var input = "125 17";

var stones = input.Split(" ").Select(x => Int64.Parse(x)).ToList();

var numBlinks = 75;

for (int i = 0; i < numBlinks; i++)
{
   stones = Blink(stones);
}

List<long> Blink(List<long> stones)
{
    var newStones = new List<long>();
    foreach (var (stone, index) in stones.Select((x, i) => (x,i)))
    {
        if (stone == 0)
        {
            newStones.Add(1);
        }
        else if (HasEvenNumberOfDigits(stone))
        {
            var stoneString = stone.ToString();
            var stoneLength = stoneString.Length;
            newStones.Add(Int32.Parse(stoneString.Substring(0, stoneLength / 2)));
            newStones.Add(Int32.Parse(stoneString.Substring(stoneLength/2, stoneLength/2)));
        }
        else
        {
            newStones.Add(stone * 2024);
        }
    }

    return newStones;
}

//foreach (var stone in stones)
//{
//    Console.WriteLine(stone);
//}

Console.WriteLine(stones.Count);

bool HasEvenNumberOfDigits(long number)
{
    var counter = 0;

    do
    {
        number /= 10;
        counter++;
    }
    while (number != 0);

    return counter % 2 == 0;
}


