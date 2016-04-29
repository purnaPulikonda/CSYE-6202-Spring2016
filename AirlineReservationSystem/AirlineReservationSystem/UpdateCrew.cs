using AirlineRegistration.Dao;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AirlineReservationSystem
{
    public partial class UpdateCrew : Form
    {
        FlightCrew flightCrew;
        List<Employee> listOfEmployee = new List<Employee>();
        private static ILog logger = LogManager.GetLogger(typeof(UpdateCrew));
        public UpdateCrew(FlightCrew flightCrew)
        {
            InitializeComponent();
            logger.Info("IN update crew screen");
            this.flightCrew = flightCrew;
            loadCrew();
        }

        private void loadCrew()
        {
            txtCrewID.Text = flightCrew.crewId.ToString();
            txtCrewID.Enabled = false;
            loadCurrentEmployee();
            loadAllEmployees();
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
        private void loadAllEmployees()
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
                catch (SqlException ex)
                {
                    logger.Error(ex);
                }
                conn.Close();

            }
        }

        private bool validateAddCrew()
        {
            if (txtCrewID.Text == null) return false;
            else return true;
        }
        private void loadCurrentEmployee()
        {
            DataTable dt = new DataTable();
            String SQLConnectionString = Helper.GetConnectionStringFromExeConfig
            ("ConnString");


            // Create an SqlConnection from the provided connection string.
            using (SqlConnection conn = new SqlConnection(SQLConnectionString))
            {

                var cmdOne = conn.CreateCommand();

                cmdOne.CommandText = @"
                Select * from employee 
                WHERE  crewId =@crewId";

                cmdOne.Parameters.AddWithValue("@crewId", flightCrew.crewId);
                conn.Open();

                using (var reader = cmdOne.ExecuteReader())
                {

                    dt.Load(reader);
                    dataGridView2.DataSource = dt;
                }
            }


            if (dt.Rows.Count > 0)
            {
                dataGridView2.Columns[0].HeaderText = "EmployeeId ";
                dataGridView2.Columns[1].HeaderText = "Employee Name";
                dataGridView2.Columns[2].HeaderText = "Address";
                dataGridView2.Columns[3].HeaderText = "Phone";
                dataGridView2.Columns[4].HeaderText = "Email";
                dataGridView2.Columns[5].Visible = false;
            }
            else {
                logger.Error("The crew details not loaded");
            }
            
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            int result= 0;
          
               
                FlightCrewDAL fdal = new FlightCrewDAL();
            try
            {
                fdal.openConnection();
                result = fdal.updateCrew(txtCrewID.Text, listOfEmployee);
            }
            catch (SqlException ex)
            {
                logger.Error(ex);
            }
            finally
            {
                fdal.closeConnection();
            }

                MessageBox.Show("Successfully updated Flight Crew", "Confirmation", MessageBoxButtons.OK);
                logger.Debug("successfully updated flight crew" + txtCrewID.Text);
                this.Close();
                ShowAllFLightCrew amm = new ShowAllFLightCrew();
                amm.Show();

            
           
        }

        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            Employee emp = getEmployee();
            listOfEmployee.Add(emp);

            var bindingList = new BindingList<Employee>(listOfEmployee);
            var source = new BindingSource(bindingList, null);

            dataGridView2.DataSource = source;
        }

        private void toolStripLabel1_Click(object sender, EventArgs e)
        {
            this.Close();
            AdminMainMenu amm = new AdminMainMenu();
            amm.Show();
        }

        private void toolStripLabel3_Click(object sender, EventArgs e)
        {
            this.Close();
            ShowAllCArriers sac = new ShowAllCArriers();
            sac.Show();
        }

        private void toolStripLabel2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
