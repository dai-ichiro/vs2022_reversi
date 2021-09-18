namespace reversi;

class Board
{
    private readonly UInt64 init = 1;
    private readonly UInt64 horizontal_watcher = Convert.ToUInt64("7E7E7E7E7E7E7E7E", 16);

    public UInt64 black;
    public UInt64 white;

    public List<(int, UInt64)> possiblePos;

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

        update_possiblePos(black, white);
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

    public bool checkPos(int x)
    {
        return true;
    }

    private UInt64 horizontal_transfer(UInt64 m)
    {
        return (m >> 1) & horizontal_watcher;
    }
    public List<(int, UInt64)> update_possiblePos(UInt64 turn, UInt64 not_turn)
    {
        for (int i = 0; i < 64; i++)
        {
            possiblePos.Clear();

            //着手箇所は空白でなければダメ
            //空きマスが「0」で表されているUInt64と着手箇所との比較
            //空白でなければ空のリストを返す
            UInt64 check_position = init << i;
            if (((black | white) & (check_position)) != 0) return possiblePos;

            bool is_possible = false;
            UInt64 rev = 0;
            UInt64 tmp;

            //右方向チェック
            tmp = 0;
            UInt64 mask = horizontal_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = horizontal_transfer(mask);
            }
            if ((mask & turn) != 0) 
            {    
                is_possible = true;
                rev |= mask;
            }
        }
        return possiblePos;
    }
}

