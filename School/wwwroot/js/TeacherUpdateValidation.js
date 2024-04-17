
window.onload = function () {
	//Creating variable for the form
	var formHandle = document.forms.UpdateTeacher;

	//Creating variables for each input field in the forn
	var firstName = formHandle.FirstName;
	var lastName = formHandle.LastName;
	var employeeNum = formHandle.EmployeeNumber;
	var salary = formHandle.Salary;

	//Creating a function to process the form after the user has hit submit
	function processForm() {

		firstName.style.background = "#FFFFFF";
		lastName.style.background = "#FFFFFF";
		employeeNum.style.background = "#FFFFFF";
		salary.style.background = "#FFFFFF";
		document.getElementById("error-firstName").style.display = "none";
		document.getElementById("error-lastName").style.display = "none";
		document.getElementById("error-employeeNum").style.display = "none";
		document.getElementById("error-salary").style.display = "none";
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
		}
		if (lastName.value === "" || null) {
			lastName.style.background = "#EE5A70";
			lastName.focus();
			document.getElementById("error-lastName").style.display = "block";
			return false;
		}
		if (employeeNum.value === "" || null) {
			employeeNum.style.background = "#EE5A70";
			employeeNum.focus();
			document.getElementById("error-employeeNum").style.display = "block";
			return false;
		}if (salary.value === "" || null) {
			salary.style.background = "#EE5A70";
			salary.focus();
			document.getElementById("error-salary").style.display = "block";
			return false;
		}
	} 

//Form submit event listener
	formHandle.onsubmit = processForm;
}
