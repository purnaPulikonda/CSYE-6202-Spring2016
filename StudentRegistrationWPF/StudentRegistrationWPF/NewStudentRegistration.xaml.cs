using System;
using System.Windows;

namespace StudentRegistrationWPF
{
    /// <summary>
    /// Interaction logic for NewStudentRegistration.xaml
    /// </summary>
    public partial class NewStudentRegistration : Window
    {
        public NewStudentRegistration()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            LoadDepartments();
            LoadDefaults();
        }
        private void LoadDepartments()
        {
            comboBoxDepartment.Items.Add("Information Systems");
            comboBoxDepartment.Items.Add("International Affairs");
            comboBoxDepartment.Items.Add("Nursing");
            comboBoxDepartment.Items.Add("Pharmacy");
            comboBoxDepartment.Items.Add("Professional Studies");
            comboBoxDepartment.Items.Add("Psychology");
            comboBoxDepartment.Items.Add("Public Administration");

        }

        private void LoadDefaults()
        {
           
            comboBoxDepartment.SelectedIndex = 0;
            radioButtonFullTime.IsChecked = true;
            btnAddStudent.IsEnabled = false;
            btnReset.IsEnabled = false;

        }
        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (txtStudentID.Text == null || txtFirstName.Text == "" || txtLastName.Text == "" || txtFirstName.Text == "" || txtLastName.Text == "" || txtFirstName.Text == null || txtLastName.Text == null)
            {
                MessageBox.Show("Please fill in all fields", "New Student Registration Warning", MessageBoxButton.OK);

            }
            else {

                if (!Helper.validateStudentID(txtStudentID.Text) || !Helper.ValidateName(txtFirstName.Text) || !Helper.ValidateName(txtLastName.Text))
                {

                    MessageBox.Show("Student values not in format", "New Student Registration Warning", MessageBoxButton.OK);
                }
                else {

                    String enrollmentType;
                    if (radioButtonFullTime.IsChecked == true)
                    {
                        enrollmentType = "FullTime";
                    }
                    else {
                        enrollmentType = "PartTime";
                    }
                    Student student = new Student(txtStudentID.Text, txtFirstName.Text, txtLastName.Text, comboBoxDepartment.SelectedItem.ToString(), enrollmentType);
                    Helper.addStudent(student);
                    this.Hide();
                    var form = Helper.getForm();
                    form.LoadValuesinGrid();
                    form.Show();
                }
            }

        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            txtStudentID.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            radioButtonFullTime.IsChecked = true;
            comboBoxDepartment.SelectedIndex = 0;
        }

        private void txtStudentID_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            btnAddStudent.IsEnabled = true;
            btnReset.IsEnabled = true;
        }
    }
}