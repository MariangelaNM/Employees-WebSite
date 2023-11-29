using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Employees_Front.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IService_API_Employee _apiService;
        private readonly IService_API_Department _departmentService;

        public EmployeeController(IService_API_Employee apiService, IService_API_Department departmentService)
        {
            _departmentService = departmentService;
            _apiService = apiService;
        }

        // Action method for displaying the list of employees
        public async Task<IActionResult> Index()
        {
            List<Employee> employees = await _apiService.GetEmployee();
            return View(employees);
        }

        // Action method for displaying the employee form
        public async Task<IActionResult> EmployeeForm()
        {
            var model = new Employee();
            model.Department = await GetDepartmentAsync();

            return View(model);
        }

        // GET: Display a form for creating or editing a department
        // Controls the functions to save or edit
        public IActionResult Employee(int departmentID)
        {
            Employee employee;

            if (departmentID == 0)
            {
                // New employee
                employee = new Employee();
                ViewBag.Action = "New Employee"; // Set ViewBag.Action for displaying the appropriate title in the view
            }
            else
            {
                // Edit existing employee (retrieve data from the database or wherever necessary)
                employee = new Employee();
                ViewBag.Action = "Edit Employee"; // Set ViewBag.Action for displaying the appropriate title in the view
            }

            return View(employee);
        }
        // Helper method to get the list of departments asynchronously
        public async Task<List<Department>> GetDepartmentAsync()
        {
            return await _departmentService.GetDepartment();
        }

        // Action method for creating or editing an employee
        public async Task<IActionResult> EmployeeAsync(int id)
        {
            Employee employee;
            var departments = await _departmentService.GetDepartment();

            ViewBag.Data = new SelectList(departments, "DepartmentID", "DepartmentName");

            if (id == 0)
            {
                // New employee
                employee = new Employee();
                ViewBag.Action = "New Employee";
            }
            else
            {
                // Edit existing employee (retrieve data from the database or elsewhere)
                employee = new Employee();
                ViewBag.Action = "Edit Employee";
            }

            return View(employee);
        }

        // Action method for handling the POST request to save or edit an employee
        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            (bool Success, string Message) response;

            if (employee.EmployeeID == 0)
            {
                // Create a new employee
                response = await _apiService.Post(employee);
            }
            else
            {
                // Edit existing employee
                response = await _apiService.Edit(employee);
            }

            if (response.Success)
            {
                TempData["AlertMessage"] = "Saved Successfully!";
                return RedirectToAction("Employee", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = response.Message; // Pass the error message to the view
                return NoContent();
            }
        }

        // Action method for handling the GET request to delete an employee
        [HttpGet]
        public async Task<IActionResult> Delete(int EmployeeID)
        {
            var response = await _apiService.Delete(EmployeeID);

            if (response)
            {
                TempData["AlertMessage"] = "Deleted Successfully!";
                return RedirectToAction("Employee", "Home");
            }
            else
            {
                return NoContent();
            }
        }
    }
}
