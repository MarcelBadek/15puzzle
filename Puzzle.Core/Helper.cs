namespace Puzzle.Core;

public class Helper
{
    public static char? GetReverseMove(char? move)
    {
        return move switch
        {
            'L' => 'R',
            'R' => 'L',
            'U' => 'D',
            'D' => 'U',
            _ => null
        };
    }

    public static bool CompareBoards(Board first, Board second)
    {
        if (first.Rows != second.Rows || first.Columns != second.Columns)
        {
            return false;
        }
        
        for (var row = 0; row < first.Rows; row++)
        {
            for (var col = 0; col < first.Columns; col++)
            {
                if (first.Fields[row, col] != second.Fields[row, col])
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}