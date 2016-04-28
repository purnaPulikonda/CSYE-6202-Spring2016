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
    public partial class ShowAllCArriers : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(ShowAllCArriers));
        public ShowAllCArriers()
        {
            InitializeComponent();
            loadCArriers();
        }

        private void loadCArriers()
       
        {
            DataTable dt = new DataTable();

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
                    dt.Load(reader);
                    dataGridViewCarrier.DataSource = dt;
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            AddNewCarrier anc = new AddNewCarrier();
            anc.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int result = 0;
            Carrier c = getSelectedCarrier();
            try
            {
                CarrierDAL cdal = new CarrierDAL();
                cdal.openConnection();
               result= cdal.removeCarrier(c);
                cdal.closeConnection();
            }
            catch (SqlException ex) {
            }
            if (result == 1) {
                MessageBox.Show("Successfully deleted carrier","Conformation", MessageBoxButtons.OK);
            }
            loadCArriers();
        }


        public Carrier getSelectedCarrier() {
            Carrier c = new Carrier();
            foreach (DataGridViewRow row in dataGridViewCarrier.SelectedRows)
            {
                c.carrierId = row.Cells[0].Value.ToString();
                c.carrierName = row.Cells[1].Value.ToString();
                c.carrierLocation = row.Cells[2].Value.ToString();
              
            }
            return c;

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

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
            Carrier c = new Carrier();
              c=  getSelectedCarrier();
            Console.WriteLine(c);
          
            UpdateCarrier uc = new UpdateCarrier(c);
            uc.Show();
        }
    }
}
