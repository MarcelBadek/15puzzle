using System.Runtime.InteropServices.JavaScript;

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
        var mainNode = new Node(heuristic, Board);
        var openList = new List<Node>();
        var closedList = new List<Node>();
        const string order = "LURD";
        var iter = 0;

        openList.Add(mainNode);

        while (true)
        {
            var minVal = openList.OrderBy(x => x.F).First();
            
            openList.Remove(minVal);
            closedList.Add(minVal);
            
            Console.WriteLine(++iter);
            // minVal.Board.DisplayBoard();
            // Console.WriteLine();
            // Console.ReadKey();
            
            if (minVal.Board.CheckBoard())
            {
                return;
            }

            foreach (var move in order)
            {
                // ten przypadek istnieje - uno ruch
                // if (minVal.Move == Helper.GetReverseMove(move))
                // {
                //     continue;
                // }
                
                var existingBoard = false;
                var board = new Board(minVal.Board);

                // ten przypadek istnieje - brak ruchu
                if (!board.Move(move))
                {
                    continue;
                }

                // ten przypadek istnieje - juz sprawdzony
                foreach (var node in closedList)
                {
                    if (Helper.CompareBoards(board, node.Board))
                    {
                        existingBoard = true;
                        break;
                    }
                }
                
                // ten przypadek istnieje - czeka na sprawdzenie
                foreach (var node in openList)
                {
                    if (Helper.CompareBoards(board, node.Board))
                    {
                        existingBoard = true;
                        break;
                    }
                }

                if (existingBoard)
                {
                    continue;
                }

                var newNode = new Node(heuristic, board, move, minVal);
                openList.Add(newNode);
            }
        }
        
    }


}