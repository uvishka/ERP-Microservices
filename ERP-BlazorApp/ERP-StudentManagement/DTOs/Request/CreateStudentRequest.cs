using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP_StudentManagement.DTOs.Request
{
    public class CreateStudentRequest
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string? RegistrationNumber { get; set; }

        public string Email { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string Address1 { get; set; } = string.Empty;
        public string Address2 { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; } 

        public string District { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

       // public Guid? AdvisorId { get; set; }
      //  public virtual Lecturer? Advisor { get; set; }
    }
}
