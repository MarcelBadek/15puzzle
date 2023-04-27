using System.Diagnostics;
using Puzzle.Core;
using Puzzle.Core.Heuristics;

var puzzle = new Puzzle.Core.Puzzle(@"C:\Users\macze\Downloads\puzzle.txt");

puzzle.Board.DisplayBoard();
var time = new Stopwatch();
time.Start();
puzzle.AStar(new Hamming());
// puzzle.BreadthFirstSearch("LURD");
time.Stop();
Console.WriteLine(time.ElapsedMilliseconds);

var board = new Board(@"C:\Users\macze\Downloads\puzzle.txt");
board.CheckBoard();