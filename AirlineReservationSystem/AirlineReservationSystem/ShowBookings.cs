
using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class ShowBookings : Form
    {
        User user;
        private static ILog logger = LogManager.GetLogger(typeof(ShowBookings));
        List<FlightInformation> listInfo = new List<FlightInformation>();
        public ShowBookings()
        {
            InitializeComponent();
        }
        public ShowBookings(User user)
        {
            InitializeComponent();
            logger.Info("IN previous Booking screen");
            this.user = user;
            loadGrid();
        }

        public void loadGrid()
        {
         
            DataTable dt = new DataTable();
            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
            ("ConnString");


            // Create an SqlConnection from the provided connection string.
            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();
             

                cmdOne.CommandText = @"
                Select * from FlightBookings where userid =@userid";
                cmdOne.Parameters.AddWithValue("@userid", user.id);

            
                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {

                    dt.Load(reader);

                    dataGridViewBooking.DataSource = dt;
                }
            }
       
            Console.WriteLine("Reader::::::" + dataGridViewBooking);
            if (dt.Rows.Count > 0)
            {
                dataGridViewBooking.Columns[0].HeaderText = "My Id";
                dataGridViewBooking.Columns[0].Visible = false;
                dataGridViewBooking.Columns[1].HeaderText = "Flight ID";
                dataGridViewBooking.Columns[2].HeaderText = "Number of Seats";
                dataGridViewBooking.Columns[3].HeaderText = "Amount Per Seat";
                dataGridViewBooking.Columns[4].HeaderText = "Category";
            }
        }
        private void dataGridViewBookings_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridViewBooking_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuScreen ms = new MenuScreen(user);
            ms.Show();
        }

        private void btnCancelBooking_Click(object sender, EventArgs e)
        {
            logger.Info("clicked on cancel booking");
            DialogResult dr = MessageBox.Show("Are you sure you want to Cancel this Booking?", "Cancellation Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                FlightBoookings fb = getSelectedBooking();
                FlightBookingDAL fd = new FlightBookingDAL();
                fd.openConnection();
                var result = fd.deleteBooking(fb);
                    if (result == 1)
                    {
                    logger.Debug("Booking canceled flightid" + fb.flight.flightId+"by user");
                        MessageBox.Show("Booking Cancelled","Cancellation Confirmation",MessageBoxButtons.OK);
                    }
                    loadGrid();
                }
        }

        public FlightBoookings getSelectedBooking() {
            FlightBoookings fb = new FlightBoookings();
            foreach (DataGridViewRow row in dataGridViewBooking.SelectedRows)
            {
                User ufb = new User();
                ufb.id = (int)row.Cells[0].Value;
                fb.user = ufb;

                Flight uf = new Flight();
                uf.flightId = row.Cells[1].Value.ToString();
                fb.flight = uf;

                fb.numberOfSeats = (int)row.Cells[2].Value;
                fb.amountperSeat = float.Parse(row.Cells[3].Value.ToString());

            }
            return fb;

        }

    
    }
}
