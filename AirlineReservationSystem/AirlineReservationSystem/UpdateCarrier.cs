using AirlineRegistration.Dao;
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
    public partial class UpdateCarrier : Form
    {
        public Carrier c;
        public UpdateCarrier(Carrier c)
        {
            InitializeComponent();
            this.c = c;
            loadCarrierDetails();
        }

        private void loadCarrierDetails()
        {
            txtCarrierId.Text = c.carrierId;
            txtCarrierLocation.Text = c.carrierLocation;
            txtCarrierName.Text = c.carrierName;
            txtCarrierId.Enabled = false;
            txtCarrierLocation.Enabled = false;
            txtCarrierName.Enabled = false;
        }

        private void btnAddCrarrier_Click(object sender, EventArgs e)
        {
            txtCarrierLocation.Enabled = true;
            txtCarrierName.Enabled = true;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            int result = 0;
            CarrierDAL cdal = new CarrierDAL();
            try
            {
                cdal.openConnection();
               result= cdal.updateCArrier(c);
                cdal.closeConnection();
            }
            catch (SqlException ex) {

            }
            if (result == 1)
            {
                MessageBox.Show("Carrier Updated Successfully", "Confirmation", MessageBoxButtons.OK);

            }
            
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
            ShowAllCArriers sw = new ShowAllCArriers();
            sw.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
