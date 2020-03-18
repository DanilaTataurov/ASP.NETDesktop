using System.Collections.Generic;
using ASP.NETDesktop.Common.Dtos.Base;

namespace ASP.NETDesktop.Common.Dtos {
    public class DeveloperDto : BaseDto {
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

        public virtual ICollection<ProjectDto> Projects { get; set; }
        public virtual ICollection<VacationDto> Vacations { get; set; }
    }
}
