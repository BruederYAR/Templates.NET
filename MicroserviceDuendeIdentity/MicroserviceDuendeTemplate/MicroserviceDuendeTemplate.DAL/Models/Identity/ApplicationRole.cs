﻿using Microsoft.AspNetCore.Identity;

namespace MicroserviceDuendeTemplate.DAL.Models.Identity;

public class ApplicationRole : IdentityRole<Guid>
{
    public ApplicationRole() { }
    public ApplicationRole(string name) : base(name) { }
}
