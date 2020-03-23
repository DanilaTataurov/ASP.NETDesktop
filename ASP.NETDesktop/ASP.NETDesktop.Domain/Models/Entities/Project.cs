using System;
using System.Collections.Generic;
using ASP.NETDesktop.Domain.Entities.Base;

namespace ASP.NETDesktop.Domain.Entities {
    public class Project : BaseEntity {
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

        public virtual ICollection<Developer> Developers { get; set; }
    }
}
