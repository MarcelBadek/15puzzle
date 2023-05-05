namespace Puzzle.Core.Solvers;

public class DepthFirstSolver : ISolver
{
    private readonly string _order;
    private readonly int _maxDepth;

    public DepthFirstSolver(string order, int maxDepth)
    {
        _order = order;
        _maxDepth = maxDepth;
    }

    public SolveResult Solve(Board board)
    {
        var stack = new Stack<Node>();
        var currNode = new Node(board)
        {
            G = 0
        };

        if (currNode.Board.CheckBoard())
        {
            currNode.Board.DisplayBoard();
            return new SolveResult();
        }

        stack.Push(currNode);

        while (true)
        {
            currNode = stack.Pop();

            if (currNode.Board.CheckBoard())
            {
                currNode.Board.DisplayBoard();

                Console.WriteLine();
                while (currNode.Parent != null)
                {
                    Console.Write(currNode.Move);
                    currNode = currNode.Parent;
                }

                Console.WriteLine();

                return new SolveResult();
            }

            if (currNode.G == _maxDepth)
            {
                continue;
            }

            foreach (var move in _order.Reverse())
            {
                var newBoard = new Board(currNode.Board);

                if (!newBoard.Move(move))
                {
                    continue;
                }

                var newNode = new Node(newBoard, move, currNode)
                {
                    G = currNode.G + 1
                };
                stack.Push(newNode);
            }
        }
    }
}