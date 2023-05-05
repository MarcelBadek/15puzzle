using System.Diagnostics;
using Puzzle.Core;
using Puzzle.Core.Heuristics;

var puzzle = new Puzzle.Core.Puzzle(@"C:\Users\kriol\Desktop\puzzle.txt");

Console.WriteLine("Board:");
puzzle.Board.DisplayBoard();
Console.WriteLine();

var time = new Stopwatch();
time.Start();
//puzzle.AStar(new Manhattan());
//puzzle.BreadthFirstSearch("LURD");
puzzle.DepthFirstSearch("LURD", 100);
//puzzle.DepthFirstSearchRec("LURD", 100);
time.Stop();
Console.WriteLine("Time elapsed (ms): " + time.ElapsedMilliseconds);


// string order = "LURD";
// foreach (var move in order.Reverse())
// {
//     Console.Write(move);
// }