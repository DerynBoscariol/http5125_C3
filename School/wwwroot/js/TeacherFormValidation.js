
window.onload = function () {
	//Creating variable for the form
	var formHandle = document.forms.NewTeacher;

	//Creating variables for each input feild in the forn
	var firstName = formHandle.FirstName;
	var lastName = formHandle.LastName;
	var employeeNum = formHandle.EmployeeNumber;
	var salary = formHandle.Salary;

	//Creating a function to process the form after the user has hit submit
	function processForm() {

		//If the first name value is empty or null...
		if (firstName.value === "" || null) {
			//The field background will turn red
			firstName.style.background = "#EE5A70";
			//Focus is applied to the field
			firstName.focus();
			//A hidden div containing an error message in displayed
			document.getElementById("error-firstName").style.display = "block";
			//The form will not be sent to the server
			return false;

		//Repeating this same logic for each input field
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

	//Form submit event listener
	formHandle.onsubmit = processForm;
}