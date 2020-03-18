using System;
using System.Collections.Generic;
using ASP.NETDesktop.Common.Dtos.Base;

namespace ASP.NETDesktop.Common.Dtos {
    public class ProjectDto : BaseDto {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Client { get; set; }
        public string Company { get; set; }
        public string Source { get; set; }
        public string Contact { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GitUrl { get; set; }

        public virtual ICollection<DeveloperDto> Developers { get; set; }
    }
}
