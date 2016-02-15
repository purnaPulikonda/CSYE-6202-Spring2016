using System;

namespace TrianglePatterns
{
	class Program
	{

        static int value= 10;
        static int i, j, k;
        static void Main(string[] args)
		{
            
            DisplayPatternA();
			DisplayPatternB();
			DisplayPatternC();
			DisplayPatternD();

			Console.ReadLine();
		}

		static void DisplayPatternA()
		{
            Console.WriteLine("Pattern A");
            for (i = 1; i <= value; i++)
            {
                for (k = 1; k <= i; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }

        }

        static void DisplayPatternB()
		{
            Console.WriteLine("Pattern B");
            for (i = value; i >= 0; i--)
            {
                for (k = 1; k <= i; k++)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }

        }

        static void DisplayPatternC()
		{
            Console.WriteLine("Pattern C");
            for (i = 1; i <= value; i++)
            {
                for (j = 1; j <= i - 1; j++)
                {
                    Console.Write(" ");
                }
                for (k = value - i; k >= 0; k--)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }

        }

        static void DisplayPatternD()
		{
            Console.WriteLine("Pattern D");
            for (i = value; i >= 1; i--)
            {
                for (j = 1; j <= i - 1; j++)
                {
                    Console.Write(" ");
                }
                for (k = value; k >= i; k--)
                {
                    Console.Write("*");
                }
                Console.WriteLine("");
            }


        }
    }
}
