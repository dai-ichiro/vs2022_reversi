namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board();

        board.display();
        int[] moves = { 26, 18 };

        foreach(int each_move in moves)
        {
            Console.ReadKey();
            Console.SetCursorPosition(0, 0);
            board.move(each_move);
            board.display();
        }

    }
}

