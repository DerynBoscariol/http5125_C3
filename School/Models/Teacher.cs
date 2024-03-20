using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
namespace School.Models
{
	public class Teacher
	{
		public int TeacherId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? EmployeeNumber { get; set; }
		public DateTime? HireDate { get; set; }
		public decimal Salary { get; set; }
	}
}

