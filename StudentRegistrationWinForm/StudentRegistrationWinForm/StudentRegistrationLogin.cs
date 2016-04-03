using System;
using System.Windows.Forms;

namespace StudentRegistrationWinForm
{
    public partial class StudentRegistrationLogin : Form
    {
        int numberAttempts;

        public StudentRegistrationLogin()
        {
            InitializeComponent();
            numberAttempts = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
               
            string username = "user";
            string password = "pass";
            if (numberAttempts < 3)
            {

                if (txtUsername.Text == username && txtPassword.Text == password)
                {
                 //   MessageBox.Show("successfull");
                    this.Hide();
                    var form = new MainWindow();
                    form.ShowDialog();


                }
                else {
                    numberAttempts++;
                    MessageBox.Show("Login failed, attempt number:" + numberAttempts);

                }
            }
            else {
                MessageBox.Show("exceeded login attempts, Exit Application");
                System.Environment.Exit(1);
            }
        }
    }
}
