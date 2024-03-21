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
        // Creating a context class to access database
        private SchoolDbContext School = new SchoolDbContext();

        //This Controller accesses the teachers table in the school database
        /// <summary>
        /// Returns a list of teachers and their data from the school database
        /// </summary>
        
        /// <returns>
        /// A list of data about the teachers (teacherid, first names and last names, employeenumber, hiredate, and salary)
        /// </returns>
        /// <param name="SearchKey"></param>
        /// <example>GET api/TeacherData/ListTeachers</example>
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

            //sql query
            string query = "Select * from teachers where teacherfname like @searchkey or teacherlname like @searchkey";

            command.CommandText = query;
            command.Parameters.AddWithValue("@searchkey","%"+SearchKey+"%");
            command.Prepare();

            //grouping results data into a variable
            MySqlDataReader DataResults = command.ExecuteReader();

            //creating a list of Teacher objects for the teachers data
            List<Teacher> TeacherData = new List<Teacher> { };

            //creating a loop to populate the list
            while (DataResults.Read())
            {
                //getting the teachers first names
                string FirstName = DataResults["teacherfname"].ToString();
                //getting the teachers last names
                string LastName = DataResults["teacherlname"].ToString();
                //getting the teachers id numbers
                int TeacherId = Convert.ToInt32(DataResults["teacherid"]);
                //getting the teachers employee numbers
                string EmployeeNumber = DataResults["employeenumber"].ToString();
                //getting the date the teachers were hired
                DateTime HireDate = (DateTime)DataResults["hiredate"];
                //getting the teachers salaries
                decimal TeacherSalary = Convert.ToDecimal(DataResults["salary"]);

                Teacher NewTeacher = new Teacher();
                NewTeacher.FirstName = FirstName;
                NewTeacher.LastName = LastName;
                NewTeacher.TeacherId = TeacherId;
                NewTeacher.EmployeeNumber = EmployeeNumber;
                NewTeacher.HireDate = HireDate;
                NewTeacher.Salary = TeacherSalary;


                TeacherData.Add(NewTeacher);
            }

            //closing database server connection
            Connection.Close();

            //returning the list of all the data
            
            return TeacherData;
        }



        //GOAL: Method that recives a TeacherId and returns info
        /// <summary>
        /// 
        /// </summary>
        /// <example>localhost:xxxx/api/teacherdata/findteacher/2--> {"teacherId":2,"firstName":"Caitlin","lastName":"Cummings","employeeNumber":"T381","hireDate":"2014-06-10T00:00:00","salary":62.77}</example>
        /// <param name="id"></param>
        /// <returns>A new instance of Teacher with the information about the teacher based in id input</returns>
        [HttpGet]
        [Route("api/teacherdata/findteacher/{id}")]
        public Teacher FindTeacher(int id)
        {
            Teacher SelectedTeacher = new Teacher();

            //Creating an instance of the database connection
            MySqlConnection Connection = School.AccessDatabase();

            //Opening the connection
            Connection.Open();

            //Creating a new sql command to apply to the database
            MySqlCommand command = Connection.CreateCommand();

            //sql query
            command.CommandText = "Select * from teachers where teacherid = "+id;

            //grouping results data into a variable
            MySqlDataReader TeacherResult = command.ExecuteReader();

            //creating a while loop to read through sql results

            while (TeacherResult.Read())
            {
                //accessing information through database column names
                //getting the teachers id number
                int TeacherId = Convert.ToInt32(TeacherResult["teacherid"]);
                //getting the teachers first name
                string FirstName = TeacherResult["teacherfname"].ToString();
                //getting the teachers last name
                string LastName = TeacherResult["teacherlname"].ToString();
                //getting the teachers employee number
                string EmployeeNumber = TeacherResult["employeenumber"].ToString();
                //getting the date the teacher was hired
                DateTime HireDate = (DateTime)TeacherResult["hiredate"];
                //getting the teachers salary
                decimal TeacherSalary = Convert.ToDecimal(TeacherResult["salary"]);

                SelectedTeacher.TeacherId = TeacherId;
                SelectedTeacher.FirstName = FirstName;
                SelectedTeacher.LastName = LastName;
                SelectedTeacher.EmployeeNumber = EmployeeNumber;
                SelectedTeacher.HireDate = HireDate;
                SelectedTeacher.Salary = TeacherSalary;
            }
            //closing database server connection
            Connection.Close();

            return SelectedTeacher;
        }
    }
}
