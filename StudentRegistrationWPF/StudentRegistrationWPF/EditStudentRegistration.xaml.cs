using System;
using System.Windows;


namespace StudentRegistrationWPF
{
    /// <summary>
    /// Interaction logic for EditStudentRegistration.xaml
    /// </summary>
    public partial class EditStudentRegistration : Window
    {
        Student s;
        public EditStudentRegistration()
        {
            InitializeComponent();
        }
        public EditStudentRegistration(Student s)
        {
            InitializeComponent();
            LoadDepartments();
            this.s = s;
            displayStudentValues();

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

        private void displayStudentValues()
        {
            txtFirstName.Text = s.FirstName;
            txtLastName.Text = s.LastName;
            txtStudentID.Text = s.StudentID;
            comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(s.Department);
            if (s.EnrollmentType == Helper.EnrollmentType[0])
            {
                radioButtonFullTime.IsChecked = true;
            }
            else {
                radioButtonPartTime.IsChecked = true;
            }
        }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            if (txtStudentID.Text == null || txtStudentID.Text == "" || txtFirstName.Text == "" || txtFirstName.Text == null || txtLastName.Text == null || txtLastName.Text == "")
            {
                MessageBox.Show("Please fill all fields", "Edit Student Registration Warning", MessageBoxButton.OK);

            }
            else {
                MessageBoxResult dr = MessageBox.Show("Are you sure you want to update this student?", "Edit student registration Confirmation", MessageBoxButton.YesNo);
                if (dr == MessageBoxResult.Yes)
                {


                    if (!Helper.validateStudentID(txtStudentID.Text) || !Helper.ValidateName(txtFirstName.Text) || !Helper.ValidateName(txtLastName.Text))
                    {

                        MessageBox.Show("Student Information not in format", "Edir student registration Warning", MessageBoxButton.OK);
                    }

                    else {
                        Student updatedStudent = new Student();

                        updatedStudent.FirstName = txtFirstName.Text;
                        updatedStudent.LastName = txtLastName.Text;
                        updatedStudent.StudentID = txtStudentID.Text;
                        updatedStudent.Department = comboBoxDepartment.SelectedItem.ToString();
                        Console.WriteLine(comboBoxDepartment.SelectedItem.ToString());
                        if (radioButtonFullTime.IsChecked == true)
                        {
                            updatedStudent.EnrollmentType = Helper.EnrollmentType[0];

                        }
                        else {
                            updatedStudent.EnrollmentType = Helper.EnrollmentType[1];
                        }


                        Helper.removeStudent(s);
                        Helper.addStudent(updatedStudent);
                        this.Hide();
                        var form = Helper.getForm();
                        form.LoadValuesinGrid();
                        form.Show();

                    }
                }
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var form = Helper.getForm();
            form.Show();
        }
    }
}
