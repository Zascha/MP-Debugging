using System;

namespace MP.Debugging.CodeGen
{
    class Program
    {
        static void Main(string[] args)
        {
            var crackCode = CodeGererator.GetCrackKey();
            Console.WriteLine($"Your crack key is: {crackCode}");

            Console.ReadKey();
        }
    }
}
