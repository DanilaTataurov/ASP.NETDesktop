using System.Collections.Generic;
using ASP.NETDesktop.Common.ApiModels.Base;

namespace ASP.NETDesktop.Common.ApiModels {
    public class DeveloperApiModel : BaseApiModel {
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

        public virtual ICollection<ProjectApiModel> Projects { get; set; }
        public virtual ICollection<VacationApiModel> Vacations { get; set; }
    }
}
