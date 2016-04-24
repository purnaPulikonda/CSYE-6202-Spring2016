using AirlineRegistration.Dao;
using log4net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class AddNewCarrier : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(AddNewCarrier));
        public AddNewCarrier()
        {
            InitializeComponent();
            logger.Info("In the Add new Carrier screen");
        }

        private void btnAddCrarrier_Click(object sender, EventArgs e)
        {
            if (valideteCarrierAdd())
            {
                logger.Info("Admin clicked on add carrier button");
                Carrier carrier = new Carrier();
                carrier.carrierId = txtCarrierId.Text;
                carrier.carrierName = txtCarrierName.Text;
                carrier.carrierLocation = txtCarrierLocation.Text;

                CarrierDAL cdal = new CarrierDAL();
                int result=0;
                try
                {
                    cdal.openConnection();
                     result = cdal.addCarrier(carrier);
                    cdal.closeConnection();
                }
                catch (SqlException ex) {
                    logger.Error(ex);
                }
                if (result == 1)
                {
                    MessageBox.Show("Successfully Added", "Carrier Confirmation", MessageBoxButtons.OK);
                    logger.Debug("The admin successfully added a new carrier");

                }
            }
            else {
                MessageBox.Show("Information Incomplete");
            }

        }

        private bool valideteCarrierAdd()
        {
            if ((String .IsNullOrEmpty(txtCarrierId.Text) ) && (String.IsNullOrEmpty(txtCarrierName.Text)) && (String.IsNullOrEmpty(txtCarrierLocation.Text)))
                return false;
            return true;
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

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtCarrierId.Text = null;
            txtCarrierLocation.Text = null;
            txtCarrierName.Text = null;
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllCArriers sc = new ShowAllCArriers();
            sc.Show();
        }
    }

}

