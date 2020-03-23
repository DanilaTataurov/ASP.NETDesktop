using System.Collections.Generic;
using ASP.NETDesktop.Domain.Entities.Base;

namespace ASP.NETDesktop.Domain.Entities {
    public class Developer : BaseEntity {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Grade { get; set; }
        public string Location { get; set; }
        public string Room { get; set; }
        public string Skype { get; set; }
        public string Email { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string Schedule { get; set; }

        public virtual ICollection<Project> Projects { get; set; }
        public virtual ICollection<Vacation> Vacations { get; set; }
    }
}
