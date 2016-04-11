using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentRegistrationWPF
{
    class Helper
    {
        public static List<Student> listOfStudents = new List<Student>();
        public static StudentRegistrationMain form;

        public static StudentRegistrationMain instantiateForm()
        {
            form = new StudentRegistrationMain();
            return form;
        }
        public static StudentRegistrationMain getForm()
        {
            return form;
        }

        public static bool addStudent(Student s)
        {
            listOfStudents.Add(s);
            return true;
        }

        public static bool removeStudent(Student s)
        {
            listOfStudents.RemoveAll(stu => stu.StudentID == s.StudentID);
            return true;
        }

        public static String[] DepartmentNames = new[] { "Information Systems", "International Affairs", "Nursing", "Pharmacy",
                "Professional Studies", "Psychology", "Public Administration" };
        public static String[] EnrollmentType = new[] { "FullTime", "PartTime" };


        public static List<Student> generateRecords()
        {

            string[] names = new string[15] { "Chandler", "abdul", "kyle", "Camaron", "Rachel", "adam", "Ross", "Monica", "Adrian", "Purna", "Haily", "James", "Joanne", "Earl", "Emily" };

            string[] lastNames = new string[10] { "Bing", "Greene", "Scott", "Rimesr", "Geller", "Buffay", "Camreron", "Jade", "Johnson", "Perry" };

            Random rnd = new Random();
            List<Student> records = new List<Student>();
            for (int i = 0; i < 10; ++i)
            {
                Student s = new Student();

                int myRandomNo = rnd.Next(100000000, 999999990);
                s.StudentID = myRandomNo.ToString("###-##-###0");
                s.FirstName = names[rnd.Next(0, names.Length - 1)];
                s.LastName = lastNames[rnd.Next(0, lastNames.Length - 1)];
                s.Department = DepartmentNames[rnd.Next(0, DepartmentNames.Length - 1)];
                s.EnrollmentType = EnrollmentType[rnd.Next(0, 2)];
                records.Add(s);
                listOfStudents.Add(s);

            }

            return records;
        }

        public static bool validateStudentID(String id)
        {

            if (String.IsNullOrEmpty(id))
                return false;

            string pattern = @"^\d{3}-\d{2}-\d{4}$";
            return Regex.IsMatch(id, pattern);
        }

        public static bool ValidateName(string userInputName)
        {
            if (String.IsNullOrEmpty(userInputName))
                return false;

            string pattern = @"^[a-zA-Z]+$";
            return Regex.IsMatch(userInputName, pattern);
        }
    }
}

