using System.Diagnostics;
using Puzzle.Core;
using Puzzle.Core.Heuristics;

var puzzle = new Puzzle.Core.Puzzle(@"C:\Users\macze\Downloads\puzzle.txt");

Console.WriteLine("Board:");
puzzle.Board.DisplayBoard();
Console.WriteLine();

var time = new Stopwatch();
time.Start();

time.Stop();
Console.WriteLine(time.ElapsedMilliseconds);
