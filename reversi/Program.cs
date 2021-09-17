namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board();

        board.display();
        int[] moves = { 19, 18, 17, 11, 4, 43, 51, 20, 29 };

        foreach(int each_move in moves)
        {
            Console.ReadKey();
            Console.Clear();
            board.move(each_move);
            board.display();
        }
    }
}

