using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class AddNewFlight : Form
    {
        List<String> carrierIdList = new List<String>();
        List<int> crewIdList = new List<int>();
        private static ILog logger = LogManager.GetLogger(typeof(AddNewCarrier));
        public AddNewFlight()
        {
            InitializeComponent();
            logger.Info("Admin in Flight add screen");
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
        private void button1_Click(object sender, EventArgs e)
        {
            Flight f = new Flight();
            f.flightId = txtId.Text;
            f.flightName = txtName.Text;
            f.thisDate = dateTimePicker.Value.ToString("yyyy-MM-dd");
            f.timeSpan = txtTime.Text;
            f.flightSource = txtSource.Text;
            f.flightDestination = txtDestination.Text;
            f.economyPrice = GetValueFloat(txtEconomyPrice.Text);
            f.ecoPlusPrice = GetValueFloat(txtEcoPlusPrice.Text);
            f.businessPrice =GetValueFloat(txtBusinessPrice.Text);
            f.carrierId = comboBoxCarrier.GetItemText(comboBoxCarrier.SelectedItem);
            f.flightCrewId = GetValueInt(comboBoxCrew.GetItemText(comboBoxCrew.SelectedItem));

            if (String.IsNullOrEmpty(f.flightId) || String.IsNullOrEmpty(f.flightName) ||
                            String.IsNullOrEmpty(f.thisDate) || String.IsNullOrEmpty(f.timeSpan) ||
                            String.IsNullOrEmpty(f.flightSource) || String.IsNullOrEmpty(f.flightDestination) ||
                            String.IsNullOrEmpty(f.carrierId)||f.economyPrice==0||f.ecoPlusPrice==0|| f.businessPrice==0||
                            f.flightCrewId==0)
            {
                MessageBox.Show("Incomplete information", "Warning", MessageBoxButtons.OK);
            }
            else {
                int result=0;
                FlightDAL fdal = new FlightDAL();
                try
                {
                    fdal.openConnection();
                     result = fdal.createNewFlight(f);
                    fdal.closeConnection();
                }
                catch (SqlException ex) {
                    logger.Error(ex);

                }
                if (result == 1)
                {
                    MessageBox.Show("Flight Added");
                    logger.Debug("Admin successfully added a new flight" + f.flightId);
                }
                else {
                    MessageBox.Show("Unsucessful in adding the flight");
                }

            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminMainMenu amm = new AdminMainMenu();
            amm.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void ShowAllFLights_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllFlights swl = new ShowAllFlights();
            swl.Show();
        }
    }
}

