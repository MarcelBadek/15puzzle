namespace Puzzle.Core.Solvers;

public interface ISolver
{
   SolveResult Solve(Board board);
}