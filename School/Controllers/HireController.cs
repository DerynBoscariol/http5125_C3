using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mysqlx.Crud;
using School.Models;

namespace School.Controllers
{

    [ApiController]
    public class HireController : ControllerBase
    {
        /// <summary>
        /// Recieves info about the teacher and makes a new hire
        /// </summary>
        /// <returns>
        /// Info about the New teacher
        /// </returns>
        /// <param name></param>
        /// <example>
        /// GET api/order/MakeOrder/S/Chris -> {"OrderName":"Chris","OrderChocolates":"S", "OrderTotal":10}
        /// </example>
        [HttpGet]
        [Route("api/Teacher/NewHire/")]

        public Teacher NewHire(int TeacherId, string TeacherFirstName, string TeacherLastName, string EmployeeNumber, DateTime HireDate, decimal Salary)
        {
            

            //Creating a teacher object to send as the response
            Teacher NewHire = new Teacher();
            NewHire.FirstName= TeacherFirstName;
            NewHire.LastName = TeacherLastName;
            NewHire.EmployeeNumber = EmployeeNumber;
            NewHire.TeacherId = TeacherId;
            NewHire.HireDate = HireDate;
            NewHire.Salary = Salary;
            
            return NewHire;

        }

        //HireController Hiring = new HireController();
        //Hire NewHire = Hiring.NewTeacher(TeacherId, TeacherFirstName, TeacherLastName, EmployeeNumber, HireDate, TeacherSalary);
    }
}