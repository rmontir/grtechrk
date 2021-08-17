using System;

namespace GrTechRK.DTO
{
    public class EmployeeDto
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                {
                    return $"{FirstName} {LastName}";
                }
                else if (!string.IsNullOrWhiteSpace(FirstName) && string.IsNullOrWhiteSpace(LastName))
                {
                    return FirstName;
                }
                else if (string.IsNullOrWhiteSpace(FirstName) && !string.IsNullOrWhiteSpace(LastName))
                {
                    return LastName;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public EmployeeDto() { }

        public EmployeeDto(
            int id,
            string firstName,
            string lastName,
            int companyId,
            string companyName,
            string email,
            string phone,
            DateTime created
        )
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            CompanyId = companyId;
            CompanyName = companyName;
            Email = email;
            Phone = phone;
            Created = created;
        }
    }
}
