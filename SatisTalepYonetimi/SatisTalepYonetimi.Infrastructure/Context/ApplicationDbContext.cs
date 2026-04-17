using GenericRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SatisTalepYonetimi.Domain.Entities;

namespace SatisTalepYonetimi.Infrastructure.Context
{
    public sealed class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>, IUnitOfWork
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SalesRequest> SalesRequests { get; set; }
        public DbSet<SalesRequestItem> SalesRequestItems { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(typeof(DependencyInjection).Assembly);

            builder.Ignore<IdentityUserLogin<Guid>>();
            builder.Ignore<IdentityRoleClaim<Guid>>();
            builder.Ignore<IdentityUserToken<Guid>>();
            builder.Ignore<IdentityUserRole<Guid>>();
            builder.Ignore<IdentityUserClaim<Guid>>();
        }
    }
}
