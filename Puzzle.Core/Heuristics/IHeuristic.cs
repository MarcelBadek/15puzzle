namespace Puzzle.Core;

public interface IHeuristic
{
    int Calculate(Board board);
}