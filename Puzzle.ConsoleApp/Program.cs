using System.Diagnostics;
using Puzzle.Core;
using Puzzle.Core.Heuristics;
using Puzzle.Core.Solvers;

if (args.Length == 0)
{
    return;
}

var algorithm = args[0];
var param = args[1];
var inputFile = args[2];
var solutionFile = args[3];
var statisticsFile = args[4];

ISolver solver;
switch (algorithm)
{
    case "bfs":
    {
        if (!IsOrderPermutationValid(param))
        {
            throw new ArgumentException("Not suported argument");
        }

        solver = new BreadthFirstSolver(param);
        break;
    }
    case "dfs":
    {
        if (!IsOrderPermutationValid(param))
        {
            throw new ArgumentException("Not suported argument");
        }

        solver = new DepthFirstSolver(param, 20);
        break;
    }
    case "astr":
    {
        IHeuristic heuristic = param switch
        {
            "manh" => new Manhattan(),
            "hamm" => new Hamming(),
            _ => throw new ArgumentException("Not suported argument")
        };

        solver = new AStarSolver(heuristic);

        break;
    }
    default:
        throw new ArgumentException("Algorithm does not exists");
}

var board = new Board(inputFile);

var startTime = Stopwatch.GetTimestamp();

var result = solver.Solve(board);

result.CreateSolutionFile(solutionFile);
result.CreateStatisticsFile(statisticsFile, Stopwatch.GetElapsedTime(startTime).TotalMilliseconds);

bool IsOrderPermutationValid(string order)
{
    if (order.Length != 4)
    {
        return false;
    }

    return order.Contains('R') && order.Contains('L') && order.Contains('U') && order.Contains('D');
}