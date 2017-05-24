using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using chesslib.Field;
using chesslib.Field.Bit;

namespace tester
{
    class Program
    {
        private static int TestsNumber = 10000000;

        static void Main(string[] args)
        {
            EnumBoard board = new EnumBoard(8);
            
            Stopwatch s = new Stopwatch();
            //s.Start();
            //for (int i = 0; i < TestsNumber; i++)
            //{
            //    var t = board.GetCopy();
            //}
            //s.Stop();
            //Console.WriteLine("Serialization: "+ s.Elapsed);

            //s.Restart();
            //for (int i = 0; i < TestsNumber; i++)
            //{
            //    var t = board.GetCopyTest();
            //}
            //s.Stop();
            //Console.WriteLine("Constructor: "+ s.Elapsed);

            Console.ReadKey();

        }
    }
}
