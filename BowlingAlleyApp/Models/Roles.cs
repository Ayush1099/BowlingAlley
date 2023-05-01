using System.Collections.Generic;

namespace BowlingAlleyApp.Models
{
    public class Roles
    {
        public Roles()
        {
            Reservations = new HashSet<Reservations>();
        }

        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public bool RoleType { get; set; }

        public virtual ICollection<Reservations> Reservations { get; set; }
    }
}
