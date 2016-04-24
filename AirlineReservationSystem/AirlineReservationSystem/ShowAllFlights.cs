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

namespace AirlineReservationSystem
{
    public partial class ShowAllFlights : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(ShowAllFlights));
        public List<Flight> listOfFlights;
        public ShowAllFlights()
        {
            InitializeComponent();
            LoadFLightsFromDatabase();
            LoadValuesinGrid();
        }

        public void LoadFLightsFromDatabase()
        {
            listOfFlights = new List<Flight>();
            FlightDAL flightdal = new FlightDAL();
            flightdal.openConnection();
            listOfFlights = flightdal.getAllFlights(listOfFlights);
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

      
        private void button2_Click(object sender, EventArgs e)
        {
            Flight f = getSlectedFlight();
            FlightDAL fdal = new FlightDAL();
            try
            {
                fdal.openConnection();
                fdal.removeFlight(f);
                fdal.closeConnection();
            }
            catch (Exception ex) {
                logger.Error(ex);

            }
            LoadFLightsFromDatabase();
            LoadValuesinGrid();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AddNewFlight anf = new AddNewFlight();
            anf.Show();
        }

        private void btnMoreInfo_Click_1(object sender, EventArgs e)
        {
            this.Hide();
            Flight f = getSlectedFlight();
            logger.Debug("User Clicked on update of flight" + f.flightId);
            UpdateFlight uf = new UpdateFlight(f);
            uf.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminMainMenu amm = new AdminMainMenu();
            amm.Show();

        }
    }
}
