namespace Shared
{
    public class AddEmployeePayload
    {
        public string Name { get; }
        public string Surname { get; }
        public Gender Gender { get; }
        public bool IsManager { get; }
        public decimal Salary { get; }
        public string Email { get; }
        public PhoneNumberModel PhoneNumber { get; }

        public AddEmployeePayload(string name, string surname, Gender gender, bool isManager, decimal salary, string email, PhoneNumberModel phoneNumber)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            IsManager = isManager;
            Salary = salary;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    public class UpdatePhonePayload
    {
        public int EmployeeId { get; }
        public PhoneNumberModel PhoneNumber { get; }

        public UpdatePhonePayload(int employeeId, PhoneNumberModel phoneNumber)
        {
            EmployeeId = employeeId;
            PhoneNumber = phoneNumber;
        }
    }

    public class UpdateEmailPayload
    {
        public int EmployeeId { get; }
        public string Email { get; }

        public UpdateEmailPayload(int employeeId, string email)
        {
            EmployeeId = employeeId;
            Email = email;
        }
    }

    public class UpdateSalaryPayload
    {
        public int EmployeeId { get; }
        public decimal Salary { get; }

        public UpdateSalaryPayload(int employeeId, decimal salary)
        {
            EmployeeId = employeeId;
            Salary = salary;
        }
    }

    public class EmployeeSearchPayload
    {
        public string? Name { get; }
        public string? Surname { get; }
        public Gender? Gender { get; }
        public bool? IsManager { get; }
        public decimal? Salary { get; }
        public string? Email { get; }
        public DateTime? DateFrom { get; }
        public DateTime? DateTo { get; }
        public Paging? Paging { get; }

        public EmployeeSearchPayload(string? name = null, string? surname = null, Gender? gender = null, bool? isManager = null, decimal? salary = null, string? email = null, DateTime? dateFrom = null, DateTime? dateTo = null, Paging? paging = null)
        {
            Name = name;
            Surname = surname;
            Gender = gender;
            IsManager = isManager;
            Salary = salary;
            Email = email;
            DateFrom = dateFrom;
            DateTo = dateTo;
            Paging = paging;
        }
    }
}
