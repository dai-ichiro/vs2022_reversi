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

        board.move(37);

        board.display();

        foreach (var x in board.possiblePos.Keys)
        {
            Console.WriteLine(x.ToString());
        }

    }
}

