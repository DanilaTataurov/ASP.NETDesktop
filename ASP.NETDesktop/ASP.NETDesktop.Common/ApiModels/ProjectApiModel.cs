using System;
using System.Collections.Generic;
using ASP.NETDesktop.Common.ApiModels.Base;

namespace ASP.NETDesktop.Common.ApiModels {
    public class ProjectApiModel : BaseApiModel {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public string Company { get; set; }
        public string Source { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string GitUrl { get; set; }

        public virtual ICollection<DeveloperApiModel> Developers { get; set; }
    }
}
