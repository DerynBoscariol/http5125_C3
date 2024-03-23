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

    }
}

