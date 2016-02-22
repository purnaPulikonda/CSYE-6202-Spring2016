using System;

namespace GasPump
{
	public class Program
	{
		public enum GasType
		{
			None,
			RegularGas,
			MidgradeGas,
			PremiumGas,
			DieselFuel			
		}

		static void Main(string[] args)
		{
            double totalcost=0.0;
            String userInput;

           do  {
                Console.Write("Enter purchased gas type, Q/q to quit : ");
                 userInput = Console.ReadLine();
                if (!UserEnteredSentinelValue(userInput) && UserEnteredValidGasType(userInput))
                {
                    Console.Write("Enter purchased gas amount, Q/q to quit :");
                    String userInputTwo= Console.ReadLine();
                    if (!UserEnteredSentinelValue(userInputTwo) && UserEnteredValidAmount(userInputTwo)) {
                        int gasAmount;
                        Int32.TryParse(userInputTwo, out gasAmount);
                        Char gastype;
                        char.TryParse(userInput, out gastype);
                        CalculateTotalCost(GasTypeMapper(gastype), gasAmount, ref totalcost);
                        Console.WriteLine("Your total cost of purchase is:{0}", totalcost);
                    }
                }
        }  while (!UserEnteredSentinelValue(userInput)) ;
            Console.WriteLine("Application Terminated");
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey();
             
            
		}

        // use this method to check and see if sentinel value is entered
        public static bool UserEnteredSentinelValue(string userInput)
        {
            var result = false;
            Char userInputChar;


            if (char.TryParse(userInput,out userInputChar)) { 

            if (userInputChar.Equals('q') || userInputChar.Equals('Q')) {
                result = true;
            }
        }

			return result;
		}

		// use this method to parse and check the characters entered
		// this does not conform 
		public static bool UserEnteredValidGasType(string userInput)
		{
			var result = false;
            Char userInputChar;
            try
            {
                 userInputChar = char.Parse(userInput);
            }
            catch (Exception e) {
                return false;
            }

            if (userInputChar.Equals('r') || userInputChar.Equals('R') || userInputChar.Equals('m') ||
                userInputChar.Equals('M')|| userInputChar.Equals('p') || userInputChar.Equals('P')||
                userInputChar.Equals('d')|| userInputChar.Equals('D')) {
                result = true;
            }
			
			return result;
		}

		// use this method to parse and check the double type entered
		// please use Double.TryParse() method 
		public static bool UserEnteredValidAmount(string userInput)
		{
			var result = false;
            Double  number;
            result = double.TryParse(userInput, out number);
          
			return result;
		}

		// use this method to map a valid char entry to its enum representation
		// e.g. User enters 'R' or 'r' --> this should be mapped to GasType.RegularGas
		// this mapping "must" be used within CalculateTotalCost() method later on
		public static GasType GasTypeMapper(char c)
		{
			GasType gasType = GasType.None;

            switch (c) {
                case 'r':
                case 'R':
                    gasType = GasType.RegularGas;
                    break;

                case 'p':
                case 'P':
                    gasType = GasType.PremiumGas;
                    break;

                case 'm':
                case 'M':
                    gasType = GasType.MidgradeGas;
                    break;

                case 'd':
                case 'D':
                    gasType = GasType.DieselFuel;
                    break;

            }

			return gasType;
		}

		public static double GasPriceMapper(GasType gasType)
		{
			var result = 0.0;

            switch(gasType){

                case GasType.RegularGas:
                    result = 1.94;
                    break;

                case GasType.MidgradeGas:
                    result = 2.0;
                    break;

                case GasType.PremiumGas:
                    result = 2.24;
                    break;

                case GasType.DieselFuel:
                    result = 2.17;
                    break;


            }


			return result;
	}

		public static void CalculateTotalCost(GasType gasType, int gasAmount, ref double totalCost)
		{
            double priceOfGas = GasPriceMapper(gasType);

            Console.WriteLine("You brought {0} galoons of {1} at {2} ",gasAmount,gasType.ToString(),priceOfGas);
            totalCost = priceOfGas * gasAmount;
		}
	}
}
