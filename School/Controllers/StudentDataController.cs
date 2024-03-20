using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata;


namespace School.Controllers
{

    [ApiController]
    public class StudentDataController : ControllerBase
    {
        // Creating a context class to access database
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller accesses the teachers table in the school database
        /// <summary>
        /// Returns a list of teachers and their data from the school database
        /// </summary>
        /// <example>GET api/TeacherData/ListTeachers</example>
        /// <returns>
        /// A list of data about the teachers (teacherid, first names and last names, employeenumber, hiredate, and salary)
        /// </returns>
        [HttpGet]
        [Route("api/StudentData/ListStudents")]
        public List<Student> ListStudents()
        {
            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new sql command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //sql query
            command.CommandText = "Select * from students";

            //grouping results data into a variable
            MySqlDataReader DataResults = command.ExecuteReader();

            //creating a list of Teacher objects for the teachers data
            List<Student> StudentData = new List<Student> { };

            //creating a loop to populate the list
            while (DataResults.Read())
            {
                //getting the teachers first names
                string? FirstName = DataResults["studentfname"].ToString();
                //getting the teachers last names
                string? LastName = DataResults["studentlname"].ToString();
                //getting the teachers id numbers
                int StudentId = Convert.ToInt32(DataResults["studentid"]);
                //getting the teachers employee numbers
                string? StudentNumber = DataResults["studentnumber"].ToString();
                //getting the date the teachers were hired
                string? EnrollDate = Convert.ToString(DataResults["enroldate"]);

                Student NewStudent = new Student();
                NewStudent.FirstName = FirstName;
                NewStudent.LastName = LastName;
                NewStudent.StudentId = StudentId;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;


                StudentData.Add(NewStudent);
            }

            //closing database server connection
            Connection.Close();

            //returning the list of all the data

            return StudentData;
        }



        //GOAL: Method that recives a TeacherId and returns info
        /// <summary>
        /// 
        /// </summary>
        /// <example>localhost:xxxx/api/teacherdata/findteacher/2--> {"teacherId":2,"firstName":"Caitlin","lastName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77}</example>
        /// <param name="id"></param>
        /// <returns>A new instance of Teacher with the information about the teacher based in id input</returns>
        [HttpGet]
        [Route("api/studentdata/findstudent/{id}")]
        public Student FindStudent(int id)
        {
            Student SelectedStudent = new Student();

            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new sql command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //sql query
            command.CommandText = "Select * from students where studentid = " + id;

            //grouping results data into a variable
            MySqlDataReader StudentResult = command.ExecuteReader();

            //creating a while loop to read through sql results

            while (StudentResult.Read())
            {
                //accessing information through database column names
                //getting the teachers id number
                int StudentId = Convert.ToInt32(StudentResult["studentid"]);
                //getting the teachers first name
                string? FirstName = StudentResult["studentfname"].ToString();
                //getting the teachers last name
                string? LastName = StudentResult["studentlname"].ToString();
                //getting the teachers employee number
                string? StudentNumber = StudentResult["studentnumber"].ToString();
                //getting the date the teacher was hired
                string? EnrollDate = Convert.ToString(StudentResult["enroldate"]);

                SelectedStudent.StudentId = StudentId;
                SelectedStudent.FirstName = FirstName;
                SelectedStudent.LastName = LastName;
                SelectedStudent.StudentNumber = StudentNumber;
                SelectedStudent.EnrollDate = EnrollDate;
            }
            //closing database server connection
            Connection.Close();

            return SelectedStudent;
        }
    }
}