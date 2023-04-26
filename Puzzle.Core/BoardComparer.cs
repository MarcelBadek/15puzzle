namespace Puzzle.Core;

public class BoardComparer : IComparer<Node>
{
    public int Compare(Node? x, Node? y)
    {
        return x!.F.CompareTo(y!.F);
    }
}