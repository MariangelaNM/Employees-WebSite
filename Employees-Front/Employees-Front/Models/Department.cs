
namespace Employees_Front.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public  string DepartmentName { get; set; }

        public static implicit operator Department(List<Department> v)
        {
            throw new NotImplementedException();
        }
        public Department()
        {
            DepartmentName = ""; // Establecer el valor predeterminado aquí
        }
    }
}
