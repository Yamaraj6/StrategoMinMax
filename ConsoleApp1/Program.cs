using GameStratego;
using System;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            int boardSize = 50;
            StrategoBoard a = new StrategoBoard(boardSize);
            Evaluator evaluator = new Evaluator();
            int sum = 0;
            var watch = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < boardSize; i++)
            {
                for (int j = 0; j < boardSize; j++)
                {
                    var _move = new Field(i, j);

                    sum += evaluator.RateMove(_move, a);
                //    sum += evaluator.OptimizedRateMove(_move, a);
             //       Console.WriteLine(evaluator.RateMove(_move, a) +"\t" + evaluator.OptimizedRateMove(_move, a));
                    a.MakeMove(_move);
                }
            }
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine(sum +"\t"+elapsedMs);
           // Console.WriteLine(sumO);
        }
    }
}