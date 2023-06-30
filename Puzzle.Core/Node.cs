using Puzzle.Core.Heuristics;

namespace Puzzle.Core;

public class Node
{
    public Board Board { get; set; } = null!;
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;
    public Node? Parent { get; set; }
    public char? Move { get; set; }

    public Node()
    {
    }

    public Node(Board board, char? move = null, Node? parent = null)
    {
        Parent = parent;
        Board = board;
        Move = move;
    }
}