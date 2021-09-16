namespace reversi;

class Program
{
    static Dictionary<int, string> mark = new Dictionary<int, string>()
    {
        {0, "." },
        {1, "O" },
        {-1, "X" }
    };

    static void Main(string[] args)
    {
        Board board = new Board();

        foreach (int i in Enumerable.Range(0, 8))
        {
            foreach(int j in Enumerable.Range(0, 8))
            {
                Console.Write($" {mark[board.rawboard[i][j]]}  ");
                
            }
            Console.WriteLine();
            Console.WriteLine();

        }

        foreach (int i in Enumerable.Range(0, 8))
        {
            foreach (int j in Enumerable.Range(0, 8))
            {
                Console.Write($"{board.MovableDir[i][j]:000} ");
            }
            Console.WriteLine();

        }
    }
}

