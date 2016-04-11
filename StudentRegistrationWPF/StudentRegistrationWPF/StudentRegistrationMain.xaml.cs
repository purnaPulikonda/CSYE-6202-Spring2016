using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;


namespace StudentRegistrationWPF
{
    /// <summary>
    /// Interaction logic for StudentRegistrationMain.xaml
    /// </summary>
    public partial class StudentRegistrationMain : Window
    {
        public StudentRegistrationMain()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            Helper.generateRecords();
            LoadDepartments();
            LoadValuesinGrid();
            loadDefaults();
        }

        public void loadDefaults() {
            txtStudentId.IsEnabled = false;
            txtFirstName.IsEnabled = false;
            txtLastName.IsEnabled = false;
            comboBoxDepartment.IsEnabled = false;
            radioButtonFullTime.IsEnabled = false;
            radioButtonPartTime.IsEnabled = false;
        }

        public void LoadDepartments()
        {
            comboBoxDepartment.Items.Add("Information Systems");
            comboBoxDepartment.Items.Add("International Affairs");
            comboBoxDepartment.Items.Add("Nursing");
            comboBoxDepartment.Items.Add("Pharmacy");
            comboBoxDepartment.Items.Add("Professional Studies");
            comboBoxDepartment.Items.Add("Psychology");
            comboBoxDepartment.Items.Add("Public Administration"); 
        }

        public void LoadValuesinGrid()
        {
            // dirty workaround to make sure that we can bind to the static mock list

            //   var bindingList = new BindingList<Student>(Helper.listOfStudents);
            //  var source = new BindingSource(bindingList, null);
           // dataGridStudent.ItemsSource = null;
            Console.WriteLine("Elements in lofs"+Helper.listOfStudents.Count);
            //   dataGridStudent.ItemsSource = null;

            var bindingList = new ObservableCollection<Student>(Helper.listOfStudents);
          //  var source = new BindingSource(bindingList, null);

            dataGridStudent.ItemsSource = bindingList;
            object item = dataGridStudent.Items[0];
            dataGridStudent.SelectedItem = item;
            dataGridStudent.ScrollIntoView(item);
        }

        private Student getSelectedStudent()
        {
            Student s = (Student) dataGridStudent.SelectedItem;

            return s;
        
        }

        private void btnRemoveStudent_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult dr = MessageBox.Show("Are you sure you want to remove this student?", "Remove student registration", MessageBoxButton.YesNo);
            if (dr == MessageBoxResult.Yes)
            {
                Student s = getSelectedStudent();
                Helper.removeStudent(s);
                LoadValuesinGrid();
            }
        }

        private void dataGridStudent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(dataGridStudent.SelectedItem);
            try
            {
                Student student = (Student)dataGridStudent.SelectedItem;
                //  DataRowView rowview = (DataRowView)dataGridStudent.SelectedItem;

                if (student == null)
                {
                    student = (Student)dataGridStudent.Items[0];
                }
                txtStudentId.Text = student.StudentID;
                txtFirstName.Text = student.FirstName;
                txtLastName.Text = student.LastName;

                comboBoxDepartment.SelectedIndex = comboBoxDepartment.Items.IndexOf(student.Department);

                Console.WriteLine("index of info" + comboBoxDepartment.Items.IndexOf("Information Systems"));

                //  Console.WriteLine(comboBoxDepartment.);

                // enrollment type selection driven by the grid itself
                string enrollmentType = student.EnrollmentType;
                if (enrollmentType == "FullTime")
                {
                    radioButtonFullTime.IsChecked = true;
                }
                else if ("PartTime" == enrollmentType)
                {
                    radioButtonPartTime.IsChecked = true;
                }
            } catch (Exception ex) { Console.WriteLine(ex); }
            }

        private void btnAddStudent_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            var form1 = new NewStudentRegistration();
           form1.ShowDialog();
        }

        private void btnEditStudent_Click(object sender, RoutedEventArgs e)
        {
            Student s = getSelectedStudent();
            var form = new EditStudentRegistration(s);
            form.ShowDialog();
        }

        private void btnRemoveStudent_Click_1(object sender, RoutedEventArgs e)
        {
            
            MessageBoxResult dr = MessageBox.Show("Are you sure you want to remove this student?", "Remove student registration", MessageBoxButton.YesNo);
            if (dr == MessageBoxResult.Yes)
            {
                Student s = getSelectedStudent();
                Helper.removeStudent(s);
                dataGridStudent.Items.Refresh();
                LoadValuesinGrid();
            }
        }
    }
    }

