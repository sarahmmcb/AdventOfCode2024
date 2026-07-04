var filepath = @"C:\Dev\Advent2024\Advent2024Day20\input.txt";

if(File.Exists(filepath))
{
    var lines = File.ReadAllLines(filepath);

    var mazeCols = lines[0].Length;
    var mazeRows = lines.Length;
    var maze = new string[mazeRows, mazeCols];
    int row = 0;
    int startRow = 0, startCol = 0, endRow = 0, endCol = 0;
    var shortCuts = new Dictionary<(int row, int col), int>();

    foreach (var line in lines)
    {
        for (int i = 0; i < mazeCols; i++)
        {
            maze[row, i] = line[i].ToString();
            if (string.Equals(maze[row, i], "S"))
            {
                startRow = row;
                startCol = i;
            }

            if (string.Equals(maze[row, i], "E"))
            {
                endRow = row;
                endCol = i;
            }

            if (maze[row, i] == "#" && 
                i > 0 && i < mazeCols - 1 &&
                row > 0 && row < mazeRows - 1 &&
                (
                  ((maze[row, i - 1] == "." || maze[row, i - 1] == "S" || maze[row, i - 1] == "E") &&
                   (line[i + 1].ToString() == "." || line[i + 1].ToString() == "S" || line[i + 1].ToString() == "E")) ||
                  ((maze[row - 1, i] == "." || maze[row - 1, i] == "S" || maze[row - 1, i] == "E") &&
                   (lines[row + 1][i].ToString() == "." || lines[row + 1][i].ToString() == "S" || lines[row + 1][i].ToString() == "E"))
                )
               )
            {
                shortCuts.TryAdd((row, i), 0);
            }
        }

        row++;
    }

    //for (int i = 0; i < 15; i++)
    //{
    //    for (int k = 0; k < 15; k++)
    //    {
    //        Console.Write(maze[i,k]);
    //    }

    //    Console.WriteLine();
    //}

    Console.WriteLine($"startRow, startCol: {(startRow, startCol)}");
    Console.WriteLine($"endRow, endCol: {(endRow, endCol)}");

    var basePicoSeconds = BFS(maze, startRow, startCol, endRow, endCol);

    Console.WriteLine($"basePicoSeconds: {basePicoSeconds}");

    //var shortCut = shortCuts.Keys.First();
    //maze[shortCut.row, shortCut.col] = ".";
    //var newPicoSeconds = BFS(maze, startRow, startCol, endRow, endCol);
    //shortCuts[shortCut] = newPicoSeconds;
    //Console.WriteLine($"ShortCut: ({shortCut.row}, {shortCut.col}), picoSeconds: {newPicoSeconds}");


    // Try shortcuts
    foreach (var shortCut in shortCuts.Keys)
    {
        maze[shortCut.row, shortCut.col] = ".";

        var newPicoSeconds = BFS(maze, startRow, startCol, endRow, endCol);
        shortCuts[shortCut] = newPicoSeconds;
        Console.WriteLine($"ShortCut: ({shortCut.row}, {shortCut.col}), picoSeconds: {newPicoSeconds}");

        maze[shortCut.row, shortCut.col] = "#";
    }

   Console.WriteLine($"Number of shortCuts that save at least 100:{shortCuts.Where(pair => basePicoSeconds - pair.Value >= 100).Count()}");

   
}

int BFS(string[,] maze, int startRow, int startCol, int endRow, int endCol)
{
    var picoSeconds = 0;
    int[] drow = { -1, 0, 1, 0 }; // up, right, down, left
    int[] dcol = { 0, 1, 0, -1 };

    int rows = maze.GetLength(0);
    int cols = maze.GetLength(1);
    var visited = new bool[rows, cols];
    var queue = new Queue<(int row, int col)>();
    Dictionary<(int x, int y), (int x, int y)?> parents = new Dictionary<(int x, int y), (int x, int y)?>();
    parents[(startRow, startCol)] = null;

    queue.Enqueue((startRow, startCol));
    visited[startRow, startCol] = true;

    while (queue.Count > 0)
    {
        var current = queue.Dequeue();
       // picoSeconds++;

        if (current.row == endRow && current.col == endCol)
        {
            picoSeconds = CalculateShortestPathLength(parents, (endRow, endCol));
        }

        for (int i = 0; i < 4; i++)
        {
            var newRow = current.row + drow[i];
            var newCol = current.col + dcol[i];

            if (newRow >= 0 && newCol >= 0 && newRow < cols && newCol < rows
                && maze[newRow, newCol] != "#" && !visited[newRow, newCol])
            {
                visited[newRow, newCol] = true;
                queue.Enqueue((newRow, newCol));
                parents[(newRow, newCol)] = current;
                //Console.WriteLine($"newX, newY: {(newRow, newCol)}");
            }
        }
    }

    return picoSeconds;
}

int CalculateShortestPathLength(Dictionary<(int x, int y), (int x, int y)?> parents, (int x, int y) end)
{
    int picoSeconds = -1;

    var path = new List<(int row, int col)?>();
    (int x, int y)? current = end;

    while (current != null)
    {
        path.Add(current);
        picoSeconds++;
        current = parents[((int x, int y))current];
    }

    return picoSeconds;
}