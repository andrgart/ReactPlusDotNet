using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApiTest.AppIdentity;
using WebApiTest.Domains;

namespace WebApiTest.DataAccssesLayer
{
    public class HumanContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public virtual DbSet<Oleg> Brands { get; set; }

        public HumanContext(DbContextOptions options)
            : base(options)
        {}
    }
}
