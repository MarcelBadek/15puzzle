using System.Diagnostics;
using Puzzle.Core.Solvers;

var puzzle = new Puzzle.Core.Puzzle(@"C:\Users\macze\Downloads\puzzle.txt");

Console.WriteLine("Board:");
puzzle.Board.DisplayBoard();
Console.WriteLine();

var time = new Stopwatch();
time.Start();

var solver = new BreadthFirstSolver("LURD");
solver.Solve(puzzle.Board);

time.Stop();
Console.WriteLine();
Console.WriteLine(time.ElapsedMilliseconds);