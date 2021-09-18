namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board();

        board.display();

        foreach(var x in board.possiblePos.Keys)
        {
            Console.WriteLine(x.ToString());
        }

        Console.ReadKey();

        Console.Clear();

        board.move(19);
        board.move(18);
        board.move(17);
        board.move(11);
        board.move(4);
        board.move(43);
        board.move(51);
        board.move(20);
        board.move(29);

        board.display();

        foreach (var x in board.possiblePos.Keys)
        {
            Console.WriteLine(x.ToString());
        }

    }
}

