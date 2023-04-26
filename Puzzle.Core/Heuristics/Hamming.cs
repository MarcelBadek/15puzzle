namespace Puzzle.Core;

public class Hamming : IHeuristic
{
    public int Calculate(Board board)
    {
        var diffs = 0;

        for (int row = 0; row < board.Rows; row++)
        {
            for (int col = 0; col < board.Columns; col++)
            {
                if (board.Fields[row, col] != (row * board.Columns + col + 1))
                {
                    diffs++;
                }
            }
        }

        if (board.Fields[board.Rows - 1, board.Columns - 1] == 0)
        {
            return diffs - 1;
        }
        
        return diffs;
    }
}