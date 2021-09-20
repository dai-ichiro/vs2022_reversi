using System.Diagnostics;

namespace reversi;

class Program
{
    static void Main(string[] args)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();

        Search search = new Search();
        search.start_search();

        sw.Stop();

        TimeSpan ts = new TimeSpan();
        ts = sw.Elapsed;
        Console.WriteLine("Finished!({0})", ts);
    }
}

