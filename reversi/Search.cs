using System.Numerics;

namespace reversi;

class Search
{
    public List<int> current_solution;
    public Board board;

    public Search()
    {
        UInt64 init = 1;
        UInt64 black = 0;
        UInt64 white = 0;

        black |= init << 28;
        black |= init << 35;

        white |= init << 27;
        white |= init << 36;

        board = new Board(black, white, 1);

        current_solution = new List<int> { 37 };

        board = board.move(37);
    }

    private bool is_solved(Board board)
    {
        if (BitOperations.PopCount(board.white) == 0 || BitOperations.PopCount(board.black) == 0) return true;
        return false;
    }

    
    public bool depth_limited_search(Board board, int depth)
    {
        if (depth == 0) return is_solved(board) ? true : false;

        foreach(var x in board.possiblePos)
        {
            current_solution.Add(x.Item1);
            if(depth_limited_search(board.move(x.Item1, x.Item2), depth -1)) return true;
            current_solution.RemoveAt(current_solution.Count - 1);
        }
        */
        return false;
    }

    public void start_search()
    {
        for (int depth = 1; depth < 20; depth++)
        {
            Console.WriteLine($"Start searching length {depth + 1}");
            if (depth_limited_search(board, depth)) break;
        }
        Console.WriteLine(string.Join(" ", current_solution));
    }
}

