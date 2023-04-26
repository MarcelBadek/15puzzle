namespace Puzzle.Core.Heuristics;

public interface IHeuristic
{
    int Calculate(Board board);
}