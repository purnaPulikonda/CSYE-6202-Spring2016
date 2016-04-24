using AirlineRegistration.Dao;
using AirlineReservationSystem;
using NUnit.Framework;

namespace AirlineReservation.Tests
{

    [TestFixture]
    public class AirlineReservationTests
    {

        User user;
        Flight flight;
        FlightInformation flightinformation;
       [TestFixtureSetUp]
        public void Setup()
        {
            // create the student here..
            user = new User();
            user.id = 10;
            user.name = "katy";
            user.address = "324 Park Street,Boston";
            user.phoneNumber= "(123)-567-9873";
            user.emailId = "email@gmail.com";

            flight = new Flight();
            flight.flightId = "6876";
            flight.flightName = "KA123";
            flight.thisDate = "02-04-2016";
            flight.timeSpan = "8:00";
            flight.economyPrice = 1000f;
            flight.ecoPlusPrice = 1600f;
            flight.businessPrice = 3000f;
            flight.carrierId = "123";
            flight.flightCrewId = 1;


            FlightInformation flightinformation = new FlightInformation();
            flightinformation.user = user;
            flightinformation.flight = flight;

        }

        [Test]
        public void CalculateTotalWhenUserBuyThreeEconomyTickets_ResultsAreCorrect() {

            

            // prepare
           float expected = 3000;
            int numberOfEconomyTickets = 3;
            int numberOfEconomyPlusTickets = 0;
            int numberOfBusinessTickets = 0;


            // action
            //  FlightInformation
            float total = flightinformation.calculateTotal(numberOfEconomyTickets,numberOfEconomyPlusTickets,numberOfBusinessTickets);

            // assert
            Assert.That(expected, Is.EqualTo(total));


        }

        [Test]
        public void CalculateTotalWhenUserBuyTwoBusinessTickets_ResultsAreCorrect()
        {



            // prepare
            float expected = 6000;
            int numberOfEconomyTickets = 0;
            int numberOfEconomyPlusTickets = 0;
            int numberOfBusinessTickets = 2;


            // action
            //  FlightInformation
            float total = flightinformation.calculateTotal(numberOfEconomyTickets, numberOfEconomyPlusTickets, numberOfBusinessTickets);

            // assert
            Assert.That(expected, Is.EqualTo(total));


        }

        [Test]
        public void CalculateTotalWhenUserBuyTwoBusinessTicketsAndTwoEconomy_ShouldNotBook_ResultIsNegetive()
        {

       


            // prepare
            float expected = -1;
            int numberOfEconomyTickets = 2;
            int numberOfEconomyPlusTickets = 0;
            int numberOfBusinessTickets = 2;


            // action
            //  FlightInformation
            float total = flightinformation.calculateTotal(numberOfEconomyTickets, numberOfEconomyPlusTickets, numberOfBusinessTickets);

            // assert
            Assert.That(expected, Is.EqualTo(total));


        }


    }
}
