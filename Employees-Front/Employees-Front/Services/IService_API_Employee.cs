using Employees_Front.Models;

namespace Employees_Front.Services
{
    public interface IService_API_Employee
    {
         Task<List<Employee>>  GetEmployee();

        Task<(bool Success, string Message)> Post(Employee objeto);

        Task<(bool Success, string Message)> Edit(Employee objeto);

        Task<bool> Delete(int EmployeeID);
        Task<Employee> GetEmployeeById(int id);
    }
}