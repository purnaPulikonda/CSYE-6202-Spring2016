using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using C = System.Data.SqlClient;

namespace AirlineReservationSystem
{
    public partial class BookingScreen : Form
    {
        User user;
        private static ILog logger = LogManager.GetLogger(typeof(ShowBookings));
        public List<Flight> listOfFlights;
        public BookingScreen()
        {
            InitializeComponent();
        }
        public BookingScreen(User user)
        {
            InitializeComponent();
            logger.Info("In flight booking screen");
            Init();
            this.user = user;
        }

        public void Init()
        {
            LoadFLightsFromDatabase();
            LoadValuesinGrid();
        }

        public void LoadFLightsFromDatabase()
        {
            listOfFlights = new List<Flight>();
            FlightDAL flightdal = new FlightDAL();
            flightdal.openConnection();
            listOfFlights= flightdal.getAllFlights(listOfFlights);
            flightdal.closeConnection();

        }

        public void LoadValuesinGrid()
        {
            //   dataGridViewFlights.Rows.Clear();
            //   dataGridViewFlights.AutoSize = true;
            Console.WriteLine(listOfFlights);
            var bindingList = new BindingList<Flight>(listOfFlights);
            var source = new BindingSource(bindingList, null);
            dataGridViewFlights.DataSource = source;
           
            dataGridViewFlights.Columns[0].HeaderText = "Flight ID";
            //  dataGridViewBooking.Columns[0].Visible = false;
            dataGridViewFlights.Columns[1].HeaderText = "Flight Name";
            dataGridViewFlights.Columns[2].HeaderText = "Date";
            dataGridViewFlights.Columns[3].HeaderText = "Time";
            dataGridViewFlights.Columns[4].HeaderText = "Source";
            dataGridViewFlights.Columns[5].HeaderText = "Destination";
            dataGridViewFlights.Columns[6].HeaderText = "Economy Price";
            dataGridViewFlights.Columns[7].HeaderText = "Economy Plus Price";
            dataGridViewFlights.Columns[8].HeaderText = "Business Price";
            dataGridViewFlights.Columns[9].Visible = false;
            dataGridViewFlights.Columns[10].Visible = false;
        }

        private void tsbMainMenu_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuScreen mm = new MenuScreen(user);
            this.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Hide();
            MenuScreen ms = new MenuScreen(user);
            ms.Show();
        }

        private void btnShowAll_Click(object sender, EventArgs e)
        {
            LoadFLightsFromDatabase();
            LoadValuesinGrid();
        }

        private void dataGridViewFlights_AllowUserToAddRowsChanged(object sender, EventArgs e)
        {

        }

        private void dataGridViewFlights_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private Flight getSlectedFlight()
        {
            Flight ft = new Flight();
            foreach (DataGridViewRow row in dataGridViewFlights.SelectedRows)
            {
                ft.flightId = row.Cells[0].Value.ToString();
                ft.flightName = row.Cells[1].Value.ToString();
                ft.thisDate = row.Cells[2].Value.ToString();
                ft.timeSpan = row.Cells[3].Value.ToString();
                ft.flightSource = row.Cells[4].Value.ToString();
                ft.flightDestination = row.Cells[5].Value.ToString();
                ft.economyPrice = (float)row.Cells[6].Value;
                ft.ecoPlusPrice = (float)row.Cells[7].Value;
                ft.businessPrice = (float)row.Cells[8].Value;
                ft.carrierId = row.Cells[9].Value.ToString();
                ft.flightCrewId = (int)row.Cells[10].Value;
            }
            return ft;

        }

        private void btnMoreInfo_Click(object sender, EventArgs e)
        {
         
            this.Hide();
            Flight f = getSlectedFlight();
            logger.Debug("User Clicked on More information of flight" + f.flightId);
            FlightInformation fi = new FlightInformation(user, f);
            fi.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowBookings sb = new ShowBookings(user);
            sb.Show();
        }
    }
}
