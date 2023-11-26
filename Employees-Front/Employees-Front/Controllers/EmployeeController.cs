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
        private readonly Service_API_Employee _servicioApi;

        public EmployeeController(Service_API_Employee servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Employee> lista = await _servicioApi.GetEmployee();
            return View(lista);
        }
        public async Task<IActionResult> Employee()
        {
            List<Employee> lista = await _servicioApi.GetEmployee();
            return View(lista);
        }

        public async Task<IActionResult> DepartmentFormAsync(int departmentID)
        {
            Employee department;

            if (departmentID == 0)
            {
                // Nuevo departamento
                department = new Employee();
                ViewBag.Accion = "New Department";
            }
            else
            {
                // Editar departamento existente (recuperar datos de la base de datos o de donde sea necesario)
                department = await _servicioApi.GetEmployeeById(departmentID);
                ViewBag.Accion = "Edit Department";
            }

            return View(department);
        }

        // Other action methods...

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
