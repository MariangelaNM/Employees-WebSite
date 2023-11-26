using Employees_Front.Models;

namespace Employees_Front.Services
{
    public interface IService_API
    {
         Task<List<Department>>  GetDepartment();
        Task<Department> Filter(int id);

        Task<bool> Post(Department objeto);

        Task<bool> Edit(Department objeto);

        Task<bool> Delete(int id);
    }
}