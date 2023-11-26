using Employees_Front.Models;

namespace Employees_Front.Services
{
    public interface IService_API_Department
    {
         Task<List<Department>>  GetDepartment();

        Task<bool> Post(Department objeto);

        Task<bool> Edit(Department objeto);

        Task<bool> Delete(int id);
        Task<Department> GetDepartmentById(int departmentID);
    }
}