using System.Diagnostics;
using Puzzle.Core;
using Puzzle.Core.Heuristics;

var puzzle = new Puzzle.Core.Puzzle(@"C:\Users\kriol\Desktop\puzzle.txt");

puzzle.Board.DisplayBoard();
var time = new Stopwatch();
time.Start();
//puzzle.AStar(new Manhattan());
puzzle.BreadthFirstSearch("LURD");
time.Stop();
Console.WriteLine(time.ElapsedMilliseconds);
puzzle.Board.DisplayBoard();