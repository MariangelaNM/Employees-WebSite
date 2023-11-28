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

        // GET: Index action to display a list of departments
        public async Task<IActionResult> Index()
        {
            List<Department> departments = await _apiService.GetDepartment();
            ViewBag.Departments = departments;

            return View(departments);
        }

        // GET: Department action to handle both creating a new department and editing an existing one
        public IActionResult Department(int departmentID)
        {
            Department department;

            if (departmentID == 0)
            {
                // Creating a new department
                department = new Department();
                ViewBag.Action = "New Department";
            }
            else
            {
                // Editing an existing department (retrieve data from the database or wherever necessary)
                department = await _apiService.GetDepartmentById(departmentID);
                ViewBag.Action = "Edit Department";
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
                // Saving a new department
                response = await _apiService.Post(department);
            }
            else
            {
                // Editing an existing department
                response = await _apiService.Edit(department);
            }

            if (response.Success)
            {
                TempData["AlertMessage"] = "Saved Successfully!";
                return RedirectToAction("Department", "Home");
            }
            else
            {
                TempData["ErrorMessage"] = response.Message; // Pass the error message to the view
                return NoContent(); // Consider using a more appropriate status code
            }
        }

        // GET: Delete action to delete a department
        [HttpGet]
        public async Task<IActionResult> Delete(int departmentID)
        {
            bool hasEmployees = await _apiService.DepartmentHasEmployees(departmentID);

            if (hasEmployees)
            {
                TempData["ErrorMessage"] = "This department contains employees"; // Pass the error message to the view
                return NoContent(); // Consider using a more appropriate status code
            }

            var deletionResult = await _apiService.Delete(departmentID);

            if (deletionResult)
                return RedirectToAction("Department", "Home");
            else
                TempData["ErrorMessage"] = "Error deleting the department"; // Pass the error message to the view

            return NoContent(); // Consider using a more appropriate status code
        }
    }
}
