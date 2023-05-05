namespace Puzzle.Core.Solvers;

public class BreadthFirstSolver : ISolver
{
    private readonly string _order;

    public BreadthFirstSolver(string order)
    {
        _order = order;
    }

    public SolveResult Solve(Board board)
    {
        var processedStates = 0;
        var mainNode = new Node(board)
        {
            G = 0
        };
        var openList = new Queue<Node>();
        var visited = new List<Node>();

        openList.Enqueue(mainNode);

        while (true)
        {
            var currNode = openList.Dequeue();
            visited.Add(currNode);

            if (currNode.Board.CheckBoard())
            {
                var node = currNode;
                var path = string.Empty;
                while (node.Parent is not null)
                {
                    path += node.Move;
                    node = node.Parent;
                }

                path = string.Concat(path.Reverse().ToArray());

                return new SolveResult()
                {
                    MaxDepth = currNode.G,
                    AmountOfVisitedStates = visited.Count,
                    AmountOfProcessedStates = processedStates,
                    Solution = path
                };
            }

            foreach (var move in _order)
            {
                processedStates++;
                if (currNode.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var newBoard = new Board(currNode.Board);

                if (!newBoard.Move(move))
                {
                    continue;
                }

                var newNode = new Node(newBoard, move, currNode)
                {
                    G = currNode.G + 1
                };

                openList.Enqueue(newNode);
            }
        }
    }
}