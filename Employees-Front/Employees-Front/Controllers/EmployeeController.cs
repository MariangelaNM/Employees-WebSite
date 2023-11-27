using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Employees_Front.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IService_API_Employee _apiService;

        public EmployeeController(IService_API_Employee apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> list = await _apiService.GetEmployee();
            return View(list);
        }


        // CONTROLS THE FUNCTIONS TO SAVE OR EDIT
        public IActionResult Employee(int id)
        {
            Employee employee;

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
