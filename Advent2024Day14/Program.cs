
using System.Text.RegularExpressions;
using Advent2024Day14;

var filepath = @"C:\Dev\Advent2024\Advent2024Day14\input.txt";
var width = 101;
var height = 103;

if (File.Exists(filepath))
{
    var lines = File.ReadAllLines(filepath);
    var robots = new List<Robot>();

    // load the robots
    foreach (var line in lines)
    {
        var points = line.Split(' ');

        var positionMatches = Regex.Matches(points[0], @"-?\d+");
        var velocityMatches = Regex.Matches(points[1], @"-?\d+");

        var position = new OrderedPair
        {
            X = int.Parse(positionMatches[0].Value),
            Y = int.Parse(positionMatches[1].Value)
        };

        var velocity = new OrderedPair
        {
            X = int.Parse(velocityMatches[0].Value),
            Y = int.Parse(velocityMatches[1].Value)
        };

        robots.Add(new Robot(position, velocity));
    }

    // move the robots
    for(var i = 0; i < 100; i++)
    {
        foreach (var robot in robots)
        {
           robot.BoundedMove(width, height);
        }
    }

    // Print resulting positions
    foreach(var robot in robots)
    {
        Console.WriteLine($"{robot.Position.X}, {robot.Position.Y}");
    }

    // calculate safety factor
    var firstQuadrant = robots.Where(r => r.Position.X < width / 2 && r.Position.Y < height / 2).Count();
    var secondQuadrant = robots.Where(r => r.Position.X > width / 2 && r.Position.Y < height / 2).Count();
    var thirdQuadrant = robots.Where(r => r.Position.X < width / 2 && r.Position.Y > height / 2).Count();
    var fourthQuadrant = robots.Where(r => r.Position.X > width / 2 && r.Position.Y > height / 2).Count();

    Console.WriteLine($"Safety Factor: {firstQuadrant * secondQuadrant * thirdQuadrant * fourthQuadrant}");

}