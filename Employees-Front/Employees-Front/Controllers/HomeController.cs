using Employees_Front.Models;
using Employees_Front.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Employees_Front.Controllers

    {
    public class HomeController : Controller
    {
        private IService_API _servicioApi;

        public HomeController(IService_API servicioApi)
        {
            _servicioApi = servicioApi;
        }

        public async Task<IActionResult> Index()
        {
            List<Department> lista = await _servicioApi.GetDepartment();
            return View(lista);
        }
        public async Task<IActionResult> Department()
        {
            List<Department> lista = await _servicioApi.GetDepartment();
            return View(lista);
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
                department = await _servicioApi.GetDepartmentById(departmentID);
                ViewBag.Accion = "Edit Department";
            }

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
    