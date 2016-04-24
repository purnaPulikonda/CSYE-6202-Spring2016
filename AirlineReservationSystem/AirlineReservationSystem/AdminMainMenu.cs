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
    public partial class AdminMainMenu : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(AdminMainMenu));
        public AdminMainMenu()
        {
            InitializeComponent();
            logger.Info("In the admin main menu");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowAllCArriers anc = new ShowAllCArriers();
            anc.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowAllFLightCrew sfc = new ShowAllFLightCrew();
            sfc.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllFlights saf = new ShowAllFlights();
            saf.Show();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to Sign out?", "Sign Out Confirmation", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                this.Close();
                LoginScreen ls = new LoginScreen();
                ls.Show();
            }
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
