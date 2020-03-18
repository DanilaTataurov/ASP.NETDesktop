﻿using System;
using ASP.NETDesktop.Common.ApiModels.Base;
using ASP.NETDesktop.Common.Enums;

namespace ASP.NETDesktop.Common.ApiModels {
    public class VacationApiModel : BaseApiModel {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public string Comment { get; set; }
        public VacationStatus Status { get; set; }

        public Guid DeveloperId { get; set; }
        public virtual DeveloperApiModel Developer { get; set; }
    }
}
