using System.ComponentModel.DataAnnotations;

namespace Employees_Front.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required]
        public  int EmployeeDNI { get; set; }
        [Required]
        public  string EmployeeName { get; set; }
        [Required]
        public  string EmployeeLastName { get; set; }
        [Required]
        public Department Department { get; set; }

        public List<Department> Departments { get; set; }
    }
}