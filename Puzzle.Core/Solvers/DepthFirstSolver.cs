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
        var processedStates = 0;
        var stack = new Stack<Node>();
        var visited = new List<Node>();
        var currNode = new Node(board)
        {
            G = 0
        };

        stack.Push(currNode);

        while (true)
        {
            currNode = stack.Pop();
            visited.Add(currNode);

            if (currNode.Board.CheckBoard())
            {
                var path = string.Empty;
                var node = currNode;
                while (node.Parent is not null)
                {
                    path += node.Move;
                    node = node.Parent;
                }

                path = string.Concat(path.Reverse().ToArray());

                return new SolveResult
                {
                    Solution = path,
                    MaxDepth = visited.Max(x => x.G),
                    AmountOfVisitedStates = visited.Count,
                    AmountOfProcessedStates = processedStates
                };
            }

            if (currNode.G == _maxDepth)
            {
                continue;
            }

            foreach (var move in _order.Reverse())
            {
                processedStates++;
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