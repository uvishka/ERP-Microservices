using System;
using ERP.UserManagement.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.Core.DTOs.Request
{
    public class UpdateLecturerRequest
    {
        public string LecturerID { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public DateTime DoB { get; set; }

       // public virtual ICollection<Student>? Advicees { get; set; }
    }
}
