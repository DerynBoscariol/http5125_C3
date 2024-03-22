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
    public class StudentController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// This method navigates to and displays the student list view as a
        /// webpage and allows the user  to search through a list of all the
        /// students in the system
        /// </summary>
        /// <param name="SearchKey">The term the user is using to to search as a
        /// string</param>
        /// <returns>
        /// A dynamically rendered webpage (/views/student/list.cshtml)
        /// displaying a search bar and a list of all students or the students
        /// whose names match the search key
        /// </returns>
        /// <example>
        /// GET: localhost:xxxx/Student/List --> the search bar above a list of
        /// all the students names as links
        /// </example>
        /// <example>
        /// GET: localhost:xxxx/Student/List?SearchKey=Roy--> the search bar
        /// and the name Roy Davidson as a link to
        /// localhost:xxxx/Student/Show/14
        /// </example>
        [HttpGet]
        public ActionResult List(string SearchKey)
        {

            //creating a neww instance of the StudentDataController
            StudentDataController Controller = new StudentDataController();

            //connecting the Student model to the list and search functionality
            //in the StudentDataController and assigning it to the variable
            //Students
            IEnumerable<Student> Students = Controller.ListStudents(SearchKey);

            //creating a ViewResultObject and rendering it based on the
            //StudentDataController and Student model
            return View(Students);
        }

        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student SelectedStudent = Controller.FindStudent(id);
            
            return View(SelectedStudent);
        }

    }
}

