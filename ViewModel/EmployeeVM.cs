using Pro_Practice.Models;

namespace Pro_Practice.ViewModel
{
    public class EmployeeVM
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Patronymic { get; set; }
        public string Division { get; set; }
        public string PhoneNumber { get; set; }

        public int? IdRole { get; set; }
        public string? Role { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }

        public string FullName { get; set; }

        public EmployeeVM() { }

        public EmployeeVM(Employee employee)
        {
            Id = employee.IdEmployee;
            FirstName = employee.FirstName;
            LastName = employee.LastName;
            Patronymic = employee.Patronymic;
            Division = employee.IdDivisionNavigation.NameDivision;
            PhoneNumber = employee.PhoneNumber;
            FullName = $"{employee.LastName} {employee.FirstName} {employee.Patronymic}";

            IdRole = employee.IdRole;
            Role = employee.IdRoleNavigation?.NameRole;
            Login = employee.Login;
            Password = employee.Password;
        }
    }
}
