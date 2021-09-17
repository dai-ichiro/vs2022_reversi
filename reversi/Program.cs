namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board();

        board.move(26);
        board.move(18);
        board.move(19);
        board.move(34);
        board.move(41);

        board.display();
    }
}

