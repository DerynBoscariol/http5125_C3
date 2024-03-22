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
        /// GET: localhost:xxxx/Student/List?SearchKey=Roy--> web page
        /// displaying the search bar and the name Roy Davidson as a link to
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
            //functionality in StudentDataController and the Student model
            return View(Students);
        }
        /// <summary>
        /// This method navigates to and displays a rendered webpage show the
        /// view of a specific student based on studentId
        /// </summary>
        /// <param name="id">An integer representing the unique studentId number
        /// associated with each student</param>
        /// <returns>
        /// A dynamically rendered webpage (/views/student/show.cshtml)
        /// displaying information(firstName, lastName, studentId, enrollDate,
        /// studentNumber) about a specific student based on studentId
        /// </returns>
        /// <example>
        /// GET: localhost:xxxx/Student/Show/7 --> web page displaying Jason
        /// Freeman Student Id: 7 Enrolled: 8/16/2018 12:00:00 AM
        /// Student Number: N1694
        /// </example>
        public ActionResult Show(int id)
        {
            //creating a new instance of the StudentDataController
            StudentDataController Controller = new StudentDataController();

            //connecting the Student model to the find student functionality in
            //the StudentDataController and assigning it to the variable
            //SelectedStudent
            Student SelectedStudent = Controller.FindStudent(id);

            //creating a ViewResultObject and rendering it based on the
            //functionality the StudentDataController and the Student model
            return View(SelectedStudent);
        }

    }
}

