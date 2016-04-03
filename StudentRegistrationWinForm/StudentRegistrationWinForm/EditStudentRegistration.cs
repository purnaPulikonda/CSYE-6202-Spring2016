using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StudentRegistrationWinForm
{
    public partial class EditStudentRegistration : Form
    {
        private Student s;

        public EditStudentRegistration()
        {
            InitializeComponent();
        }

        public EditStudentRegistration(Student s)
        {
            InitializeComponent();
            this.s = s;
            displayStudentValues();
           
        }

        private void displayStudentValues()
        {
            txtFirstName.Text = s.FirstName;
            txtLastName.Text = s.LastName;
            txtStudentID.Text = s.StudentID;
            comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(s.Department);
            if (s.EnrollmentType == "FullTime")
            {
                radioButtonFullTime.Checked = true;
            }
            else {
                radioButtonPartTime.Checked = false;
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DialogResult dr = MessageBox.Show("Are you sure you want to update this student?", "Edit student registration", MessageBoxButtons.YesNo,
         MessageBoxIcon.Information);
            if (dr == DialogResult.Yes)
            {
                if (txtStudentID == null || txtFirstName == null || txtLastName == null)
                {
                    MessageBox.Show("Please fill all fields", "Edit Student Registration Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                }

                else if (!HelperClass.validateStudentID(txtStudentID.Text) || !HelperClass.ValidateName(txtFirstName.Text) || !HelperClass.ValidateName(txtLastName.Text))
                {

                    MessageBox.Show("Student Information not in format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                else {
                    Student updatedStudent = new Student();

                    updatedStudent.FirstName = txtFirstName.Text;
                    updatedStudent.LastName = txtLastName.Text;
                    updatedStudent.StudentID = txtStudentID.Text;
                    //    updatedStudent.Department = comboBoxDepartment.Selected
                    if (radioButtonFullTime.Checked == true)
                    {
                        updatedStudent.EnrollmentType = "FullTime";

                    }
                    else {
                        updatedStudent.EnrollmentType = "PartTime";
                    }

                    HelperClass.addStudent(updatedStudent);
                    HelperClass.removeStudent(s);
                    this.Hide();
                    var form = new MainWindow();
                    form.ShowDialog();

                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new MainWindow();
            form.ShowDialog();
        }
    }
}
