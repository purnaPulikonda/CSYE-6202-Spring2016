using System;
using System.Windows.Forms;
using System.Drawing;
using AirlineRegistration.Dao;
using log4net;
using System.Data.SqlClient;

namespace AirlineReservationSystem
{
    public partial class Register : Form
    {
        User mainUser;
        private static ILog logger = LogManager.GetLogger(typeof(Register));
        public Register()
        {
            InitializeComponent();
        }

        private void btnRgister_Click(object sender, EventArgs e)
        {


            User user = new User();
            {
                user.name = txtName.Text;
                user.address = txtAddress.Text;
                user.phoneNumber = txtPhone.Text;
                user.emailId = txtEmail.Text;
                user.username
                    = txtUsername.Text;
            }

            LoginDetails lg = new LoginDetails();
            {
                lg.username = user.username;
                lg.password = txtPassword.Text;
            }


            if (validateRegister(user) && ValidateLogin(lg))
            {
                int num = 0;
                int num1 = 0;
                logger.Debug("Valid user details entered");
                try {
                    UserDAL userDAL = new UserDAL();
                    userDAL.openConnection();
                     num = userDAL.addUser(user);
                    userDAL.closeConnection();
                    LoginDetailsDAL loginDAL = new LoginDetailsDAL();
                    loginDAL.openConnection();
                     num1 = loginDAL.addLoginUser(lg, user);
                    loginDAL.closeConnection();
                }
            
            catch (SqlException ex) {
                    logger.Error(ex);
            }
                if (num == 1 && num1 == 1)
                {
                    logger.Debug("Created new user" + user.name);
                    MessageBox.Show("Successfully Added User", "Registeration confirmation", MessageBoxButtons.OK);
                    mainUser = user;

                }
            }

        }

        private bool ValidateLogin(LoginDetails lg)
        {
            return true;
        }

        private bool validateRegister(User user)
        {
            bool vemailId = false;
            bool vName = false;
            bool vAddress = false;
            bool vPhoneNUmber = false;
            bool vUsername = false;
            bool vPassword = false;


            if (Helper.ValidateName(user.name))
            {
                vName = true;
                txtName.BackColor = Color.White;
            }
            else {
                txtName.BackColor = Color.Yellow;
            }

            if (Helper.ValidateAddress(user.address))
            {
                vAddress = true;
                txtAddress.BackColor = Color.White;
            }
            else {
                txtAddress.BackColor = Color.Yellow;
            }
            if (Helper.ValidatePhoneNumber(user.phoneNumber))
            {
                vPhoneNUmber = true;
                txtPhone.BackColor = Color.White;
            }
            else {
                txtPhone.BackColor = Color.Yellow;
            }
            if (Helper.ValidateEmail(user.emailId))
            {
                vemailId = true;
                txtEmail.BackColor = Color.White;
            }
            else {
                txtEmail.BackColor = Color.Yellow;
            }
            if (Helper.ValidateName(user.username))
            {
                vUsername = true;
                txtUsername.BackColor = Color.White;
            }
            else {
                txtUsername.BackColor = Color.Yellow;
            }

            if (Helper.ValidateAddress(txtPassword.Text))
            {
                vPassword = true;
                txtPassword.BackColor = Color.White;
            }
            else {
                txtPassword.BackColor = Color.Yellow;
            }

            if (vName && vPhoneNUmber && vAddress && vemailId && vUsername && vPassword)
                return true;

            return false;
        }




        private void btnReset_Click(object sender, EventArgs e)
        {
            txtName.Text = null;
            txtAddress.Text = null;
            txtPhone.Text = null;
            txtEmail.Text = null;
            txtUsername.Text = null;
            txtPassword.Text = null;
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginScreen ls = new LoginScreen();
            ls.Show();
        }
    }
}