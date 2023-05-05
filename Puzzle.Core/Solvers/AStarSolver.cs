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
        var startNode = new Node()
        {
            Board = board,
            G = 0,
            H = _heuristic.Calculate(board)
        };
        var openList = new PriorityQueue<Node, int>();
        var visited = new List<Node>();
        const string order = "LURD";
        var iter = 0;
        var path = string.Empty;

        openList.Enqueue(startNode, startNode.F);

        while (true)
        {
            var current = openList.Dequeue();
            visited.Add(current);

            Console.WriteLine(++iter);
            // Console.WriteLine(minVal.H);
            // minVal.Board.DisplayBoard();
            // Console.WriteLine();
            // Console.ReadKey();
            Console.WriteLine("F:" + current.F);

            if (current.H == 0)
            {
                while (current.Parent is not null)
                {
                    path += current.Move;
                    current = current.Parent;
                }

                path = string.Concat(path.ToCharArray().Reverse());

                return new SolveResult
                {
                    Solution = path
                };
            }

            foreach (var move in order)
            {
                // ten przypadek istnieje - uno ruch
                if (current.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var newBoard = new Board(current.Board);

                // ten przypadek istnieje - brak ruchu
                if (!newBoard.Move(move))
                {
                    continue;
                }

                // ten przypadek istnieje - juz sprawdzony
                var alreadyVisited = visited.Any(x => Helper.CompareBoards(x.Board, newBoard));
                if (alreadyVisited)
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