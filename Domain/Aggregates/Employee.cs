using Domain.Values;
using Shared;

namespace Domain.Aggregates
{
#warning add aggregateroot that contains id
    public class Employee
    {
        public long Id { get; }
        public string Name { get; }
        public string Surname { get; }
        public Gender Gender { get; }
        public bool IsManager { get; private set; }
        public decimal Salary { get; private set; }
        public string Email { get; private set; }
        public PhoneNumber PhoneNumber { get; private set; }
        public DateTime CreatedAt { get; }
        public DateTime UpdatedAt { get; private set; }

        public Employee(long id, string name, string surname, Gender gender, bool isManager, decimal salary, string email, PhoneNumber phoneNumber, DateTime createdAt, DateTime updatedAt)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Gender = gender;
            IsManager = isManager;
            Salary = salary;
            Email = email;
            PhoneNumber = phoneNumber;
            CreatedAt = createdAt;
            UpdatedAt = updatedAt;
        }

        private Employee()
        {

        }

        public static Employee Create(long id, string name, string surname, Gender gender, decimal salary, string email, PhoneNumber phoneNumber)
        {
            var employee = new Employee(
                id: id,
                name: name,
                surname: surname,
                gender: gender,
                isManager: false,
                salary: salary,
                email: email,
                phoneNumber: phoneNumber,
                createdAt: DateTime.UtcNow,
                updatedAt: DateTime.UtcNow);

            return employee;
        }

        public Employee Promote()
        {
            IsManager = true;
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

        public Employee UpdateSalary(decimal salary)
        {
            Salary = salary;
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

        public Employee UpdateEmail(string email)
        {
            Email = email;
            UpdatedAt = DateTime.UtcNow;

            return this;
        }

        public Employee UpdatePhone(PhoneNumber phoneNumber)
        {
            PhoneNumber = phoneNumber;
            UpdatedAt = DateTime.UtcNow;

            return this;
        }
    }
}
