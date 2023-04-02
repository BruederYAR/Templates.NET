using MicroserviceDuendeTemplate.DAL.Database;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MicroserviceDuendeTemplate.DAL.Models.Identity;

public class ApplicationRoleStore : RoleStore<ApplicationRole, ApplicationDbContext, Guid>
{
    public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber? describer = null) : base(context, describer)
    {
    }
}
