using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;

namespace Employees_Front.Controllers
{
	public class DepartmentController : Controller
	{
		private readonly IService_API_Department _apiService;

		public DepartmentController(IService_API_Department apiService)
		{
			_apiService = apiService;
		}

		// GET: Display a list of departments
		public async Task<IActionResult> Index()
		{
			List<Department> list = await _apiService.GetDepartment();
			ViewBag.Departments = list; // Store the list of departments in ViewBag for use in the view

			return View(list);
		}

		// GET: Display a form for creating or editing a department
		// Controls the functions to save or edit
		public IActionResult Department(int departmentID)
		{
			Department department;

			if (departmentID == 0)
			{
				// New department
				department = new Department();
				ViewBag.Action = "New Department"; // Set ViewBag.Action for displaying the appropriate title in the view
			}
			else
			{
				// Edit existing department (retrieve data from the database or wherever necessary)
				department = new Department();
				ViewBag.Action = "Edit Department"; // Set ViewBag.Action for displaying the appropriate title in the view
			}

			return View(department);
		}

		// POST: Save or edit a department
		[HttpPost]
		public async Task<IActionResult> Post(Department department)
		{
			(bool Success, string Message) response;

			if (department.DepartmentID == 0)
			{
				// Post a new department
				response = await _apiService.Post(department);
			}
			else
			{
				// Edit an existing department
				response = await _apiService.Edit(department);
			}

			if (response.Success)
			{
				TempData["AlertMessage"] = "Saved Successfully!"; // Display a success message using TempData
				return RedirectToAction("Department", "Home"); // Redirect to the department list after successful save
			}
			else
			{
				TempData["ErrorMessage"] = response.Message; // Pass the error message to the view using TempData
				return NoContent(); // Return NoContent for unsuccessful save
			}
		}

		// GET: Delete a department
		[HttpGet]
		public async Task<IActionResult> Delete(int departmentID)
		{
			var response = await _apiService.Delete(departmentID);

			if (response)
				return RedirectToAction("Department", "Home"); // Redirect to the department list after successful delete
			else
				TempData["ErrorMessage"] = "This department contains employees"; // Pass the error message to the view using TempData

			return NoContent(); // Return NoContent for unsuccessful delete
		}
	}
}
