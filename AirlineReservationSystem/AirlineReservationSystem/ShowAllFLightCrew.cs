using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class ShowAllFLightCrew : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(ShowAllFLightCrew));
        public ShowAllFLightCrew()
        {
            InitializeComponent();
            logger.Info("In show all flights crew screen");
            LoadFLightCrew();
        }

        private void LoadFLightCrew()
        {
            DataTable dt = new DataTable();

            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
                ("ConnString");

            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select distinct crewId from CrewEmployee";

                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {
                    dt.Load(reader);
                    dataGridViewCrew.DataSource = dt;
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

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            AddNewFlightCrew afc = new AddNewFlightCrew();
            afc.Show();

        }
        public FlightCrew getSelectedCrew()
        {
            FlightCrew c = new FlightCrew();
            foreach (DataGridViewRow row in dataGridViewCrew.SelectedRows)
            {
                c.crewId = Int32.Parse(row.Cells[0].Value.ToString());

            }
            return c;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            int result = 0;
            FlightCrewDAL fdal = new FlightCrewDAL();
            try
            {
                fdal.openConnection();
                result = fdal.removeCrew(getSelectedCrew());
                fdal.closeConnection();
            }
            catch (Exception ex) {
                logger.Error(ex);
            }
            if (result == 1)
            {
                MessageBox.Show("Successfully removed crew", "confirmation", MessageBoxButtons.OK);

            }
            else {
                MessageBox.Show("Operation failed", "confirmation", MessageBoxButtons.OK);
            }

            LoadFLightCrew();
        }

    }
}
