namespace reversi;

class Search
{
    List<int> current_solution;
    public Board board;

    public Search()
    {
        board = new Board();
        current_solution = new List<int>{ 37 };
        board.move(37);
    }

    private bool  is_solved(Board current_board)
    {
        int black_count = current_board.rawboard.Count(s => s == 1);
        int white_count = current_board.rawboard.Count(s => s == -1);
        if(black_count == 0 ||  white_count == 0) return true;
        return false;
    }

    public void start_search(int depth)
    {

    }
}