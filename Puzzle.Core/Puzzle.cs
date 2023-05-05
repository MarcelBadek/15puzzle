using System.Runtime.InteropServices.JavaScript;
using System.Threading.Channels;
using Puzzle.Core.Heuristics;
using Puzzle.Core.Heuristics;


namespace Puzzle.Core;

public class Puzzle
{
    public Board Board { get; set; }

    public Puzzle(string fileName)
    {
        Board = new Board(fileName);
    }

    public void AStar(IHeuristic heuristic)
    {
        var startNode = new Node(heuristic, Board);
        var openList = new PriorityQueue<Node, int>();
        var visited = new List<Node>();
        const string order = "LURD";
        var iter = 0;

        if (mainNode.Board.CheckBoard())
        {
            return;
        }
        
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
                var solvedBoard = current.Board;
                while (current.Parent is not null)
                {
                    path += current.Move;
                    current = current.Parent;
                }

                path = string.Concat(path.ToCharArray().Reverse());

                Console.WriteLine(path);
                return;
            }

            foreach (var move in order)
            {
                // ten przypadek istnieje - uno ruch
                if (current.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var board = new Board(current.Board);

                // ten przypadek istnieje - brak ruchu
                if (!board.Move(move))
                {
                    continue;
                }

                // ten przypadek istnieje - juz sprawdzony
                var alreadyVisited = visited.Any(x => Helper.CompareBoards(x.Board, board));
                if (alreadyVisited)
                {
                    continue;
                }

                var newNode = new Node(board, move, current)
                {
                    H = heuristic.Calculate(board),
                    G = current.G + 1
                };
                openList.Enqueue(newNode, newNode.F);
            }

        }
    }

    public void BreadthFirstSearch(string order)
    {
        var mainNode = new Node(Board);
        var openList = new Queue<Node>();
        var visited = new List<Node>();
        var iter = 0;

        if (mainNode.Board.CheckBoard())
        {
            visited.Add(mainNode);
            return;
        }

        openList.Enqueue(mainNode);

        while (true)
        {
            var currNode = openList.Dequeue();
            visited.Add(currNode);

            Console.WriteLine(++iter);
            // currNode.Board.DisplayBoard();
            // Console.WriteLine();
            // Console.ReadKey();

            if (currNode.Board.CheckBoard())
            {
                return;
            }


            foreach (var move in order)
            {
                if (currNode.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var board = new Board(currNode.Board);

                if (!board.Move(move))
                {
                    continue;
                }

                // var alreadyVisited = visited.Any(x => Helper.CompareBoards(x.Board, board));
                // if (alreadyVisited)
                // {
                //     continue;
                // }
                //
                // // ten przypadek istnieje - czeka na sprawdzenie
                // var presentInOpenList = openList.Any(x => Helper.CompareBoards(x.Board, board));
                // if (presentInOpenList)
                // {
                //     continue;
                // }

                var newNode = new Node(board, move, currNode);
                openList.Enqueue(newNode);
            }
        }
    }

    public void DepthFirstSearch(string order, int maxDepth)
    {
        var iter = 0;
        var stack = new Stack<Node>();
        //var visited = new List<Node>();
        var currNode = new Node(Board)
        {
            G = 0
        };

        if (currNode.Board.CheckBoard())
        {
            currNode.Board.DisplayBoard();
            return;
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
                
                return;
            }

            if (currNode.G == maxDepth)
            {
                continue;
            }

            foreach (var move in order.Reverse())
            { 
                var board = new Board(currNode.Board);

                if (!board.Move(move))
                {
                    continue;
                }

                var newNode = new Node(board, move, currNode)
                {
                    G = currNode.G + 1
                };
                stack.Push(newNode);
            }
        }
    }

    public bool DepthFirstSearch(string order, int maxDepth, int currentDepth = 0, Node? currNode = null)
    {
        Node currentNode;
        if (currentDepth == 0)
        {
            currentNode = new Node(Board);
        }
        else
        {
            if (currNode is null)
            {
                throw new NullReferenceException();
            }
            else
            {
                currentNode = currNode;
            }
        }

        if (currentNode.Board.CheckBoard())
        {
            Console.WriteLine("FOUND!!!!!!!!!!!");
            currentNode.Board.DisplayBoard();
            return true;
        }

        openList.Enqueue(mainNode);

        if (currentDepth == maxDepth)
        {
            return false;
        }


        foreach (var move in order)
        {
            var board = new Board(currentNode.Board);
            if (!board.Move(move))
            {
                continue;
            }

            var newNode = new Node(board, move, currentNode);

            if (DepthFirstSearchRec(order, maxDepth, currentDepth + 1, newNode))
            {
                return true;
            }
        }

        return false;
    }

}