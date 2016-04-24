using AirlineRegistration.Dao;
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
using C = System.Data.SqlClient;  // System.Data.dll

namespace AirlineReservationSystem
{
    public partial class LoginScreen : Form
    {
        private static ILog logger = LogManager.GetLogger(typeof(LoginScreen));
        public LoginScreen()
        {
            InitializeComponent();
            log4net.Config.XmlConfigurator.Configure();
            logger.Info("In Logging Screen");
        }

        private void BtnSignIn_Click(object sender, EventArgs e)
        {
            LoginDetailsDAL loginDAl = new LoginDetailsDAL();
            logger.Info("Clicked on Sign in button");
            string logintype = "";
            User usr = new User();
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            try
            {
                loginDAl.openConnection();
                logintype = loginDAl.isValidLogin(username, password);
                loginDAl.closeConnection();
            }
            catch (Exception ex) {
                logger.Error(ex);
            }

            if (logintype.Equals("u"))
            {
                logger.Debug("Login correct, type user");
                UserDAL userDal = new UserDAL();
                userDal.openConnection();
                usr = userDal.getUserFromUsername(username);
                userDal.closeConnection();
                this.Hide();
                MenuScreen menuscreen = new MenuScreen(usr);
                menuscreen.Show();
            }

            else if (logintype.Equals("a"))
            {
                logger.Debug("Login correct, type admin");
                this.Hide();
                AdminMainMenu menuscreen = new AdminMainMenu();
                menuscreen.Show();
            }
            else {
                logger.Error("Invalid Login Provided");
                MessageBox.Show("Invalid login");
            }


        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            this.Hide();
            Register register = new Register();
            register.Show();
        }
    }
}
