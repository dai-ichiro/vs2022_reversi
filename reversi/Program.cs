namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        UInt64 init = 1;
        UInt64 black = 0;
        UInt64 white = 0;

        black |= init << 28;
        black |= init << 35;

        white |= init << 27;
        white |= init << 36;

        Board board = new Board(black, white, 1);

        board.display();

        foreach(var x in board.possiblePos)
        {
            Console.Write($" {x.Item1} ");
        }
        Console.WriteLine();

        /*
        //search.start_search();

        
        foreach(var x in search.board.possiblePos)
        {
            Console.Write($" {x.Item1} ");
        }

        UInt64 current_move = 0;
        foreach(var x in search.board.possiblePos)
        {
            if(x.Item1 == 43) current_move = x.Item2;
        }

        Board new_board = search.board.move(43, current_move);

        new_board.display();

        foreach (var y in new_board.possiblePos)
        {
            Console.Write($" {y.Item1} ");
        }

        foreach (var x in search.board.possiblePos)
        {
            if (x.Item1 == 50) current_move = x.Item2;
        }

        new_board = new_board.move(50, current_move);
        new_board.display();
        */

    }
}

