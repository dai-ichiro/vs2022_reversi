using System.Numerics;

namespace reversi;

class Search
{
    public List<int> current_solution;
    public Board board;
    bool is_finished = false;

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

    public void depth_limited_search(Board board, int depth)
    {
        if (depth == 0 && is_solved(board)) 
        {
            Console.WriteLine(String.Join(" ", current_solution));
            is_finished = true;
            return;
        }

        if (depth == 0) return;
        
        foreach(var x in board.possiblePos.Keys)
        {
            current_solution.Add(x);
            depth_limited_search(board.move(x), depth - 1);
            current_solution.RemoveAt(current_solution.Count - 1);
        }
        return;
    }

    public void start_search()
    {
        for (int depth = 1; depth < 20; depth++)
        {
            Console.WriteLine($"Start searching length {depth + 1}");
            depth_limited_search(board, depth);
            if (is_finished == true) break;
        }
    }
}

