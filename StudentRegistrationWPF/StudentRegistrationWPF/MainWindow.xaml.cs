using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace StudentRegistrationWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int numberAttempts;
        public MainWindow()
        {
            InitializeComponent();
            numberAttempts = 0;
        }

        private void btnSignIn_Click(object sender, RoutedEventArgs e)
        {
            string username = "user";
            string password = "pass";
            if (numberAttempts < 3)
            {

                if (txtUserName.Text == username && txtPassword.Password == password)
                {
                    //   MessageBox.Show("successfull");
                    this.Hide();
                    var form = Helper.instantiateForm();
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
    

