using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using School.Models;
using MySql.Data.MySqlClient;
using System.Reflection.Metadata;
using System.Diagnostics;


namespace School.Controllers
{
    
    [ApiController]
    public class TeacherDataController : ControllerBase
    {
        // Creating a context class to access the database
        private SchoolDbContext School = new SchoolDbContext();


        /// <summary>
        /// This method displays information (teacherId, firstName, lastName,
        /// employeeNumber, hireDate, and salary) about one or many teacher(s)
        /// based on the string input as the search key
        /// </summary>
        /// <returns>
        /// Information about the teacher(s) matching the search key
        /// </returns>
        /// <param name="SearchKey">The term the user wants to search for as a
        /// string</param>
        /// <example>
        /// GET localhost:xxxx/api/TeacherData/ListTeachers/Lin -> [{"teacherId"
        /// :2,"firstName":"Caitlin","lastName":"Cummings","employeeNumber":
        /// "T381","hireDate":"2014-06-10T00:00:00","salary":62.77},{"teacherId"
        /// :3,"firstName": "Linda","lastName":"Chan","employeeNumber":"T382",
        /// "hireDate":"2015-08-22T00:00:00","salary":60.22}]
        /// </example>
        [HttpGet]
        [Route("api/TeacherData/ListTeachers/{SearchKey}")]
        public List<Teacher> ListTeachers(string SearchKey)
        {
            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new sql command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //SQL query
            string query = "Select * from teachers where teacherfname like @searchkey or teacherlname like @searchkey or salary like @searchkey";
            command.CommandText = query;

            //Altering the SearchKey variable so that no character in the
            //search key will be mistaken as part of the query
            command.Parameters.AddWithValue("@searchkey","%"+SearchKey+"%");
            command.Prepare();

            //Grouping results data into a variable
            MySqlDataReader DataResults = command.ExecuteReader();

            //Creating a list of Teacher objects to store the teachers data
            List<Teacher> TeacherData = new List<Teacher> { };

            //Creating a loop to populate the list
            while (DataResults.Read())
            {
                //Getting the teachers first names
                string FirstName = DataResults["teacherfname"].ToString();
                //Getting the teachers last names
                string LastName = DataResults["teacherlname"].ToString();
                //Getting the teachers id numbers
                int TeacherId = Convert.ToInt32(DataResults["teacherid"]);
                //Getting the teachers employee numbers
                string EmployeeNumber = DataResults["employeenumber"].ToString();
                //Getting the date the teachers were hired
                DateTime HireDate = (DateTime)DataResults["hiredate"];
                //Getting the teachers salaries
                decimal TeacherSalary = Convert.ToDecimal(DataResults["salary"]);

                //Creating a new instance of the Teacher model
                Teacher NewTeacher = new Teacher();

                //Assiging variables to the properties of the new Teacher model
                NewTeacher.FirstName = FirstName;
                NewTeacher.LastName = LastName;
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = TeacherSalary;

                //Adding the newTeacher object(s) to the list
                TeacherData.Add(NewTeacher);
            }

            //Closing the database server connection
            Connection.Close();

            //Returning the list of all the data
            return TeacherData;
        }


        /// <summary>
        /// This method recieves an id number and will display information
        /// (teacherId, firstName, lastName, employeeNumber, hireDate, and
        /// salary) about a teacher based on that id
        /// </summary>
        /// <example>
        /// GET localhost:xxxx/api/teacherdata/findteacher/6--> {"teacherId":6,
        /// "firstName":"Thomas","lastName":"Hawkins","employeeNumber":"T393",
        /// "hireDate":"2016-08-10T00:00:00","salary":54.45}
        /// </example>
        /// <param name="id">An integer representing the id number of the
        /// teacher being searched for</param>
        /// <returns>A new instance of Teacher with the information about the
        /// teacher based on id input</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            //Creating a new instance of the Teacher model
            Teacher SelectedTeacher = new Teacher();

            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new SQL command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //SQL query
            command.CommandText = "Select * from teachers where teacherid = "+id;

            //Grouping results data into a variable
            MySqlDataReader TeacherResult = command.ExecuteReader();

            //Creating a while loop to read through SQL results

            while (TeacherResult.Read())
            {
             //Accessing information through database column names:
                //Getting the teachers id number
                int TeacherId = Convert.ToInt32(TeacherResult["teacherid"]);
                //Getting the teachers first name
                string FirstName = TeacherResult["teacherfname"].ToString();
                //Getting the teachers last name
                string LastName = TeacherResult["teacherlname"].ToString();
                //Getting the teachers employee number
                string EmployeeNumber = TeacherResult["employeenumber"].ToString();
                //Getting the date the teacher was hired
                DateTime HireDate = (DateTime)TeacherResult["hiredate"];
                //Getting the teachers salary
                decimal TeacherSalary = Convert.ToDecimal(TeacherResult["salary"]);

                //Assigning the variables to the properties of the Teacher model
                SelectedTeacher.TeacherId = TeacherId;
                SelectedTeacher.FirstName = FirstName;
                SelectedTeacher.LastName = LastName;
                SelectedTeacher.EmployeeNumber = EmployeeNumber;
                SelectedTeacher.HireDate = HireDate;
                SelectedTeacher.Salary = TeacherSalary;
            }
            //Closing the database server connection
            Connection.Close();

            //Returning an instance of the Teacher model populated with the data
            //from the teacher searched for
            return SelectedTeacher;
        }

        //Add Teacher Method
        /// <summary>
        /// Recieves Teacher information and adds it to the database
        /// </summary>
        /// <returns>
        /// concated success message
        /// </returns>
        /// <example>
        /// POST localhost:xxxx/api/teacherdata/addteacher
        /// FORM DATA / POST DATA / REQUEST BODY
        /// {
        ///     "FirstName" : "Alex"
        ///     "LastName" : "Turner"
        ///     "EmployeeNumber" : "T365"
        ///     "HireDate" : "2024-04-03 00:00:00"
        ///     "Salary" : "57.83"
        /// }
        /// </example>
        [HttpPost]
        [Route("api/teacherdata/AddTeacher")]
        public string AddTeacher([FromBody]Teacher NewTeacher)
        {
            //Creating an instance of the database connection and opening it
            MySqlConnection Connect = School.AccessDatabase();
            Connect.Open();

            //Creating a command to apply to the database
            MySqlCommand Command = Connect.CreateCommand();

            //SQL query
            string query = "insert into teachers (teacherfname, teacherlname, " +
                "employeenumber, hiredate, salary) values(@FirstName,@LastName," +
                "@EmployeeNumber,CURRENT_DATE(),@Salary)";
            Command.CommandText = query;

            //Adding each parameter with the value
            Command.Parameters.AddWithValue
                ("@FirstName", NewTeacher.FirstName);
            Command.Parameters.AddWithValue
                ("@LastName", NewTeacher.LastName);
            Command.Parameters.AddWithValue
                ("@EmployeeNumber", NewTeacher.EmployeeNumber);
            Command.Parameters.AddWithValue
                ("@Salary", NewTeacher.Salary);
            Command.Prepare();

            //Execute query
            Command.ExecuteNonQuery();

            //Closing the server database connection
            Connect.Close();

            //Returning concated success statement
            return "A new teacher has been added to the database under the name"
                +NewTeacher.FirstName + " " +NewTeacher.LastName;
        }

        /// <summary>
        /// Recieves a teacher id and deletes all corresponding data in the
        /// database
        /// </summary>
        /// <param name="TeacherId">The id of the teacher the user wamts to remove</param>
        /// <returns>
        /// Success message
        /// </returns>
        /// <example>
        /// POST : api/teacherdata/removeteacher/11 --> "Teacher has been
        /// successfully removed"
        /// </example>
        [HttpPost]
        [Route("api/TeacherData/RemoveTeacher/{TeacherId}")]
        public string RemoveTeacher(int TeacherId)
        {

            //Creating a connection between the server and database and opening that connection
            MySqlConnection Connect = School.AccessDatabase();
            Connect.Open();
            
            //Creating an SQL command
            string query = "delete from teachers where teacherid=@teacherid";
            MySqlCommand Command = Connect.CreateCommand();
            Command.CommandText = query;

            //Adding TeacherId parameter with value
            Command.Parameters.AddWithValue("@teacherid", TeacherId);

            //Preparing and executing the command
            Command.Prepare();
            Command.ExecuteNonQuery();

            //Closing the server database connection
            Connect.Close();

            //Returning a success statement
            return "Teacher has been successfully removed";
        }

    }
}
