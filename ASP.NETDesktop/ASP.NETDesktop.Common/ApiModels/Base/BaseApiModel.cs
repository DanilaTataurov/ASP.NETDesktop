using System;

namespace ASP.NETDesktop.Common.ApiModels.Base {
    public class BaseApiModel {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
