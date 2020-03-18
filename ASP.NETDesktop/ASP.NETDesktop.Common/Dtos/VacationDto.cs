using System;
using ASP.NETDesktop.Common.Dtos.Base;
using ASP.NETDesktop.Common.Enums;

namespace ASP.NETDesktop.Common.Dtos {
    public class VacationDto : BaseDto {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public VacationStatus Status { get; set; }

        public Guid DeveloperId { get; set; }
        public virtual DeveloperDto Developer { get; set; }
    }
}
