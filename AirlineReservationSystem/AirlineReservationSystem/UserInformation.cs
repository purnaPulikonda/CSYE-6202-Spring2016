using AirlineRegistration.Dao;
using log4net;
using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class UserInformation : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(UserInformation));
        User user;
        public UserInformation()
        {
            InitializeComponent();
        }
        public UserInformation(User user)
        {
            InitializeComponent();
            logger.Info("In user information screen");
            this.user = user;
            loadDefault();
        }
        public void loadDefault()
        {
            txtName.Enabled = false;
            txtAddress.Enabled = false;
            txtPhone.Enabled = false;
            txtEmail.Enabled = false;
            loadUser();
        }

        public void loadUser()
        {
            UserDAL userdal = new UserDAL();
            userdal.openConnection();
            User u = userdal.getUserFromUsername(user.username);
            userdal.closeConnection();


            txtName.Text = u.name;
            txtAddress.Text = u.address;
            txtPhone.Text = u.phoneNumber;
            txtEmail.Text = u.emailId;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = null;
            txtAddress.Text = null;
            txtPhone.Text = null;
            txtEmail.Text = null;

        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            txtName.Enabled = true;
            txtAddress.Enabled = true;
            txtPhone.Enabled = true;
            txtAddress.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtName.Text) || String.IsNullOrEmpty(txtAddress.Text) ||
                String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtPhone.Text))
            {

                MessageBox.Show("Please fill all the fields", "Incomplete Information", MessageBoxButtons.OK);
            }
            else {
                int result = 0;
                UserDAL udal = new UserDAL();
                try
                {
                    udal.openConnection();
                    result = udal.updateUser(txtName.Text, txtAddress.Text, txtPhone.Text, txtEmail.Text, user.id);
                    udal.closeConnection();
                }
                catch (SqlException ex) {
                    logger.Error(e);
                }
                if (result == 1)
                {

                    MessageBox.Show("Successfully updated");
                    logger.Debug("Successfully updated User details");
                }

               
            }
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            MenuScreen ms = new MenuScreen(user);
            ms.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
