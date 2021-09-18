namespace reversi;

class Board
{
    private readonly UInt64 init = 1;
    private readonly UInt64 horizontal_watcher = Convert.ToUInt64("7E7E7E7E7E7E7E7E", 16);
    private readonly UInt64 vertical_watcher = Convert.ToUInt64("00FFFFFFFFFFFF00", 16);
    private readonly UInt64 around_watcher = Convert.ToUInt64("007E7E7E7E7E7E00", 16);

    public UInt64 black;
    public UInt64 white;

    public Dictionary<int, UInt64> possiblePos = new Dictionary<int, UInt64>();

    public int Turns;
    public int CurrentColor;

    public Board()
    {
        black = 0;
        white = 0;

        black |= init << 28;
        black |= init << 35;

        white |= init << 27;
        white |= init << 36;

        Turns = 0;
        CurrentColor = 1;

        update_possiblePos(black, white);
    }
    public void move(int x)
    {
        if (!(possiblePos.ContainsKey(x))) return;

        if(CurrentColor == 1)
        {
            black |= (init << x | possiblePos[x]);
            white ^= possiblePos[x];
            update_possiblePos(white, black);
        }
        else
        {
            white |= (init << x | possiblePos[x]);
            black ^= possiblePos[x];
            update_possiblePos(black, white);
        }

        Turns += 1;
        CurrentColor *= -1;

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

    private UInt64 right_direction_transfer(UInt64 m)
    {
        return (m << 1) & horizontal_watcher;
    }

    private UInt64 left_direction_transfer(UInt64 m)
    {
        return (m >> 1) & horizontal_watcher;
    }

    private UInt64 upper_direction_transfer(UInt64 m)
    {
        return (m >> 8) & vertical_watcher;
    }

    private UInt64 lower_direction_transfer(UInt64 m)
    {
        return (m << 8) & vertical_watcher;
    }

    private UInt64 upperright_direction_transfer(UInt64 m)
    {
        return (m >> 7) & around_watcher;
    }

    private UInt64 lowerright_direction_transfer(UInt64 m)
    {
        return (m << 9) & around_watcher;
    }

    private UInt64 upperleft_direction_transfer(UInt64 m)
    {
        return (m >> 9) & around_watcher;
    }

    private UInt64 lowerleft_direction_transfer(UInt64 m)
    {
        return (m << 7) & around_watcher;
    }

    public Dictionary<int, UInt64> update_possiblePos(UInt64 turn, UInt64 not_turn)
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
            if (((black | white) & (check_position)) != 0) continue;

            rev = 0;

            //右方向チェック
            tmp = 0;
            mask = right_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = right_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;
            

            //左方向チェック
            tmp = 0;
            mask = left_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = left_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //上方向チェック
            tmp = 0;
            mask = upper_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = upper_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //下方向チェック
            tmp = 0;
            mask = lower_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = lower_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //右上方向チェック
            tmp = 0;
            mask = upperright_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = upperright_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //右下方向チェック
            tmp = 0;
            mask = lowerright_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = lowerright_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //左上方向チェック
            tmp = 0;
            mask = upperleft_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = upperleft_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;

            //左下方向チェック
            tmp = 0;
            mask = lowerleft_direction_transfer(check_position);
            while (mask != 0 && (mask & not_turn) != 0)
            {
                tmp |= mask;
                mask = lowerleft_direction_transfer(mask);
            }
            if ((mask & turn) != 0) rev |= tmp;


            if (rev != 0) possiblePos.Add(i, rev);
        }
        return possiblePos;
    }
}

