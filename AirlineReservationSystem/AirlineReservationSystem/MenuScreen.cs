using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class MenuScreen : Form
    {
        User user;
        private static ILog logger = LogManager.GetLogger(typeof(MenuScreen));
        public MenuScreen()
        {
            InitializeComponent();
        }
        public MenuScreen(User user)
        {
            InitializeComponent();
            this.user = user;
            logger.Info("In main user menu screen");
        }

        private void btnBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            BookingScreen bk = new BookingScreen(user);
            bk.Show();
        }

        private void btnUserInfo_Click(object sender, EventArgs e)
        {
            this.Hide();
            UserInformation us = new UserInformation(user);
            us.Show();
         
        }

        private void btnPreviousBookings_Click(object sender, EventArgs e)
        {
            this.Hide();
            ShowBookings sb = new ShowBookings(user);
            sb.Show();

        }

        public void email() {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();

                message.From = new MailAddress("purnarox@gmail.com");
                message.To.Add(new MailAddress("purnarox@gmail.com"));
                message.Subject = "Test";
                message.Body = "Content";

                smtp.Port = 587;
                smtp.Host = "smtp.gmail.com";
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("purnarox@gmail.com", "timgp9291");
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.Send(message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("err: " + ex.Message);
            }
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
