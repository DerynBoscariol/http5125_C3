
window.onload = function () {
	var formHandle = document.forms.NewTeacher;
	var firstName = formHandle.FirstName;
	var lastName = formHandle.LastName;
	var employeeNum = formHandle.EmployeeNumber;
	var salary = formHandle.Salary;

	function processForm() {

		if (firstName.value === "" || null) {
			firstName.style.background = "#EE5A70";
			firstName.focus();
			document.getElementById("error-firstName").style.display = "block";
			return false;
		} else if (lastName.value === "" || null) {
			lastName.style.background = "#EE5A70";
			lastName.focus();
			document.getElementById("error-lastName").style.display = "block";
			return false;
		} else if (employeeNum.value === "" || null) {
			employeeNum.style.background = "#EE5A70";
			employeeNum.focus();
			document.getElementById("error-employeeNum").style.display = "block";
		return false;
		} else if (salary.value === "" || null) {
			salary.style.background = "#EE5A70";
			salary.focus();
			document.getElementById("error-salary").style.display = "block";
		return false;
		}
	}
		

	


	//LISTENERS
	formHandle.onsubmit = processForm;
}