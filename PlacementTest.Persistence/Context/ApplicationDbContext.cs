using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlacementTest.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlacementTest.Persistence.Context
{
    public class ApplicationDbContext : IdentityDbContext<
        User, Role, Guid,
        IdentityUserClaim<Guid>, UserRole,
        IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<TestTakers> TestTakers { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
