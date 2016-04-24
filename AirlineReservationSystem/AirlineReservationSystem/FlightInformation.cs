using AirlineRegistration.Dao;
using log4net;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class FlightInformation : Form
    {
        public Flight flight;
        public User user;
        private static ILog logger = LogManager.GetLogger(typeof(FlightInformation));
        public FlightInformation()
        {
            InitializeComponent();
        }
        public FlightInformation(User user, Flight flight)
        {
            InitializeComponent();
            this.user = user;
            this.flight = flight;
            logger.Info("In the flight information screen, flight number:" + flight.flightId);
            loadFlightDetails();
            loadCarrierDetails();
            loadFlightCrewDetails();
            loadComboBox();
            loadPassengers();

        }

        public void loadPassengers()
        {
            DataTable dt = new DataTable();
            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
              ("ConnString");
            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select * from flightBookings 
                WHERE  flightid =@flightid";

                cmdOne.Parameters.AddWithValue("@flightid", flight.flightId);
                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {

                    dt.Load(reader);
                    dataGridViewPassengers.DataSource = dt;

                }
            }
            if (dt.Rows.Count > 0)
            {
                dataGridViewPassengers.Columns[0].HeaderText = "User ID";
                //dataGridViewBooking.Columns[0].Visible = false;
                dataGridViewPassengers.Columns[1].HeaderText = "Flight ID";
                dataGridViewPassengers.Columns[2].HeaderText = "Number of Seats";
                dataGridViewPassengers.Columns[3].HeaderText = "Amount Per Seat";
                dataGridViewPassengers.Columns[4].HeaderText = "Category";
                dataGridViewPassengers.Columns[4].HeaderText = "User Name";
            }
        }
        public void loadComboBox()
        {


        }
        public void loadFlightDetails()
        {
            label1.Text = "Flight Id:         \t" + flight.flightId;
            label2.Text = "Flight Name:       \t" + flight.flightName;
            label3.Text = "Flight Date:       \t" + flight.thisDate;
            label4.Text = "Flight Time:       \t" + flight.timeSpan;
            label5.Text = "Flight Source:     \t" + flight.flightSource;
            label6.Text = "Flight Destination:\t" + flight.flightDestination;
            label7.Text = "Economy price:     \t" + flight.economyPrice;
            label8.Text = "EconomyPlus price: \t" + flight.ecoPlusPrice;
            label9.Text = "Business price:    \t" + flight.businessPrice;

        }

        public void loadCarrierDetails()
        {

            CarrierDAL cdal = new CarrierDAL();
            cdal.openConnection();
            Carrier c = cdal.getCarrierFromID(flight.carrierId);
            cdal.closeConnection();

            if (c.carrierId != null)
            {
                label13.Text = c.carrierId;
                label14.Text = c.carrierName;
                label15.Text = c.carrierLocation;
            }
            else {
                logger.Error("Cannot find Carrier");
            }

        }


        public void loadFlightCrewDetails()
        {

            //   FlightCrewDAL fcdal = new FlightCrewDAL();
            //   fcdal.openConnection();
            //   var reader = fcdal.getCrewByCrewid(flight.flightCrewId);
            //    fcdal.closeConnection();

            DataTable dt = new DataTable();
            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
            ("ConnString");


            // Create an SqlConnection from the provided connection string.
            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select * from employee 
                WHERE  crewId =@crewId";

                cmdOne.Parameters.AddWithValue("@crewId", flight.flightCrewId);
                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {

                    dt.Load(reader);
                    dataGridViewEmployee.DataSource = dt;
                }
            }


            if (dt.Rows.Count > 0)
            {
                dataGridViewEmployee.Columns[0].HeaderText = "EmployeeId ";
                dataGridViewEmployee.Columns[1].HeaderText = "Employee Name";
                dataGridViewEmployee.Columns[2].HeaderText = "Address";
                dataGridViewEmployee.Columns[3].HeaderText = "Phone";
                dataGridViewEmployee.Columns[4].HeaderText = "Email";
                dataGridViewEmployee.Columns[5].Visible = false;
            }
            else {
                logger.Error("The crew details not loaded");
            }


        }
        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }


        public float calculateTotal(int ecotickets, int ecoplustickets, int busstickts)
        {
            float ecTotal = flight.economyPrice * ecotickets;
            float ecptotal = flight.ecoPlusPrice * ecoplustickets;
            float btotal = flight.businessPrice * busstickts;
            if (checkIfOnlyOneSelected(ecTotal, ecptotal, btotal))
            {
                float total = ecptotal + ecTotal + btotal;
                return total;
            }
            else {
                return -1;
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {


            int ecotickets = GetValue(textBoxEco.Text);
            int ecoplustickets = GetValue(textBoxEcoPlus.Text);
            int busstickts = GetValue(textBoxBusiness.Text);

            /*  float ecTotal = flight.economyPrice * ecotickets;
              float ecptotal = flight.ecoPlusPrice * ecoplustickets;
              float btotal = flight.businessPrice * busstickts;

              if (checkIfOnlyOneSelected(ecTotal, ecptotal, btotal))
              {
                  float Total = ecptotal + ecTotal + btotal;
                  lblTotal.Text = "Total : $" + Total;*/

            float Total = calculateTotal(ecotickets, ecoplustickets, busstickts);
            if (Total >= 0)
            {

                DialogResult dr = MessageBox.Show("Are you sure you want Book this flight? The total Amount is : $" + Total, "Book flight", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    FlightBoookings fb = new FlightBoookings();
                    fb.user = user;
                    fb.flight = flight;

                    if (ecotickets != 0)
                    {
                        if (ecoplustickets == 0 && busstickts == 0)
                        {
                            fb.numberOfSeats = ecotickets;
                            fb.category = "Economy";
                            fb.amountperSeat = flight.economyPrice;
                        }
                        else {

                        }
                    }
                    else if (ecoplustickets != 0)
                    {
                        fb.numberOfSeats = ecoplustickets;
                        fb.category = "Economy Plus";
                        fb.amountperSeat = flight.ecoPlusPrice;
                    }
                    else if (busstickts != 0)
                    {
                        fb.numberOfSeats = busstickts;
                        fb.category = "Business";
                        fb.amountperSeat = flight.businessPrice;
                    }
                    int result = 0;
                    try
                    {
                        FlightBoookings flightb = new FlightBoookings();

                        User fbuser = new User();
                        fbuser.id = user.id;
                        fbuser.name = user.name;

                        Flight fbflight = new Flight();
                        fbflight.flightId = flight.flightId;

                        flightb.user = fbuser;
                        flightb.flight = fbflight;
                        flightb.numberOfSeats = fb.numberOfSeats;
                        flightb.amountperSeat = fb.amountperSeat;
                        flightb.category = fb.category;

                        FlightBookingDAL fbdal = new FlightBookingDAL();
                        fbdal.openConnection();
                        result = fbdal.insertFlightBooking(flightb);
                        fbdal.closeConnection();
                    }
                    catch (SqlException ex)
                    {
                        logger.Error(ex);
                    }

                    if (result == 1)
                    {
                        sendEmail(user.emailId, flight.flightId, fb.numberOfSeats, Total);
                        MessageBox.Show("Tickets Booked Successfully", "Ticket Confirmation", MessageBoxButtons.OK);
                        logger.Debug("Booked tickets for flight, total=" + Total + "number of tickets" + fb.numberOfSeats);
                    }
                    else {
                        MessageBox.Show("Booking Unsuccesfull");
                    }

                }

            }
            else {
                MessageBox.Show("Incomplete/Incorrect information Entered", "Warning Information", MessageBoxButtons.OK);
            }
        }

        private bool checkIfOnlyOneSelected(float ecTotal, float ecptotal, float btotal)
        {
            if ((ecTotal > 0 && ecptotal == 0 && btotal == 0) ||
                (ecTotal == 0 && ecptotal > 0 && btotal == 0) ||
                (ecTotal == 0 && ecptotal == 0 && btotal > 0))
                return true;

            else return false;
        }


        public void sendEmail(String email, String flightid, int numberofseats, float total)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("purnaairlinereservation@gmail.com");
                message.To.Add(new MailAddress(email));
                message.Subject = "Flight Booking";
                message.Body = "Booking Confirmation for Flight" + flightid +
                    "\n Number of seats booked:" + numberofseats + "\n Total amount $" + total;
                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("purnaairlinereservation@gmail.com", "pur@12345");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                // MessageBox.Show("err: " + ex.Message);
                logger.Error("error:" + ex.Message);
            }
        }


        public int GetValue(string input)
        {
            int val;

            if (Int32.TryParse(input.Trim(), out val))
            {
                return val;
            }

            return 0;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuScreen ms = new MenuScreen(user);
            ms.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingScreen bs = new BookingScreen(user);
            bs.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserInformation ui = new UserInformation(user);
            ui.Show();
        }

        private void toolStripLabel4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FlightInformation_Load(object sender, EventArgs e)
        {

        }
    }

}