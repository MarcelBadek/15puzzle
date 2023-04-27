﻿using System.Runtime.InteropServices.JavaScript;
using System.Threading.Channels;
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
        var mainNode = new Node(heuristic, Board);
        var openList = new PriorityQueue<Node, int>();
        var visited = new List<Node>();
        const string order = "LURD";
        var iter = 0;

        if (mainNode.Board.CheckBoard())
        {
            return;
        }

        openList.Enqueue(mainNode, mainNode.F);

        while (true)
        {
            var minVal = openList.Dequeue();
            visited.Add(minVal);

            Console.WriteLine(++iter);
            // Console.WriteLine(minVal.H);
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
                if (minVal.Move == Helper.GetReverseMove(move))
                {
                    continue;
                }

                var board = new Board(minVal.Board);

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

                // ten przypadek istnieje - czeka na sprawdzenie
                var presentInOpenList = openList.UnorderedItems.Any(x => Helper.CompareBoards(x.Element.Board, board));
                if (presentInOpenList)
                {
                    continue;
                }

                var newNode = new Node(heuristic, board, move, minVal);
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

        Console.WriteLine(currentDepth);
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

            if (DepthFirstSearch(order, maxDepth, currentDepth + 1, newNode))
            {
                return true;
            }
        }

        return false;
    }
}