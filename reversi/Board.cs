namespace reversi;

class Board
{
    public UInt64 black;
    public UInt64 white;

    private readonly UInt64 init = 1;

    public int Tunrs;
    public int CurrentColor;

    public Board()
    {
        black = 0;
        white = 0;

        black |= init << 28;
        black |= init << 35;

        white |= init << 27;
        white |= init << 36;

        Tunrs = 0;
        CurrentColor = 1;
    }

    public void display()
    {
        foreach (int i in Enumerable.Range(0, 8))
        {
            foreach (int j in Enumerable.Range(0, 8))
            {
                if ((black & init << (8 * i + j)) != 0)
                {
                    Console.Write(" O  ");
                }
                else
                {
                    Console.Write(((white & init << (8 * i + j)) != 0) ? " x  " : " .  ");
                }
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}

