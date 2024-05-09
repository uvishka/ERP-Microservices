using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.Core.Entities
{
    public class Student : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? RegistrationNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string Address1 { get; set; }
        public string Address2 { get; set; }

        public DateTime DateOfBirth { get; set; }

        public string District { get; set; } = string.Empty;

        public string NationalId { get; set; }


       // public Guid? AdvisorId { get; set; }

        public string Department {  get; set; }


       // public virtual Lecturer? Advisor { get; set; }

    }
}
