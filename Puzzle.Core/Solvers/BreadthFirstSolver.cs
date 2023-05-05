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
        var mainNode = new Node(board);
        var openList = new Queue<Node>();
        var visited = new List<Node>();

        if (mainNode.Board.CheckBoard())
        {
            visited.Add(mainNode);
            return new SolveResult();
        }

        openList.Enqueue(mainNode);

        while (true)
        {
            var currNode = openList.Dequeue();
            visited.Add(currNode);

            if (currNode.Board.CheckBoard())
            {
                return new SolveResult();
            }

            foreach (var move in _order)
            {
                if (currNode.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var newBoard = new Board(currNode.Board);

                if (!newBoard.Move(move))
                {
                    continue;
                }

                var newNode = new Node(newBoard, move, currNode);
                openList.Enqueue(newNode);
            }
        }
    }
}