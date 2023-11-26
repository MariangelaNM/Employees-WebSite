using Employees_Front.Models;
using Employees_Front.Services;

using Microsoft.AspNetCore.Mvc;

using System.Diagnostics;


namespace Employees_Front.Controllers
{
	public class DepartmentController : Controller
	{
		private IService_API _servicioApi;

		public DepartmentController(IService_API servicioApi)
		{
			_servicioApi = servicioApi;
		}

        public async Task<IActionResult> Index()
        {
            List<Department> lista = await _servicioApi.GetDepartment();
            return View(lista);
        }
        public async Task<IActionResult> DepartmentHome()
        {
            List<Department> lista = await _servicioApi.GetDepartment();
            return View(lista);
        }

        //VA A CONTROLAR LAS FUNCIONES DE GUARDAR O EDITAR

        public IActionResult Department(int departmentID)
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
                department = new Department();
                ViewBag.Accion = "Edit Department";
            }

            return View(department);
        }



        [HttpPost]
		public async Task<IActionResult> Post(Department ob_producto)
		{

			bool respuesta;

			if (ob_producto.DepartmentID == 0)
			{
				respuesta = await _servicioApi.Post(ob_producto);
			}
			else
			{
				respuesta = await _servicioApi.Edit(ob_producto);
			}


			if (respuesta)
                return RedirectToAction("DepartmentHome");
            else
				return NoContent();

		}

		[HttpGet]
		public async Task<IActionResult> Delete(int DepartmentID)
		{

			var respuesta = await _servicioApi.Delete(DepartmentID);

			if (respuesta)
				return RedirectToAction("Department");
			else
				return NoContent();
		}


	}
}