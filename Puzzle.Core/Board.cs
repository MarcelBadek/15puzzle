using System.Text;

namespace Puzzle.Core;

public class Board
{
    public int[,] Fields { get; set; }
    public int Rows { get; set; }
    public int Columns { get; set; }

    public Board(string fileName)
    {
        var file = File.ReadAllLines(fileName);
        var size = file[0].Split(' ');
        Rows = int.Parse(size[0]);
        Columns = int.Parse(size[1]);
        Fields = new int[Rows, Columns];
        for (var i = 0; i < Rows; i++)
        {
            var line = file[i + 1].Split(' ');
            for (var j = 0; j < Columns; j++)
            {
                Fields[i, j] = int.Parse(line[j]);
            }
        }
    }

    public Board(Board board)
    {
        Rows = board.Rows;
        Columns = board.Columns;
        Fields = new int[Rows, Columns];

        Array.Copy(board.Fields, Fields, Rows * Columns);
    }

    public void DisplayBoard()
    {
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                if (Fields[i, j] < 10)
                {
                    Console.Write(" " + Fields[i, j] + " ");
                }
                else
                {
                    Console.Write(Fields[i, j] + " ");
                }
            }

            Console.Write('\n');
        }
    }

    public bool Move(char? where)
    {
        if (where == null)
        {
            return false;
        }

        var positionX = -1;
        var positionY = -1;
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                if (Fields[i, j] == 0)
                {
                    positionY = i;
                    positionX = j;
                    break;
                }
            }

            if (positionX != -1)
            {
                break;
            }
        }

        if (where == 'L')
        {
            if (positionX - 1 >= 0)
            {
                Fields[positionY, positionX] = Fields[positionY, positionX - 1];
                Fields[positionY, positionX - 1] = 0;
                return true;
            }
        }
        else if (where == 'R')
        {
            if (positionX + 1 < Columns)
            {
                Fields[positionY, positionX] = Fields[positionY, positionX + 1];
                Fields[positionY, positionX + 1] = 0;
                return true;
            }
        }
        else if (where == 'U')
        {
            if (positionY - 1 >= 0)
            {
                Fields[positionY, positionX] = Fields[positionY - 1, positionX];
                Fields[positionY - 1, positionX] = 0;
                return true;
            }
        }
        else if (where == 'D')
        {
            if (positionY + 1 < Rows)
            {
                Fields[positionY, positionX] = Fields[positionY + 1, positionX];
                Fields[positionY + 1, positionX] = 0;
                return true;
            }
        }

        return false;
    }

    public bool CheckBoard()
    {
        var diffs = 0;

        for (int row = 0; row < Rows; row++)
        {
            for (int col = 0; col < Columns; col++)
            {
                if (Fields[row, col] != (row * Columns + col + 1))
                {
                    diffs++;
                }
            }
        }

        if (Fields[Rows - 1, Columns - 1] == 0 && diffs == 1)
        {
            return true;
        }

        return false;
    }

    public override string ToString()
    {
        var builder = new StringBuilder();
        for (var i = 0; i < Rows; i++)
        {
            for (var j = 0; j < Columns; j++)
            {
                builder.Append(Fields[i, j]);
                builder.Append(' ');
            }
        }

        return builder.ToString();
    }
}