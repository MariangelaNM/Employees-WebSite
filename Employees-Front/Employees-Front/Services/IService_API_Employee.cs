using Employees_Front.Models;

namespace Employees_Front.Services
{
    public interface IService_API_Employee
    {
         Task<List<Employee>>  GetEmployee();

        Task<bool> Post(Employee objeto);

        Task<bool> Edit(Employee objeto);

        Task<bool> Delete(int EmployeeID);
        Task<Employee> GetEmployeeById(int id);
    }
}