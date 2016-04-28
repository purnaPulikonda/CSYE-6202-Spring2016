using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AirlineRegistration.Dao;
using System.Data.SqlClient;
using log4net;

namespace AirlineReservationSystem
{
    public partial class UpdateFlight : Form
    {
        private Flight f;
        List<String> carrierIdList = new List<String>();
        List<int> crewIdList = new List<int>();
        private static ILog logger = LogManager.GetLogger(typeof(UpdateFlight));

        public UpdateFlight()
        {
            InitializeComponent();
           
        }

        public UpdateFlight(Flight f)
        {
            InitializeComponent();
            this.f = f;
            loadDetailsofFLight();
            logger.Info("In update flight screen");   
            loadCarrier();
            loadCrew();
            LoadComboBoxes();
        }

        public void loadCarrier()
        {

            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
                ("ConnString");

            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select * from Carrier";

                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        carrierIdList.Add(reader.GetString(0));
                    }
                }

            }
        }
        public void loadCrew()
        {

            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
                ("ConnString");

            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select DISTINCT crewid from crewEmployee";

                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {
                    while (reader.Read())
                    {

                        crewIdList.Add(reader.GetInt32(0));
                    }
                }

            }
        }

        public void LoadComboBoxes()
        {

            BindingSource source = new BindingSource();
            source.DataSource = carrierIdList;
            comboBoxCarrier.DataSource = source;

            BindingSource source1 = new BindingSource();
            source1.DataSource = crewIdList;
            comboBoxCrew.DataSource = source1;

        }
        private void loadDetailsofFLight()
        {
            txtId.Text = f.flightId;
            txtName.Text = f.flightName;
            txtSource.Text = f.flightSource;
            txtTime.Text = f.timeSpan;
            txtDate.Text = f.thisDate;
            txtDestination.Text = f.flightDestination;
            txtEconomyPrice.Text = f.economyPrice.ToString();
            txtEcoPlusPrice.Text = f.ecoPlusPrice.ToString();
            txtBusinessPrice.Text = f.businessPrice.ToString();


            txtId.Enabled = false;
            txtName.Enabled = false;
            txtSource.Enabled = false;
            txtDestination.Enabled = false;
            txtTime.Enabled = false;
            txtDate.Enabled = false;
            txtEconomyPrice.Enabled = false;
            txtEcoPlusPrice.Enabled = false;
            txtBusinessPrice.Enabled = false;

        }
        public int GetValueInt(string input)
        {
            int val;

            if (Int32.TryParse(input.Trim(), out val))
            {
                return val;
            }

            return 0;
        }
        public float GetValueFloat(string input)
        {
            float val;

            if (float.TryParse(input.Trim(), out val))
            {
                return val;
            }

            return 0;
        }
        private void Update_Click(object sender, EventArgs e)
        {
            Flight flight = new Flight();
            flight.flightId = f.flightId;
            flight.flightName = f.flightName;
            flight.thisDate = txtDate.Text;
            flight.timeSpan = txtTime.Text;
            flight.flightSource = txtSource.Text;
            flight.flightDestination = txtDestination.Text;
            flight.economyPrice = GetValueFloat(txtEconomyPrice.Text);
            flight.ecoPlusPrice = GetValueFloat(txtEcoPlusPrice.Text);
            flight.businessPrice = GetValueFloat(txtBusinessPrice.Text);
            flight.carrierId = comboBoxCarrier.GetItemText(comboBoxCarrier.SelectedItem);
            flight.flightCrewId = GetValueInt(comboBoxCrew.GetItemText(comboBoxCrew.SelectedItem));

            if (String.IsNullOrEmpty(flight.flightId) || String.IsNullOrEmpty(flight.flightName) ||
                            String.IsNullOrEmpty(flight.thisDate) || String.IsNullOrEmpty(flight.timeSpan) ||
                            String.IsNullOrEmpty(flight.flightSource) || String.IsNullOrEmpty(flight.flightDestination) ||
                            String.IsNullOrEmpty(flight.carrierId) || flight.economyPrice == 0 || flight.ecoPlusPrice == 0 || flight.businessPrice == 0 ||
                           flight.flightCrewId == 0)
            {
                MessageBox.Show("Incomplete information", "Warning", MessageBoxButtons.OK);
            }
            else {
                int result = 0;
                FlightDAL fdal = new FlightDAL();
                try
                {
                    fdal.openConnection();
                    result = fdal.updateFlight(flight);
                    fdal.closeConnection();
                }
                catch (SqlException ex)
                {
                    logger.Error(ex);

                }
                if (result == 1)
                {
                    MessageBox.Show("Flight Updated");
                    logger.Debug("Admin successfully updated a new flight" + f.flightId);
                }
                else {
                    MessageBox.Show("Unsucessful in updating the flight");
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            txtSource.Enabled = true;
            txtDestination.Enabled = true;
            txtTime.Enabled = true;
            txtDate.Enabled = true;
            txtEconomyPrice.Enabled = true;
            txtEcoPlusPrice.Enabled = true;
            txtBusinessPrice.Enabled = true;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminMainMenu amm = new AdminMainMenu();
           
            amm.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllFlights saf = new ShowAllFlights();
            saf.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
