using System.Numerics;

namespace reversi;

class Search
{
    public List<int> current_solution;
    public Board board;
    bool is_finished = false;

    public Search()
    {
        UInt64 black = Convert.ToUInt64("0000000810000000", 16);
        UInt64 white = Convert.ToUInt64("0000001008000000", 16);

        board = new Board(black, white, 1);

        current_solution = new List<int> { 37 };

        board = board.move(37);
    }

    private bool is_solved(Board board)
    {
        if (board.white == 0 || board.black == 0) return true;
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

