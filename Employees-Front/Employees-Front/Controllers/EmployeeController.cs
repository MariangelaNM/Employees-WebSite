using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Diagnostics;
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

        public async Task<IActionResult> Index()
        {
            List<Employee> list = await _apiService.GetEmployee();
            return View(list);
        }
        public async Task<IActionResult> EmployeeForm()
        {
            var model = new Employee();
            model.Department = await GetDepartmentAsync();  // Debes implementar este método según tu lógica

            // Resto del código...

            return View(model);
        }


        public async Task<List<Department>> GetDepartmentAsync()
        {
            
            return await _departmentService.GetDepartment(); 
        }

        // CONTROLS THE FUNCTIONS TO SAVE OR EDIT
        public async Task<IActionResult> EmployeeAsync(int id)
        {
            Employee employee;
    
            // Use _employeeService here as needed
            var departments = await _departmentService.GetDepartment(); // Debes implementar este método según tu lógica


            // Asigna la lista de departamentos a ViewData
            ViewBag.Data = new SelectList(departments, "DepartmentID", "DepartmentName");

            if (id == 0)
            {
                // New department
                employee = new Employee();
                ViewBag.Action = "New Employee";
            }
            else
            {
                // Edit existing department (retrieve data from the database or wherever necessary)
                employee = new Employee();
                ViewBag.Action = "Edit Employee";
            }

            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Employee employee)
        {
            bool response;

            if (employee.EmployeeID == 0)
            {
                response = await _apiService.Post(employee);
            }
            else
            {
                response = await _apiService.Edit(employee);
            }

            if (response)
                return RedirectToAction("Employee", "Home");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int EmployeeID)
        {
            var response = await _apiService.Delete(EmployeeID);

            if (response)
                return RedirectToAction("Employee", "Home");
            else
                return NoContent();
        }
    }
}
