namespace Employees_Front.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public  int EmployeeDNI { get; set; }
        public  string EmployeeName { get; set; }
        public  string EmployeeLastName { get; set; }
        public Department Department { get; set; }
        public List<Department> Departments { get; set; }
        public string SelectedData { get; set; }
    }
}