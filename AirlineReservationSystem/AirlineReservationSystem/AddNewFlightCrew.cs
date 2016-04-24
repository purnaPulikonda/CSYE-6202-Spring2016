using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class AddNewFlightCrew : Form
    {
        List<Employee> listOfEmployee = new List<Employee>();
        private static ILog logger = LogManager.GetLogger(typeof(AddNewFlightCrew));
        public AddNewFlightCrew()
        {
            InitializeComponent();
            logger.Info("in the add flight crew screen");
            loadEmployees();

        }

        public void loadEmployees()
        {
            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
              ("ConnString");

            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {
                conn.Open();

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select * from employee where crewid is NULL";
                try
                {
                    var reader1 = cmdOne.ExecuteReader();
                    DataTable dtRecord = new DataTable();
                    dtRecord.Load(reader1);
                    dataGridView1.DataSource = dtRecord;
                }
                catch (SqlException ex) {
                    logger.Error(ex);
                }
                conn.Close();

            }
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = getEmployee();
            listOfEmployee.Add(emp);

            var bindingList = new BindingList<Employee>(listOfEmployee);
            var source = new BindingSource(bindingList, null);

            dataGridView2.DataSource = source;
        }

        public Employee getEmployee()
        {
            Employee e = new Employee();
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                e.empId = (int)row.Cells[0].Value;
                e.name = row.Cells[1].Value.ToString();
                e.address = row.Cells[2].Value.ToString();

                e.phoneNumber = row.Cells[3].Value.ToString();
                e.emailId = row.Cells[4].Value.ToString();

            }
            return e;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (validateAddCrew())
            {
                txtCrewID.BackColor = Color.White;
                FlightCrewDAL fdal = new FlightCrewDAL();
                fdal.openConnection();
                fdal.createNewCrew(txtCrewID.Text, listOfEmployee);
                fdal.closeConnection();

                MessageBox.Show("Successfully Added Flight Crew", "Confirmation", MessageBoxButtons.OK);
                logger.Debug("successfully added flight crew" + txtCrewID.Text);
                this.Close();
                ShowAllFLightCrew amm = new ShowAllFLightCrew();
                amm.Show();

            }
            else {
                MessageBox.Show("Enter crew ID","Moew Info needed",MessageBoxButtons.OK);
                txtCrewID.BackColor = Color.Yellow;
            }
        }

        private bool validateAddCrew()
        {
            if (txtCrewID.Text == null) return false;
            else return true;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminMainMenu amm = new AdminMainMenu();
            amm.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllFLightCrew s = new ShowAllFLightCrew();
            s.Show();
        }
    }
}