namespace reversi;

class Board
{
    private readonly UInt64 init = 1;
    private readonly UInt64 right_left_watcher = Convert.ToUInt64("7E7E7E7E7E7E7E7E", 16);
    private readonly UInt64 upper_lower_watcher = Convert.ToUInt64("00FFFFFFFFFFFF00", 16);
    private readonly UInt64 around_watcher = Convert.ToUInt64("007E7E7E7E7E7E00", 16);

    public UInt64 black;
    public UInt64 white;

    public Dictionary<int, UInt64> possiblePos = new Dictionary<int, UInt64>();

    public int CurrentColor;

    public Board(UInt64 black_stone, UInt64 white_stone, int turn)
    {
        black = black_stone;
        white = white_stone;

        CurrentColor = turn;

        if (CurrentColor == 1)
        {
            update_possiblePos(black, white);
        }
        else
        {
            update_possiblePos(white, black);
        }
    }

    public Board move(int x)
    {   
        UInt64 rev = possiblePos[x];
        UInt64 new_black;
        UInt64 new_white;

        if (CurrentColor == 1)
        {
            new_black = black ^ ((init << x) | rev);
            new_white = white ^ rev;
        }
        else
        {
            new_black = black ^ rev;
            new_white = white ^ (init << x | rev);
        }

        return new Board(new_black, new_white, CurrentColor * -1);
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

    public void update_possiblePos(UInt64 turn, UInt64 not_turn)
    {
        UInt64 mask;
        UInt64 tmp;
        UInt64 rev;

        possiblePos.Clear();

        for (int i = 0; i < 64; i++)
        {
            //着手箇所は空白でなければダメ
            //空きマスが「0」で表されているUInt64と着手箇所との比較
            //空白でなければcontinue
            UInt64 check_position = init << i;
            if (((turn | not_turn) & (check_position)) != 0) continue;

            rev = 0;

            //右方向チェック
            tmp = 0;
            mask = check_position << 1;
            if ((mask & right_left_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask << 1;
                while((mask & right_left_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask << 1;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //左方向チェック
            tmp = 0;
            mask = check_position >> 1;
            if ((mask & right_left_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask >> 1;

                while ((mask & right_left_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask >> 1;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //上方向チェック
            tmp = 0;
            mask = check_position >> 8;
            if ((mask & upper_lower_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask >> 8;

                while ((mask & upper_lower_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask >> 8;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //下方向チェック
            tmp = 0;
            mask = check_position << 8;
            if ((mask & upper_lower_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask << 8;

                while ((mask & upper_lower_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask << 8;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //右上方向チェック
            tmp = 0;
            mask = check_position >> 7;
            if ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask >> 7;

                while ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask >> 7;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //右下方向チェック
            tmp = 0;
            mask = check_position << 9;
            if ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask << 9;

                while ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask << 9;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //左上方向チェック
            tmp = 0;
            mask = check_position >> 9;
            if ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask >> 9;

                while ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask >> 9;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }

            //左下方向チェック
            tmp = 0;
            mask = check_position << 7;
            if ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = mask << 7;

                while ((mask & around_watcher) != 0 && (mask & not_turn) != 0)
                {
                    tmp |= mask;
                    mask = mask << 7;
                }
                if ((mask & turn) != 0) rev |= tmp;
            }
            
            if (rev != 0) possiblePos.Add(i, rev);
        }
    }
}

