using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Threading.Tasks; // Make sure to include this namespace for Task

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

        public async Task<IActionResult> Index()
        {
            List<Department> departmentList = await _departmentService.GetDepartment();


            // Your logic for combining or using both lists

            return View(departmentList);
        }

        public async Task<IActionResult> Department()
        {
            List<Department> departmentList = await _departmentService.GetDepartment();
            // Use _employeeService here as needed

            return View(departmentList);
        }
        public async Task<IActionResult> Employee()
        {
            List<Employee> departmentList = await _employeeService.GetEmployee();
            // Use _employeeService here as needed

            return View(departmentList);
        }

        public async Task<IActionResult> EmployeeFormAsync(int EmployeeID)
        {
            Employee employee;

            if (EmployeeID == 0)
            {
                // Nuevo departamento
                employee = new Employee();
                ViewBag.Accion = "New Department";
            }
            else
            {
                // Editar departamento existente (recuperar datos de la base de datos o de donde sea necesario)
                employee = await _employeeService.GetEmployeeById(EmployeeID);
                ViewBag.Accion = "Edit Department";
            }

            // Use _employeeService here as needed

            return View(employee);
        }


        public async Task<IActionResult> DepartmentFormAsync(int departmentID)
        {
            Department department;

            if (departmentID == 0)
            {
                // Nuevo departamento
                department = new Department();
                ViewBag.Accion = "New Department";
            }
            else
            {
                // Editar departamento existente (recuperar datos de la base de datos o de donde sea necesario)
                department = await _departmentService.GetDepartmentById(departmentID);
                ViewBag.Accion = "Edit Department";
            }

            // Use _employeeService here as needed

            return View(department);
        }

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
