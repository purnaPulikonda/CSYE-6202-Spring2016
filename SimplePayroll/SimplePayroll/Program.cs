using System;
using System.Text.RegularExpressions;

namespace SimplePayroll
{
    public class Program
    {
        public enum EmployeeType
        {
            None,
            SalariedEmployee,
            ComissionEmployee,
            HourlyEmployee,
            SalaryBasesCommissionEmployee

        }
        static void Main(string[] args)
        {
            String firstName;
            String lastName;
            String ssn;

            Console.WriteLine("Please enter the first name of employee:");
            String userInputFirstName = Console.ReadLine();
            if (!userEnteredNameValid(userInputFirstName))
            {
                Console.WriteLine("invalid first Name");

            }
            else {

                firstName = userInputFirstName;
                Console.WriteLine("Please enter the last name of employee:");
                String userInputLastName = Console.ReadLine();
                if (!userEnteredNameValid(userInputLastName))
                {
                    Console.WriteLine("invalid last Name");
                }
                else {
                    lastName = userInputLastName;

                    Console.WriteLine("Please enter ssn of employee in format xxx-xx-xxxx :");
                    String userInputSsn = Console.ReadLine();

                    if (!userEnteredSsnValid(userInputSsn))
                    {
                        Console.WriteLine("invalid ssn Name");

                    }
                    else {

                        ssn = userInputSsn;
                        Console.WriteLine("Please enter the type of employee you want to create :");
                        Console.WriteLine("Please type s for salaried Employee, h for hourly Employee, c for Comission Employee , sc for SalaryBasedComissioned Employee");
                        String userSelectedEmployee = Console.ReadLine();

                        if (UserEnteredValidEmployeeType(userSelectedEmployee))
                        {

                            EmployeeType employeeType = EmployeeType.None;

                            switch (userSelectedEmployee)
                            {
                                case "s":
                                case "S":
                                    employeeType = EmployeeType.SalariedEmployee;
                                    Console.WriteLine("Enter Salary of Employee :");
                                    String userInputSalary = Console.ReadLine();

                                    float Salary = userInputFloatValid(userInputSalary);
                                    if (Salary > 0)
                                        createSalariedEmployee(firstName, lastName, ssn, Salary);
                                    else {
                                        Console.WriteLine("Invalid Salary");


                                    }

                                    break;

                                case "c":
                                case "C":
                                    employeeType = EmployeeType.ComissionEmployee;
                                    Console.WriteLine("Enter Comission of Employee:");
                                    String userInputComission = Console.ReadLine();
                                    float comission = userInputFloatValid(userInputComission);
                                    if (comission <= 0)
                                    {
                                        Console.WriteLine("Invalid Commission");

                                    }
                                    else {

                                        Console.WriteLine("Enter Gross Sales: ");
                                        String userInputGrossSales = Console.ReadLine();
                                        float grossSales = userInputFloatValid(userInputGrossSales);
                                        if (grossSales > 0)
                                        {

                                            createComissionEmployee(firstName, lastName, ssn, comission, grossSales);
                                        }
                                        else {
                                            Console.WriteLine("Invalid Gross Sales");

                                        }
                                    }
                                    break;


                                case "h":
                                case "H":
                                    employeeType = EmployeeType.HourlyEmployee;
                                    Console.WriteLine("Enter hourly rate of Employee: ");
                                    String userInputHourlyRate = Console.ReadLine();
                                    float hourlyRate = userInputFloatValid(userInputHourlyRate);
                                    if (hourlyRate <= 0)
                                    {
                                        Console.WriteLine("Invalid Hourly Rate");

                                    }
                                    else {

                                        Console.WriteLine("Enter Number of Hours Worked: ");
                                        String userInputHoursWorked = Console.ReadLine();
                                        float hoursWorked = userInputFloatValid(userInputHoursWorked);
                                        if (hoursWorked > 0)
                                        {

                                            createHourlyEmployee(firstName, lastName, ssn, hourlyRate, hoursWorked);
                                        }
                                        else {
                                            Console.WriteLine("Invalid Number of Hours worked entered");

                                        }
                                    }
                                    break;

                                case "sc":
                                case "SC":
                                    employeeType = EmployeeType.SalaryBasesCommissionEmployee;
                                    Console.WriteLine("Enter Comission of Employee: ");
                                    String userInputComissionRate = Console.ReadLine();
                                    float comissionrate = userInputFloatValid(userInputComissionRate);
                                    if (comissionrate <= 0)
                                    {
                                        Console.WriteLine("Invalid Commission");

                                    }
                                    else {

                                        Console.WriteLine("Enter Gross Sales: ");
                                        String userInputGrossSales = Console.ReadLine();
                                        float grossSalesOne = userInputFloatValid(userInputGrossSales);
                                        if (grossSalesOne <= 0)
                                        {
                                            Console.WriteLine("Invalid Gross Sales");


                                        }
                                        else {
                                            Console.WriteLine("Enter Salary of Employee: ");
                                            String userInputBaseSalary = Console.ReadLine();

                                            float BaseSalary = userInputFloatValid(userInputBaseSalary);
                                            if (BaseSalary > 0)
                                                createSalartBasedCommissionyEmployee(firstName, lastName, ssn, comissionrate, grossSalesOne, BaseSalary);
                                            else {
                                                Console.WriteLine("Invalid Salary");

                                            }

                                        }

                                    }
                                    break;



                            }
                        }
                        else {
                            Console.WriteLine("Invalid user type entered");
                        }

                    }
                }
            }
            userEndProgram();
        }

        private static void askUserToQuit()
        {
            Console.WriteLine("Enter q/Q to quit");
            string userSentinal = Console.ReadLine();
            if (userInputSentinal(userSentinal))
            {
                userEndProgram();
            }
        }

        private static void userEndProgram()
        {
            Console.WriteLine("\nApplication Terminated");
            Console.WriteLine("Press any key to continue.....");
            Console.ReadKey();
        }

        private static bool userInputSentinal(string userSentinal)
        {
            var result = false;
            Char userInputChar;


            if (char.TryParse(userSentinal, out userInputChar))
            {

                if (userInputChar.Equals('q') || userInputChar.Equals('Q'))
                {
                    result = true;
                }
            }
            return result;
        }

        public static Employee createComissionEmployee(string firstName, string lastName, string ssn, float comission, float grossSalary)
        {
            Employee comissionEmployee = new ComissionEmployee(firstName, lastName, ssn, comission, grossSalary);
            Console.WriteLine(comissionEmployee);
            return comissionEmployee;
        }

        public static Employee createHourlyEmployee(string firstName, string lastName, string ssn, float hourlyrate, float hoursWorked)
        {
            Employee hourlyEmployee = new HourlyEmployee(firstName, lastName, ssn, hourlyrate, hoursWorked);
            Console.WriteLine(hourlyEmployee);
            return hourlyEmployee;
        }
        public static Employee createSalartBasedCommissionyEmployee(string firstName, string lastName, string ssn, float commission, float grossSales, float salary)
        {
            Employee salaryCommisionEmployee = new SalaryBasedCommissionEmployee(firstName, lastName, ssn, commission, grossSales, salary);
            ; Console.WriteLine(salaryCommisionEmployee);
            return salaryCommisionEmployee;
        }

        public static Employee createSalariedEmployee(string firstName, string lastName, string ssn, float salary)
        {
            Employee salariedEmployee = new SalariedEmployee(firstName, lastName, ssn, salary);
            Console.WriteLine(salariedEmployee);
            return salariedEmployee;
        }

        public static float userInputFloatValid(string userInputFloatValue)
        {
            float sales;
            if (float.TryParse(userInputFloatValue, out sales))
            {

                return sales;
            }
            else
                return -1;


        }

        public static bool userEnteredNameValid(string userInputName)
        {
            if (String.IsNullOrEmpty(userInputName))
                return false;

            string pattern = @"^[a-zA-Z]+$";
            return Regex.IsMatch(userInputName, pattern);
        }


        public static bool userEnteredSsnValid(string ssn)
        {
            if (String.IsNullOrEmpty(ssn))
                return false;

            string pattern = @"^\d{3}-\d{2}-\d{4}$";
            return Regex.IsMatch(ssn, pattern);
        }


        public static bool UserEnteredValidEmployeeType(string userInput)
        {
            var result = false;


            if (userInput.Equals("s") || userInput.Equals("S") || userInput.Equals("c") ||
               userInput.Equals("C") || userInput.Equals("h") || userInput.Equals("H") ||
               userInput.Equals("SC") || userInput.Equals("sc"))
            {
                result = true;
            }

            return result;
        }
    }

}
