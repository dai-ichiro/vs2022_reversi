namespace reversi;

class Search
{
    List<int> current_solution;
    public Board board;

    public Search()
    {
        board = new Board();
        current_solution = new List<int>{ 37 };
    }

    private bool  is_solved(Board current_board)
    {
        
        return false;
    }

    public void start_search(int depth)
    {

    }
}