﻿namespace reversi;

class Board
{
    private static int LEFT = 1;
    private static int UPPER_LEFT = 2;
    private static int UPPER = 4;
    private static int UPPER_RIGHT = 8;
    private static int RIGHT = 16;
    private static int LOWER_RIGHT = 32;
    private static int LOWER = 64;
    private static int LOWER_LEFT = 128;

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
        if (x > 1 && rawboard[x - 1][y] == -color)
        {
            x_tmp = x - 2;
            y_tmp = y;

            while (x_tmp > 0 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp -= 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | UPPER;
            }
        }

        //下
        if (x < 6 && rawboard[x + 1][y] == -color)
        {
            x_tmp = x + 2;
            y_tmp = y;

            while (x_tmp < 7 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp += 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | LOWER;
            }
        }

        //右上
        if (x > 1 && y < 6 && rawboard[x - 1][y + 1] == -color)
        {
            x_tmp = x - 2;
            y_tmp = y + 2;

            while (x_tmp >0 && y_tmp < 7 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp -= 1;
                y_tmp += 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | UPPER_RIGHT;
            }
        }

        //右下
        if (x < 6  && y < 6 && rawboard[x + 1][y + 1] == -color)
        {
            x_tmp = x + 2;
            y_tmp = y + 2;

            while (x_tmp < 7 && y_tmp < 7 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp += 1;
                y_tmp += 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | LOWER_RIGHT;
            }
        }

        //左上
        if (x > 1 && y > 1 && rawboard[x - 1][y - 1] == -color)
        {
            x_tmp = x - 2;
            y_tmp = y - 2;

            while (x_tmp > 0 && y_tmp > 0 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp -= 1;
                y_tmp -= 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | UPPER_LEFT;
            }
        }

        //左下
        if (x < 6 && y > 1 && rawboard[x + 1][y - 1] == -color)
        {
            x_tmp = x + 2;
            y_tmp = y - 2;

            while (x_tmp < 7 && y_tmp > 0 && rawboard[x_tmp][y_tmp] == -color)
            {
                x_tmp += 1;
                y_tmp -= 1;
            }

            if (rawboard[x_tmp][y_tmp] == color)
            {
                dir = dir | LOWER_LEFT;
            }
        }
        return dir;     
    }

    private void flipDiscs(int x, int y)
    {
        int x_tmp, y_tmp;
        int dir;

        //石を置く
        rawboard[x][y] = CurrentColor;

        dir = MovableDir[x][y];
        
        //右
        if ((dir & RIGHT) != 0)
        {
            y_tmp = y + 1;
            while (rawboard[x][y_tmp] == -CurrentColor)
            {
                rawboard[x][y_tmp] = CurrentColor;
                y_tmp += 1;
            }
        }

        //左
        if ((dir & LEFT) != 0)
        {
            y_tmp = y - 1;
            while (rawboard[x][y_tmp] == -CurrentColor)
            {
                rawboard[x][y_tmp] = CurrentColor;
                y_tmp -= 1;
            }
        }

        //上
        if ((dir & UPPER) != 0)
        {
            x_tmp = x - 1;
            while (rawboard[x_tmp][y] == -CurrentColor)
            {
                rawboard[x_tmp][y] = CurrentColor;
                x_tmp -= 1;
            }
        }

        //下
        if ((dir & LOWER) != 0)
        {
            x_tmp = x + 1;
            while (rawboard[x_tmp][y] == -CurrentColor)
            {
                rawboard[x_tmp][y] = CurrentColor;
                x_tmp += 1;
            }
        }

        //右上
        if ((dir & UPPER_RIGHT) != 0)
        {
            x_tmp = x - 1;
            y_tmp = y + 1;
            while (rawboard[x_tmp][y_tmp] == -CurrentColor)
            {
                rawboard[x_tmp][y_tmp] = CurrentColor;
                x_tmp -= 1;
                y_tmp += 1;
            }
        }

        //右下
        if ((dir & LOWER_RIGHT) != 0)
        {
            x_tmp = x + 1;
            y_tmp = y + 1;
            while (rawboard[x_tmp][y_tmp] == -CurrentColor)
            {
                rawboard[x_tmp][y_tmp] = CurrentColor;
                x_tmp += 1;
                y_tmp += 1;
            }
        }

        //左上
        if ((dir & UPPER_LEFT) != 0)
        {
            x_tmp = x - 1;
            y_tmp = y - 1;
            while (rawboard[x_tmp][y_tmp] == -CurrentColor)
            {
                rawboard[x_tmp][y_tmp] = CurrentColor;
                x_tmp -= 1;
                y_tmp -= 1;
            }
        }

        //左下
        if ((dir & LOWER_LEFT) != 0)
        {
            x_tmp = x + 1;
            y_tmp = y - 1;
            while (rawboard[x_tmp][y_tmp] == -CurrentColor)
            {
                rawboard[x_tmp][y_tmp] = CurrentColor;
                x_tmp += 1;
                y_tmp -= 1;
            }
        }
    }
    public void move(int x, int y)
    {
        if (x < 0 || x > 7) return;
        if (y < 0 || y > 7) return;
        if (MovablePos[x][y] == false) return;

        flipDiscs(x, y);

        Tunrs += 1;

        CurrentColor *= -1;

        updateMovable();
    }
}

