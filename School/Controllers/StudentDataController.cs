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
        // Creating a context class to access the database
        private SchoolDbContext School = new SchoolDbContext();

        /// <summary>
        /// This method displays information (studentId, firstName, lastName,
        /// studentNumber, and enrollDate) about one or many student(s) based on
        /// the string entered
        /// </summary>
        /// <returns>
        /// Information about the student(s) matching the search key
        /// </returns>
        /// <param name="SearchKey">The term the user wants to search for as a
        /// string</param>
        /// <example>
        /// GET localhost:xxxx/api/StudentData/ListStudents/Ma--> [{"studentId":
        /// 4,"firstName":"Mario","lastName":"English","studentNumber":"N1686",
        /// "enrollDate":"7/3/2018 12:00:00 AM"},{"studentId":7,"firstName":
        /// "Jason","lastName":"Freeman","studentNumber":"N1694","enrollDate":
        /// "8/16/2018 12:00:00 AM"}]
        /// </example>
        [HttpGet]
        [Route("api/StudentData/ListStudents/{SearchKey}")]
        public List<Student> ListStudents(string SearchKey)
        {
            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new SQL command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //SQL query
            string query = "Select * from students where studentfname like @searchkey or studentlname like @searchkey ";
            command.CommandText = query;

            //Altering the SearchKey variable so that no character in the
            //search key will be mistaken as part of the query
            command.Parameters.AddWithValue("@searchkey", "%" + SearchKey + "%");
            command.Prepare();

            //Grouping results data into a variable
            MySqlDataReader DataResults = command.ExecuteReader();

            //Creating a list of Student objects to store the students data
            List<Student> StudentData = new List<Student> { };

            //Creating a loop to populate the list
            while (DataResults.Read())
            {
                //Getting the students first names
                string? FirstName = DataResults["studentfname"].ToString();
                //Getting the students last names
                string? LastName = DataResults["studentlname"].ToString();
                //Getting the students id numbers
                int StudentId = Convert.ToInt32(DataResults["studentid"]);
                //Getting the students student numbers
                string? StudentNumber = DataResults["studentnumber"].ToString();
                //Getting the date the students were enrolled
                string? EnrollDate = Convert.ToString(DataResults["enroldate"]);

                //Creating a new instance of the Student model
                Student NewStudent = new Student();

                //Assigning variables to the properties of the new Student model
                NewStudent.FirstName = FirstName;
                NewStudent.LastName = LastName;
                NewStudent.StudentId = StudentId;
                NewStudent.StudentNumber = StudentNumber;
                NewStudent.EnrollDate = EnrollDate;

                //Adding the newStudent object(s) to the list
                StudentData.Add(NewStudent);
            }

            //Closing the database server connection
            Connection.Close();

            //Returning the list of all the data
            return StudentData;
        }



        /// <summary>
        /// This method recieves an id number and will display information
        /// (studentId, firstName, lastName, studentNumber, and enrollDate)
        /// about a student based on that id
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
            //Creating a new instance of the Student model
            Student SelectedStudent = new Student();

            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new SQL command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //SQL query
            command.CommandText = "Select * from students where studentid = " + id;

            //Grouping results data into a variable
            MySqlDataReader StudentResult = command.ExecuteReader();

            //creating a while loop to read through sql results
            while (StudentResult.Read())
            {
                //Accessing information through database column names
                //Getting the students id number
                int StudentId = Convert.ToInt32(StudentResult["studentid"]);
                //Getting the students first name
                string? FirstName = StudentResult["studentfname"].ToString();
                //Getting the students last name
                string? LastName = StudentResult["studentlname"].ToString();
                //Getting the students student number
                string? StudentNumber = StudentResult["studentnumber"].ToString();
                //Getting the date the student was enrolled
                string? EnrollDate = Convert.ToString(StudentResult["enroldate"]);

                //Assigning the variables to the corresponding properties of the
                //Student model
                SelectedStudent.StudentId = StudentId;
                SelectedStudent.FirstName = FirstName;
                SelectedStudent.LastName = LastName;
                SelectedStudent.StudentNumber = StudentNumber;
                SelectedStudent.EnrollDate = EnrollDate;
            }
            //Closing the database server connection
            Connection.Close();

            //Returning the new instance of the Student model populated with the
            //information from the student being searched for
            return SelectedStudent;
        }
    }
}