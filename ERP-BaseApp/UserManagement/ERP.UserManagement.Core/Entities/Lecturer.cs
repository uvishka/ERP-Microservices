using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.Core.Entities
{
    public class Lecturer : BaseEntity
    {
        public Lecturer()
        {
            Advicees = new HashSet<Student>();
        }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; }

        public string Department { get; set; }

        public string Phone { get; set; }

        public string NationalId { get; set; }

        public DateTime DoB { get; set; }

        public virtual ICollection<Student>? Advicees { get; set; }
    
}
}
