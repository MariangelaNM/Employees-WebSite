using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Employees_Front.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IService_API_Department _apiService;

        public DepartmentController(IService_API_Department apiService)
        {
            _apiService = apiService;
        }

        public async Task<IActionResult> Index()
        {
            List<Department> list = await _apiService.GetDepartment();
            return View(list);
        }


        // CONTROLS THE FUNCTIONS TO SAVE OR EDIT
        public IActionResult Department(int departmentID)
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
                department = new Department();
                ViewBag.Action = "Edit Department";
            }

            return View(department);
        }

        [HttpPost]
        public async Task<IActionResult> Post(Department department)
        {
            bool response;

            if (department.DepartmentID == 0)
            {
                response = await _apiService.Post(department);
            }
            else
            {
                response = await _apiService.Edit(department);
            }

            if (response)
                return RedirectToAction("Department", "Home");
            else
                return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int departmentID)
        {
            var response = await _apiService.Delete(departmentID);

            if (response)
                return RedirectToAction("Department", "Home");
            else
                return NoContent();
        }
    }
}
