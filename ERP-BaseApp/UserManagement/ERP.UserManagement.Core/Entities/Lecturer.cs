using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.UserManagement.Core.Entities
{
    public class Lecturer : BaseEntity
    {
        //public Lecturer()
        //{
        //    Advicees = new HashSet<Lecturer>();
        //}
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Department { get; set; } = string.Empty;

        public string Phone { get; set; } = string.Empty;

        public string NationalId { get; set; } = string.Empty;

        public DateTime DoB { get; set; }

       // public virtual ICollection<Lecturer>? Advicees { get; set; }
    
}
}
