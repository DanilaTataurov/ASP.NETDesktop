﻿using System;

namespace ASP.NETDesktop.Domain.Models.Dtos.Base {
    public class BaseDto {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
