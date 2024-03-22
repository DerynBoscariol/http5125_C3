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

        ///<summary>
        ///This method displays information(studentId, firstName, lastName,
        ///studentNumber, and enrollDate) about one or many student(s)
        ///based on the string entered
        ///</summary>
        /// <returns>
        /// Information about the student(s) matching the search key
        /// </returns>
        /// <param name="SearchKey">The term the user wants to search for as a
        /// string</param>
        /// <example>GET api/StudentData/ListStudents/Ma--> [{"studentId":4,
        /// "firstName":"Mario","lastName":"English","studentNumber":"N1686",
        /// "enrollDate":"7/3/2018 12:00:00 AM"},{"studentId":7,"firstName":
        /// "Jason","lastName":"Freeman","studentNumber":"N1694","enrollDate":
        /// "8/16/2018 12:00:00 AM"}]</example>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey}")]
        public List<Student> ListStudents(string SearchKey)
        {
            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new sql command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //sql query
            string query = "Select * from students where studentfname like @searchkey or studentlname like @searchkey";
            command.CommandText = query;

            //altering the SearchKey variable so that no character in the
            //SearchKey will be mistaken as part of the query
            command.Parameters.AddWithValue("@searchkey", "%" + SearchKey + "%");
            command.Prepare();

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

                //creating a new instance of the Student model
                Student NewStudent = new Student();

                //assigning variables to the properties of the new Student model
                NewStudent.FirstName = FirstName;
                NewStudent.LastName = LastName;
                NewStudent.StudentId = StudentId;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;

                //adding the newStudent object(s) to the list
                StudentData.Add(NewStudent);
            }

            //closing database server connection
            Connection.Close();

            //returning the list of all the data
            return StudentData;
        }



        ///<summary>
        /// This method recieves an id number and will display information
        /// (studentId, firstName, lastName, studentNumber, and enrollDate) on
        /// a student based on that id
        /// </summary>
        /// <example>
        /// GET localhost:xxxx/api/studentdata/findstudent/8--> {"studentId":8,
        /// "firstName":"Nicole","lastName":"Armstrong","studentNumber":"N1698",
        /// "enrollDate":"7/10/2018 12:00:00 AM"}
        /// </example>
        /// <param name="id">An integer representing the id number of the
        /// student being searched for</param>
        /// <returns>A new instance of the Student model with the information
        /// about the student based on id input</returns>
        [HttpGet]
        [Route("api/studentdata/findstudent/{id}")]
        public Student FindStudent(int id)
        {
            //creating a new instance of the Student model
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
                //getting the students id number
                int StudentId = Convert.ToInt32(StudentResult["studentid"]);
                //getting the students first name
                string? FirstName = StudentResult["studentfname"].ToString();
                //getting the students last name
                string? LastName = StudentResult["studentlname"].ToString();
                //getting the students student number
                string? StudentNumber = StudentResult["studentnumber"].ToString();
                //getting the date the student was enrolled
                string? EnrollDate = Convert.ToString(StudentResult["enroldate"]);

                //assigning the variables to the corresponding property of the
                //Student model
                SelectedStudent.StudentId = StudentId;
                SelectedStudent.FirstName = FirstName;
                SelectedStudent.LastName = LastName;
                SelectedStudent.StudentNumber = StudentNumber;
                SelectedStudent.EnrollDate = EnrollDate;
            }
            //closing database server connection
            Connection.Close();

            //returning the new instance of the Student model populated with the
            //information from the student being searched for
            return SelectedStudent;
        }
    }
}