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

        //GET: localhost:xxxx/Student/List -->dynamically rendered webpage
        [HttpGet]
        public ActionResult List(string SearchKey)
        {

            // Navigating to /views/Student/list.cshtml
            StudentDataController Controller = new StudentDataController();
            IEnumerable<Student> Students = Controller.ListStudents(SearchKey);

            return View(Students);
        }

        public ActionResult Show(int id)
        {
            StudentDataController Controller = new StudentDataController();
            Student SelectedStudent = Controller.FindStudent(id);
            //should navigate to /views/teacher/show.cshtml
            return View(SelectedStudent);
        }

    }
}

