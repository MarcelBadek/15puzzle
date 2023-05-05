using Puzzle.Core.Heuristics;

namespace Puzzle.Core;

public class Node
{
    public Board Board { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;
    public Node? Parent { get; set; }
    public char? Move { get; set; }

    public Node(IHeuristic heuristic, Board board, char? move = null, Node? parent = null)
    {
        Parent = parent;
        if (Parent is null)
        {
            G = 0;
        }
        else
        {
            G = Parent.G + 1;
        }
        Board = board;
        H = heuristic.Calculate(Board);
        Move = move;
    }

    public Node(Board board, char? move = null, Node? parent = null)
    {
        Parent = parent;
        Board = board;
        Move = move;
    }
}