namespace Shared
{
    public enum Gender
    {
        Male,
        Female
    }

    public class EmployeeModel
    {
        public long Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public Gender Gender { get; }
        public bool IsManager { get; }
        public decimal Salary { get; }
        public string Email { get;  }
        public PhoneNumberModel PhoneNumber { get; }

        public EmployeeModel(long id, string name, string surname, Gender gender, bool isManager, decimal salary, string email, PhoneNumberModel phoneNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gender = gender;
            IsManager = isManager;
            Salary = salary;
            Email = email;
            PhoneNumber = phoneNumber;
        }
    }

    public class PhoneNumberModel
    {
        public string Phone { get; }
        public string MobilePhone { get; }

        public PhoneNumberModel(string phone, string mobilePhone)
        {
            Phone = phone;
            MobilePhone = mobilePhone;
        }
    }
}
