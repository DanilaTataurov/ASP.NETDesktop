using System;
using ASP.NETDesktop.Common.Enums;
using ASP.NETDesktop.Domain.Entities.Base;

namespace ASP.NETDesktop.Domain.Entities {
    public class Vacation : BaseEntity {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public VacationStatus Status { get; set; }

        public Guid DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
    }
}
