namespace reversi;

class Board
{
    private static readonly Dictionary<int, string> mark = new Dictionary<int, string>()
    {
        {0, "." },
        {1, "O" },
        {-1, "X" }
    };

    private static readonly int LEFT = 1;
    private static readonly int UPPER_LEFT = 2;
    private static readonly int UPPER = 4;
    private static readonly int UPPER_RIGHT = 8;
    private static readonly int RIGHT = 16;
    private static readonly int LOWER_RIGHT = 32;
    private static readonly int LOWER = 64;
    private static readonly int LOWER_LEFT = 128;

    public int[] rawboard;
    public int[] MovableDir;
    public bool[] MovablePos;

    public int Tunrs;
    public int CurrentColor;

    public Board()
    {
        rawboard = new int[64];
        MovableDir = new int[64];
        MovablePos = new bool[64];

        rawboard[27] = -1;
        rawboard[36] = -1;
        rawboard[28] = 1;
        rawboard[35] = 1;

        Tunrs = 0;
        CurrentColor = 1;

        updateMovable();
    }
    private void updateMovable()
    {
        foreach (int i in Enumerable.Range(0, 64))
        {
            MovablePos[i] = false;
            int dir = checkMobility(i, CurrentColor);
            MovableDir[i] = dir;
            if (dir != 0)
            {
                MovablePos[i] = true;
            }

        }
    }

    private int checkMobility(int x ,int color)
    {
        int x_tmp;
        int dir = 0;

        //すでに石があればダメ
        if (rawboard[x] != 0) return dir;

        //右
        if(x % 8 < 6 && rawboard[x + 1] == -color)
        {
            x_tmp = x + 2;

            while(x_tmp % 8 < 7 && rawboard[x_tmp] == -color)
            {
                x_tmp += 1;
            }
            if(rawboard[x_tmp] == color) dir = dir | RIGHT;
        }

        //左
        if (x % 8 > 1 && rawboard[x - 1] == -color)
        {
            x_tmp = x - 2;

            while (x_tmp % 8 > 0 && rawboard[x_tmp] == -color)
            {
                x_tmp -= 1;
            }
            if (rawboard[x_tmp] == color) dir = dir | LEFT;
        }

        //上
        if (x / 8 > 1 && rawboard[x - 8] == -color)
        {
            x_tmp = x - 16;

            while (x_tmp / 8 > 0 && rawboard[x_tmp] == -color)
            {
                x_tmp -= 8;
            }
            if (rawboard[x_tmp] == color) dir = dir | UPPER;
        }

        //下
        if (x / 8 < 6 && rawboard[x + 8] == -color)
        {
            x_tmp = x + 16;

            while (x_tmp / 8 < 7 && rawboard[x_tmp] == -color)
            {
                x_tmp += 8;
            }
            if (rawboard[x_tmp] == color) dir = dir | LOWER;
        }

        //右上
        if (x / 8 > 1 && x % 8 < 6 && rawboard[x - 7] == -color)
        {
            x_tmp = x - 14;

            while (x_tmp / 8 >0 && x_tmp % 8 < 7 && rawboard[x_tmp] == -color)
            {
                x_tmp -= 7;
            }
            if (rawboard[x_tmp] == color) dir = dir | UPPER_RIGHT;
        }

        //右下
        if (x / 8 < 6  && x % 8 < 6 && rawboard[x + 9] == -color)
        {
            x_tmp = x + 18;

            while (x_tmp /8 < 7 && x_tmp % 8 < 7 && rawboard[x_tmp] == -color)
            {
                x_tmp += 9;
            }
            if (rawboard[x_tmp] == color) dir = dir | LOWER_RIGHT;
        }

        //左上
        if (x / 8 > 1 && x % 8 > 1 && rawboard[x - 9] == -color)
        {
            x_tmp = x - 18;

            while (x_tmp / 8 > 0 && x_tmp % 8 > 0 && rawboard[x_tmp] == -color)
            {
                x_tmp -= 9;
            }
            if (rawboard[x_tmp] == color) dir = dir | UPPER_LEFT;
        }

        //左下
        if (x / 8 < 6 && x % 8 > 1 && rawboard[x + 7] == -color)
        {
            x_tmp = x + 14;

            while (x_tmp / 8 < 7 && x_tmp % 8 > 0 && rawboard[x_tmp] == -color)
            {
                x_tmp += 7;
            }
            if (rawboard[x_tmp] == color) dir = dir | LOWER_LEFT;
        }
        return dir;     
    }

    private void flipDiscs(int x)
    {
        int x_tmp;
        int dir;

        //石を置く
        rawboard[x] = CurrentColor;

        dir = MovableDir[x];
        
        //右
        if ((dir & RIGHT) != 0)
        {
            x_tmp = x + 1;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp += 1;
            }
        }

        //左
        if ((dir & LEFT) != 0)
        {
            x_tmp = x - 1;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp -= 1;
            }
        }

        //上
        if ((dir & UPPER) != 0)
        {
            x_tmp = x - 8;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp -= 8;
            }
        }

        //下
        if ((dir & LOWER) != 0)
        {
            x_tmp = x + 8;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp += 8;
            }
        }

        //右上
        if ((dir & UPPER_RIGHT) != 0)
        {
            x_tmp = x - 7;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp -= 7;
            }
        }

        //右下
        if ((dir & LOWER_RIGHT) != 0)
        {
            x_tmp = x + 9;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp += 9;
            }
        }

        //左上
        if ((dir & UPPER_LEFT) != 0)
        {
            x_tmp = x - 9;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp -= 9;
            }
        }

        //左下
        if ((dir & LOWER_LEFT) != 0)
        {
            x_tmp = x + 7;
            while (rawboard[x_tmp] == -CurrentColor)
            {
                rawboard[x_tmp] = CurrentColor;
                x_tmp += 7;
            }
        }
    }
    public void move(int x)
    {
        if (x < 0 || x > 64) return;
        if (MovablePos[x] == false) return;

        flipDiscs(x);

        Tunrs += 1;

        CurrentColor *= -1;

        updateMovable();
    }

    public void display()
    {
        foreach (int i in Enumerable.Range(0, 8))
        {
            foreach (int j in Enumerable.Range(0, 8))
            {
                Console.Write($" {mark[rawboard[i * 8 + j]]}  ");

            }
            Console.WriteLine();
            Console.WriteLine();

        }

        foreach (int i in Enumerable.Range(0, 8))
        {
            foreach (int j in Enumerable.Range(0, 8))
            {
                Console.Write($"{MovableDir[i * 8 + j]:000} ");
            }
            Console.WriteLine();
        }
    }
}

