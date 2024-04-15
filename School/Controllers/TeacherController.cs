using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using School.Models;


namespace School.Controllers
{
    public class TeacherController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method navigates to and displays the teacher list view as a
        /// webpage and allows the user to search through a list of all the
        /// teachers in the system
        /// </summary>
        /// <param name="SearchKey">The term the user is using to to search as a
        /// string</param>
        /// <returns>
        /// A dynamically rendered webpage (/views/teacher/list.cshtml)
        /// displaying a search bar and a list of all teachers or the teachers
        /// whose names match the search key
        /// </returns>
        /// <example>
        /// GET: localhost:xxxx/Teacher/List --> the search bar above a list of
        /// all the teachers names as links
        /// </example>
        /// <example>
        /// GET: localhost:xxxx/Teacher/List?SearchKey=Ben --> the search bar
        /// and the name Alexander Bennett as a link to
        /// localhost:xxxx/Teacher/Show/1
        /// </example>
        [HttpGet]
        public ActionResult List(string SearchKey)
        {
           
            //Creating a new instance of the TeacherDataController
            TeacherDataController Controller = new TeacherDataController();

            //Connecting the Teacher model to the list and search functionality
            //in the TeacherDataController and assigning it to the variable
            //Teachers
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);

            //Creating a ViewResultObject and rendering it based on the
            //TeacherDataController and Teacher model
            return View(Teachers);
        }

        /// <summary>
        /// This method navigates to and displays a rendered webpage showing the
        /// view of a specific teacher based on teacherId
        /// </summary>
        /// <param name="id">An integer representing the unique teacherId number
        /// associated with each teacher</param>
        /// <returns>
        /// A dynamically rendered webpage (/views/teacher/show.cshtml)
        /// displaying information(firstName, lastName, hireDate, employeeNumber
        /// and salary) about a specific teacher based on the teachertId
        /// </returns>
        /// <example>
        /// GET: localhost:xxxx/Teacher/Show/5 --> web page displaying Jessica
        /// Morris Hired:6/4/2012 12:00:00 AM Employee Number:T389 Salary:$48.62
        /// </example>
        public ActionResult Show(int id)
        {
            //Creating a new instance of the TeacherDataController
            TeacherDataController Controller = new TeacherDataController();
            //Connecting the Teacher model to the find teacher functionality in
            //the TeacherDataController and assigning it to the variable
            //SelectedTeacher
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            //Creating a ViewResultObject and rendering it based on the
            //functionality the TeacherDataController and the Teacher model
            return View(SelectedTeacher);
        }


        /// <summary>
        /// Navigates to and displays a rendered webpage where a user can add a
        /// new teacher to the database
        /// </summary>
        /// <returns>
        /// A dynamically renered webpage (/view/teacher/new.cshtml) displaying
        /// a form containing inputs for each parameter in the teacher model
        /// </returns>
        /// <example>
        /// GET : localhost:xxxx/Teacher/New -> a webpage with a form for the
        /// user to input information about a new teacher
        /// </example>
        public ActionResult New()
        {
            //Navigate to and display /Views/Teacher/New.cshtml
            return View();
        }

        /// <summary>
        /// This method receives the information provided by the user in the
        /// form on the Teacher/New page and uses the AddTeacher method in the
        /// TeacherDataController to input the information into the school
        /// database
        /// </summary>
        /// <param name="FirstName">A string representing the first name of the
        /// new teacher</param>
        /// <param name="LastName">A string representing the last name of the
        /// new teacher</param>
        /// <param name="EmployeeNumber">A string representing the employee 
        /// number of the new teacher</param>
        /// <param name="Salary">A decimal representing the salary of the new
        /// teacher</param>
        /// <returns>
        /// Redirection to the page with a list of all teachers
        /// (/Views/Teacher/List.cshtml)
        /// </returns>
        /// <example>
        /// POST : localhost:xxxx/Teacher/Create -> redirects to the list
        /// teachers page
        /// </example>
        [HttpPost]
        public ActionResult Create(string FirstName, string LastName, string
            EmployeeNumber, decimal Salary)
        {
            //Checking to make sure the form information was received
            Debug.WriteLine("Form submission received");
            Debug.WriteLine(FirstName);
            Debug.WriteLine(LastName);

            //Creating a new instance of the Teacher model and assigning its
            //parameters to the variables
            Teacher NewTeacher = new Teacher();
            NewTeacher.FirstName = FirstName;
            NewTeacher.LastName = LastName;
            NewTeacher.EmployeeNumber = EmployeeNumber;
            NewTeacher.Salary = Salary;

            //Creating a new instance of the TeacherDataController 
            TeacherDataController TeacherController = new TeacherDataController();

            //Using the AddTeacher method to add the information to the database
            TeacherController.AddTeacher(NewTeacher);

            //Redirecting to the Teacher List View
            return RedirectToAction("List");
        }

        /// <summary>
        /// This method navigates to a webpage confirming that the user would like to remove the teacher they have selected
        /// </summary>
        /// <param name="id">An integer representing the id of the teacher
        /// selected to be removed</param>
        /// <returns>
        /// A dynamically rendered webpage(/Teacher/ConfirmRemove/{id}) displaying the name of the teacher selected by their id and 
        /// </returns>
        /// <example>
        /// GET : /Teacher/ConfirmRemove/8 --> webpage confirming that the user would like to remove Dana Ford
        /// </example>
        public ActionResult ConfirmRemove(int id)
        {
            // Retrieving teacher information with a new instance of the
            // TeacherDataController
            TeacherDataController DataController = new TeacherDataController();
            Teacher SelectedTeacher = DataController.FindTeacher(id);

            // Navigate to /Views/Teacher/ConfirmRemove.cshtml and displaying
            // the selected teacher to confirm they wish to remove from the
            // database
            return View(SelectedTeacher);
        }

        // POST : /Teacher/Remove/{id} --> redirects to list of teachers
        /// <summary>
        /// This method uses the RemoveTeacher method in the
        /// TeacherDataController to remove all information about the selected
        /// teacher from the school databse
        /// </summary>
        /// <param name="id">An integer representing the id of the teacher
        /// the user is deleteing</param>
        /// <returns>
        /// Redirection to the page with a list of all teachers
        /// (/Views/Teacher/List.cshtml)
        /// </returns>
        /// <example>
        /// POST : localhost:xxxx/Teacher/Remove/8 -> removes Dana Ford from the
        /// school database and directs to the list teachers page
        /// </example>
        public ActionResult Remove(int id)
        {
            //Checking to make sure the id has been received
            Debug.WriteLine(id);

            //Creating a new instance of the TeacherDataController
            TeacherDataController DataController = new TeacherDataController();

            //Using the RemoveTeacher method to remove the teacher with the selected id
            DataController.RemoveTeacher(id);

            //Redirecting to the Teacher List View
            return RedirectToAction("List");
        }

        // GET: /Teacher/Update/{TeacherId}

        public ActionResult Update(int id)
        {
            //Gathher information about the selected teacher
            TeacherDataController Controller = new TeacherDataController();

            Teacher SelectedTeacher = Controller.FindTeacher(id);

            //Present information to the user to update
            //Direct to /Views/Teacher/Update.cshtml
            return View(SelectedTeacher);
        }

        // POST: /Teacher/Edit/{id}
        [HttpPost]
        public ActionResult Edit(int id, string FirstName, string LastName, string
            EmployeeNumber, decimal Salary)
        {
            //Use data access component to update the teacher in the database

            Teacher UpdatedTeacher = new Teacher();
            UpdatedTeacher.FirstName = FirstName;
            UpdatedTeacher.LastName = LastName;
            UpdatedTeacher.EmployeeNumber = EmployeeNumber;
            UpdatedTeacher.Salary = Salary;

            TeacherDataController Controller = new TeacherDataController();
            Controller.UpdateTeacher(id, UpdatedTeacher);

            //Redirect to /Teacher/Show/{id}
            return Redirect("/Teacher/Show/"+id);
        }
    }
}

