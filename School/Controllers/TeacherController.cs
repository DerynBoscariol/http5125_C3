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

        //GET: localhost:xxxx/Teacher/List?SearchKey={key} -->dynamically rendered webpage
        [HttpGet]
        public ActionResult List(string SearchKey)
        {
           
            // Navigating to /views/teacher/list.cshtml
            TeacherDataController Controller = new TeacherDataController();
            IEnumerable<Teacher> Teachers = Controller.ListTeachers(SearchKey);
            
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

