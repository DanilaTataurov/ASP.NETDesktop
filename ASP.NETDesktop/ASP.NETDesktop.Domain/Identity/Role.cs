﻿using System;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ASP.NETDesktop.Domain.Identity {
    public class Role : IdentityRole<Guid, UserRole> { }
}
