using Puzzle.Core;
using Puzzle.Core.Heuristics;

var puzzle = new Puzzle.Core.Puzzle(@"C:\users\macze\downloads\puzzle.txt");

puzzle.AStar(new Manhattan());