using System;
using ASP.NETDesktop.Domain.Models.Dtos.Base;

namespace ASP.NETDesktop.Domain.Models.Dtos {
    public class VacationDto : BaseDto {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Comment { get; set; }
        public string Status { get; set; }

        public Guid DeveloperId { get; set; }
        public virtual DeveloperDto Developer { get; set; }
    }
}
