using Puzzle.Core.Heuristics;

namespace Puzzle.Core.Solvers;

public class AStarSolver : ISolver
{
    private readonly IHeuristic _heuristic;

    public AStarSolver(IHeuristic heuristic)
    {
        _heuristic = heuristic;
    }

    public SolveResult Solve(Board board)
    {
        var processedStates = 0;
        var startNode = new Node()
        {
            Board = board,
            G = 0,
            H = _heuristic.Calculate(board)
        };
        var openList = new PriorityQueue<Node, int>();
        var visited = new Dictionary<string, Node>();
        const string order = "LURD";
        var path = string.Empty;

        openList.Enqueue(startNode, startNode.F);

        while (true)
        {
            var current = openList.Dequeue();
            visited.Add(current.Board.ToString(), current);

            if (current.H == 0)
            {
                var node = current;
                while (node.Parent is not null)
                {
                    path += node.Move;
                    node = node.Parent;
                }

                path = string.Concat(path.Reverse().ToArray());

                return new SolveResult
                {
                    Solution = path,
                    MaxDepth = current.G,
                    AmountOfVisitedStates = visited.Count,
                    AmountOfProcessedStates = processedStates
                };
            }

            foreach (var move in order)
            {
                processedStates++;
                var newBoard = new Board(current.Board);

                // ten przypadek istnieje - brak ruchu
                if (!newBoard.Move(move))
                {
                    continue;
                }

                // ten przypadek istnieje - juz sprawdzony
                var alreadyVisited = visited.ContainsKey(newBoard.ToString());
                if (alreadyVisited)
                {
                    continue;
                }

                var isInQueue = openList.UnorderedItems.Any(x => x.Element.Board.ToString() == newBoard.ToString());
                if (isInQueue)
                {
                    continue;
                }

                var newNode = new Node()
                {
                    Board = newBoard,
                    H = _heuristic.Calculate(newBoard),
                    G = current.G + 1,
                    Move = move,
                    Parent = current
                };

                openList.Enqueue(newNode, newNode.F);
            }
        }
    }
}