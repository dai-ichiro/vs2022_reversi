namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        string result;
        UInt64 init = 1;
        UInt64 black = 0;
        UInt64 white = 0;

        black |= init << 28;
        black |= init << 35;

        white |= init << 27;
        white |= init << 36;

        Board board = new Board(black, white, 1);

        board.display();

        result = board.CurrentColor == 1 ? "黒" : "白";
        Console.WriteLine($"手番は {result} ");

        Console.WriteLine(String.Join(" ", board.possiblePos.Keys));

        
        while (true)
        {
            string input_text = Console.ReadLine();
            if(string.IsNullOrEmpty(input_text)) break;

            int output = int.TryParse(input_text, out int input) ? input : -1;
            
            if (board.possiblePos.ContainsKey(output))
            {
                board = board.move(output);
                Console.Clear();


                board.display();

                result = board.CurrentColor == 1 ? "黒" : "白";
                Console.WriteLine($"手番は {result} ");

                Console.WriteLine(String.Join(" ", board.possiblePos.Keys));
            }
        }

        






    }
}

