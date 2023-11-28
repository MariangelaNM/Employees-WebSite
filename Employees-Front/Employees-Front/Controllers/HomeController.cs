using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Collections.Generic; // Add this namespace for List<T>

namespace Employees_Front.Controllers
{
	public class HomeController : Controller
	{
		private readonly IService_API_Department _departmentService;
		private readonly IService_API_Employee _employeeService;

		public HomeController(IService_API_Department departmentService, IService_API_Employee employeeService)
		{
			_departmentService = departmentService;
			_employeeService = employeeService;
		}

		// Use asynchronous programming for better scalability
		public async Task<IActionResult> Index()
		{
			// Use explicit type declaration for better readability
			List<Department> departmentList = await _departmentService.GetDepartment();

			
			return View(departmentList);
		}

		// Follow a consistent naming convention for action methods
		public async Task<IActionResult> Department()
		{
			List<Department> departmentList = await _departmentService.GetDepartment();
		
			return View(departmentList);
		}

		// Follow a consistent naming convention for action methods
		public async Task<IActionResult> Employee()
		{
			List<Employee> employeeList = await _employeeService.GetEmployee();
			// Use _employeeService here as needed

			return View(employeeList);
		}

		// Follow a consistent naming convention for action methods
		public async Task<IActionResult> EmployeeFormAsync(int employeeID)
		{
			Employee employee;

			if (employeeID == 0)
			{
				// New employee
				employee = new Employee();
				ViewBag.Action = "New Employee";
			}
			else
			{
				// Edit existing employee (retrieve data from the database or wherever necessary)
				employee = await _employeeService.GetEmployeeById(employeeID);
				ViewBag.Action = "Edit Employee";
			}

			var departments = await _departmentService.GetDepartment();
			// Assign the list of departments to ViewBag.Data
			ViewBag.Data = departments;

			return View(employee);
		}

		// Follow a consistent naming convention for action methods
		public async Task<IActionResult> DepartmentFormAsync(int departmentID)
		{
			Department department;

			if (departmentID == 0)
			{
				// New department
				department = new Department();
				ViewBag.Action = "New Department";
			}
			else
			{
				// Edit existing department (retrieve data from the database or wherever necessary)
				department = await _departmentService.GetDepartmentById(departmentID);
				ViewBag.Action = "Edit Department";
			}

			// Use _employeeService here as needed

			return View(department);
		}

		// Use IActionResult for consistency
		public IActionResult Privacy()
		{
			return View();
		}

		// Use IActionResult for consistency, and add comments to clarify the purpose of this action
		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
