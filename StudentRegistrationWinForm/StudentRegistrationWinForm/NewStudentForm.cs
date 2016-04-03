using System;
using System.Windows.Forms;

namespace StudentRegistrationWinForm
{
    public partial class NewStudentForm : Form
    {
        public NewStudentForm()
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
            comboBoxDepartment.Items.AddRange(HelperClass.DepartmentNames);

        }

        private void LoadDefaults()
        {
            radioButtonFullTime.Select();
            comboBoxDepartment.SelectedIndex = 0;

        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtStudentId == null || txtFirstName == null || txtLastName == null)
            {
                MessageBox.Show("Incomplete Information", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

            else if (!HelperClass.validateStudentID(txtStudentId.Text) || !HelperClass.ValidateName(txtFirstName.Text) || !HelperClass.ValidateName(txtLastName.Text)) {

                MessageBox.Show("Student Information not in format", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else {

                String enrollmentType;
                if (radioButtonFullTime.Checked == true)
                {
                    enrollmentType = "FullTime";
                }
                else {
                    enrollmentType = "PartTime";
                }
                Student student = new Student(txtStudentId.Text, txtFirstName.Text, txtLastName.Text, comboBoxDepartment.Text, enrollmentType);
                HelperClass.addStudent(student);
                this.Hide();
                var form = new MainWindow();
                form.LoadValuesinGrid();
                form.ShowDialog();

            }

        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtStudentId.Text = null;
            txtFirstName.Text = null;
            txtLastName.Text = null;
            radioButtonFullTime.Select();
            comboBoxDepartment.SelectedIndex = 0;

        }

        private void NewStudentForm_Load(object sender, EventArgs e)
        {

        }

        private void txtStudentID_TextChanged(object sender, EventArgs e)
        {
           // txtStudentID.Text = txtStudentID.Text.ToString("###-##-###");
          //  txtStudentID.Mask = "000-00-0000";
           
            
        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            btnAdd.Enabled = true;
            btnReset.Enabled = true;
            txtStudentId.Mask = "000-00-0000";
        }
    
    }
}
