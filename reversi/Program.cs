namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Board board = new Board();

        board.display();

        Console.WriteLine(board.possiblePos.Count.ToString());
        foreach(var x in board.possiblePos)
        {
            Console.WriteLine(x.Item1);
        }
    }
}

