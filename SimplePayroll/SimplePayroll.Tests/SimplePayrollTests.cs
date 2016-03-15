using System;
using NUnit.Framework;

namespace SimplePayroll.Tests
{
    [TestFixture]
    public class SimplePayrollTests
    {
        [Test]
        public void When_validNameisEntered_ResultShouldBeTrue()
        {
            // prepare
            var expected = true;
            String name = "Purna";

            // action
            var actual = Program.userEnteredNameValid(name);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_InvalidnameEntered_ResultShouldBeFalse()
        {
            // prepare
            var expected = false;
            String name = "_*h7j";

            // action
            var actual = Program.userEnteredNameValid(name);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }
        [Test]
        public void When_validssnEntered_ResultShouldBeTrue()
        {
            // prepare
            var expected = true;
            String ssn = "123-56-7896";

            // action
            var actual = Program.userEnteredSsnValid(ssn);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_InvalidssnEntered_ResultShouldBeFalse()
        {
            // prepare
            var expected = false;
            String ssn = "123-56-7-96";

            // action
            var actual = Program.userEnteredSsnValid(ssn);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredValidEmployeeType_UpperCaseValidEmployeeTypeEntered_ResultShouldBeTrue()
        {
            // prepare
            var expected = true;

            // action
            var actual = Program.UserEnteredValidEmployeeType("H");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("C");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("SC");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("S");

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredValidEmployeeType_LowerCaseValidEmployeeTypeEntered_ResultShouldBeTrue()
        {
            // prepare
            var expected = true;

            // action
            var actual = Program.UserEnteredValidEmployeeType("h");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("c");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("sc");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("s");

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredValidEmployeeType_UpperandLowerCaseValidEmployeeTypeEntered_ResultShouldBeFalse()
        {
            // prepare
            var expected = false;

            // action
            var actual = Program.UserEnteredValidEmployeeType("k");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("J");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("A");


            // assert
            Assert.That(expected, Is.EqualTo(actual));

            // action
            actual = Program.UserEnteredValidEmployeeType("l");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            actual = Program.UserEnteredValidEmployeeType("d");

            // assert
            Assert.That(expected, Is.EqualTo(actual));

            actual = Program.UserEnteredValidEmployeeType("P");

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredSalaryisValid_ResultShouldBeTrue()
        {
            // prepare
            var expected = 200f;
            string salary = "200.00";


            // action
          var actual= Program.userInputFloatValid(salary);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredCommissionRateisValid_ResultShouldBeTrue()
        {
            // prepare
            var expected = 0.06f;
            string comissionRate = "0.06";


            // action
            var actual = Program.userInputFloatValid(comissionRate);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredGrossSalaryisInValid_ResultShouldBeFalse()
        {
            // prepare
            var expected = -1f;
            string grossSalary = "6h78";


            // action
            var actual = Program.userInputFloatValid(grossSalary);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredHourlyrateyisInValid_ResultShouldBeFalse()
        {
            // prepare
            var expected = -1f;
            string hourlyrate = "20h";


            // action
            var actual = Program.userInputFloatValid(hourlyrate);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredBaseSalaryisInValid_ResultShouldBeFalse()
        {
            // prepare
            var expected = -1f;
            string baseSalary = "20hkh";


            // action
            var actual = Program.userInputFloatValid(baseSalary);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void When_UserEnteredNumberOfHoursisValid_ResultShouldBeTrue()
        {
            // prepare
            var expected = 20;
            string numberOfHours = "20";


            // action
            var actual = Program.userInputFloatValid(numberOfHours);

            // assert
            Assert.That(expected, Is.EqualTo(actual));
        }

        [Test]
        public void CalculateMoneyEarned_SalariedEmployee_ResultShouldBeSalary()
        {
            // prepare
            float expected = 200;
            float salary = 200;
            

            // action
          Employee e =  Program.createSalariedEmployee("firstName","lastName","123-45-6789",salary);

            // assert
            Assert.That(expected, Is.EqualTo(e.MoneyEarned()));
        }

        [Test]
        public void CalculateMoneyEarned_ComissionedEmployee_ResultShouldBeComissionRateintogrossSales()
        {
            // prepare
            float expected = 50;
            float comissionRate = 0.05f;
            float grossSales = 1000;


            // action
            Employee e = Program.createComissionEmployee("firstName", "lastName", "123-45-6789", grossSales,comissionRate);

            // assert
            Assert.That(expected, Is.EqualTo(e.MoneyEarned()));
        }

        [Test]
        public void CalculateMoneyEarned_HourlyEmployee_ResultShouldBeHourlyRateintoNumberOfHours_numberOfHoursLessthanFourty()
        {
            // prepare
            float expected = 400;
            float hourlyRate = 20;
            float numberOfHours = 20;


            // action
            Employee e = Program.createHourlyEmployee("firstName", "lastName", "123-45-6789", hourlyRate, numberOfHours);

            // assert
            Assert.That(expected, Is.EqualTo(e.MoneyEarned()));
        }

        [Test]
        public void CalculateMoneyEarned_HourlyEmployee_ResultShouldBeHourlyRateintoNumberOfHours_numberOfHoursGreaterthanFourty()
        {
            // prepare
            float expected = 1100;
            float hourlyRate = 20;
            float numberOfHours = 50;


            // action
            Employee e = Program.createHourlyEmployee("firstName", "lastName", "123-45-6789", hourlyRate, numberOfHours);

            // assert
            Assert.That(expected, Is.EqualTo(e.MoneyEarned()));
        }
        [Test]
        public void CalculateMoneyEarned_SalaryBasedCommissionEmployee_ResultShouldBeComissionRateintogrossSalesplusbaseSalary()
        {
            // prepare
            float expected = 550;
            float comissionRate = 0.05f;
            float grossSales = 1000;
            float baseSalary = 500;


            // action
            Employee e = Program.createSalartBasedCommissionyEmployee("firstName", "lastName", "123-45-6789", comissionRate, grossSales,baseSalary);

            // assert
            Assert.That(expected, Is.EqualTo(e.MoneyEarned()));
        }
    }
}
