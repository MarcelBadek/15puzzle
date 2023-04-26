namespace Puzzle.Core.Heuristics;

public class Manhattan : IHeuristic
{
    public int Calculate(Board board)
    {
        int result = 0;
        for (int row = 0; row < board.Rows; row++)
        {
            for (int col = 0; col < board.Columns; col++)
            {
                var curr = board.Fields[row, col];
                var rowPos = curr - 1 / board.Columns;
                var colPos = curr - 1 % board.Columns;
                var distance = Math.Abs(row - rowPos) + Math.Abs(col - colPos);
                result += distance;
            }
        }

        return result;
    }
}