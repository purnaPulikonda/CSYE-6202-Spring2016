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
    public partial class MainWindow : Form
    {
        public MainWindow()
        {
            InitializeComponent();
            Init();
           
        }

        private void Init()
        {
            HelperClass.generateRecords();
            LoadDepartments();
            LoadValuesinGrid();
        }

        private void LoadDepartments()
        {
            comboBoxDepartment.Items.AddRange(new[] { "Information Systems", "International Affairs", "Nursing", "Pharmacy",
                "Professional Studies", "Psychology", "Public Administration" });
           
    }

        public void LoadValuesinGrid()
        {
            // dirty workaround to make sure that we can bind to the static mock list
          
            var bindingList = new BindingList<Student>(HelperClass.listOfStudents);
            var source = new BindingSource(bindingList, null);
            dataGridViewStudents.DataSource = source;
        }

        private Student getSelectedStudent() {
            Student s = new Student();
            foreach (DataGridViewRow row in dataGridViewStudents.SelectedRows)
            {
                s.StudentID = row.Cells[0].Value.ToString();
                s.FirstName = row.Cells[1].Value.ToString();
                s.LastName = row.Cells[2].Value.ToString();

                s.Department = row.Cells[3].Value.ToString();

                // enrollment type selection driven by the grid itself
                var enrollmentType = row.Cells[4].Value.ToString();
                if (radioButtonFullTime.Text == enrollmentType)
                {
                    radioButtonFullTime.Checked = true;
                    s.EnrollmentType = "FullTime";
                }
                else if (radioButtonPartTime.Text == enrollmentType)
                {
                    radioButtonPartTime.Checked = true;
                    s.EnrollmentType = "PartTime";
                }
            }
            return s;

        }
        private void btnEditStudent_Click(object sender, EventArgs e)
        {
            Student s = getSelectedStudent();
            var form = new EditStudentRegistration(s);
            form.ShowDialog();
        }

        private void btnNewStudent_Click(object sender, EventArgs e)
        {
            this.Hide();
            var form = new NewStudentForm();
            form.ShowDialog();
        }

        private void HandleDataGridViewStudentsSelectionChanged(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridViewStudents.SelectedRows)
            {
                textBox2.Text = row.Cells[0].Value.ToString();
                textBox3.Text = row.Cells[1].Value.ToString();
                textBox4.Text = row.Cells[2].Value.ToString();

                comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(row.Cells[3].Value.ToString());
                Console.Write("dep :" + row.Cells[3].Value.ToString());
                Console.Write("index of row"+ comboBoxDepartment.Items.IndexOf(row.Cells[3].Value.ToString()));
                Console.WriteLine(row.Cells[3].Value.ToString());
                Console.WriteLine("index of info"+comboBoxDepartment.Items.IndexOf("Information Systems"));
               
              //  Console.WriteLine(comboBoxDepartment.);

                // enrollment type selection driven by the grid itself
                string enrollmentType = row.Cells[4].Value.ToString();
                if ( enrollmentType== "FullTime")
                {
                    radioButtonFullTime.Checked = true;
                }
                else if ("PartTime" == enrollmentType)
                {
                    radioButtonPartTime.Checked = true;
                }
            }
        }
        private void dataGridViewStudents_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void groupBoxStudentInfo_Enter(object sender, EventArgs e)
        {

        }

        private void groupBoxEnrollmentType_Enter(object sender, EventArgs e)
        {

        }

        private void radioButtonPartTime_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void radioButtonFullTime_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void comboBoxDepartment_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblDepartment_Click(object sender, EventArgs e)
        {

        }

        private void lblLastName_Click(object sender, EventArgs e)
        {

        }

        private void lblFirstName_Click(object sender, EventArgs e)
        {

        }

        private void lblStudentID_Click(object sender, EventArgs e)
        {

        }

        private void btnRemoveStudent_Click(object sender, EventArgs e)
        {
            Student s = getSelectedStudent();
            HelperClass.removeStudent(s);
            LoadValuesinGrid();
        }
    }
}
