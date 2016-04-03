using System;
namespace FizzBuzz
{
	class Program
	{
		static void Main(string[] args)
		{
            FizzBuzz fz = new FizzBuzz();
            Console.WriteLine("Enter the input number:");
           String output= fz.RunFizzBuzz(int.Parse(Console.ReadLine()));
            Console.WriteLine("Output:"+output);
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }
	}
}
