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
        /// webpage and allows the user  to search through a list of all the
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
           
            //creating a new instance of the TeacherDataController
            TeacherDataController Controller = new TeacherDataController();

            //connecting the Teacher model to the list and search functionality
            //in the TeacherDataController and assigning it to the variable
            //Teachers
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);

            //creating a ViewResultObject and rendering it based on the
            //TeacherDataController and Teacher model
            return View(Teachers);
        }

        public ActionResult Show(int id)
        {
            TeacherDataController Controller = new TeacherDataController();
            Teacher SelectedTeacher = Controller.FindTeacher(id);
            //should navigate to /views/teacher/show.cshtml
            return View(SelectedTeacher);
        }

    }
}

