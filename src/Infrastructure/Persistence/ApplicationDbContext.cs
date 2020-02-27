using NanoCell.Application.Common.Interfaces;
using NanoCell.Domain.Common.Auditing;
using NanoCell.Domain.Entities;
using NanoCell.Infrastructure.Identity;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace NanoCell.Infrastructure.Persistence
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>, IApplicationDbContext
    {
        private readonly ICurrentUserService _currentUserService;
        private readonly IDateTime _dateTime;

        public ApplicationDbContext(
            DbContextOptions  options,
            IOptions<OperationalStoreOptions> operationalStoreOptions,
            ICurrentUserService currentUserService,
            IDateTime dateTime) : base(options, operationalStoreOptions)
        {
            _currentUserService = currentUserService;
            _dateTime = dateTime;
            
        }


        public DbSet<CRMDMTuyenDoc> CRMDMTuyenDocs { get; set; }
      
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<CreationAuditedEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatorUserId = _currentUserService.UserId;
                        entry.Entity.CreationTime = _dateTime.Now;
                        break;
                    //case EntityState.Modified:
                    //    entry.Entity.LastModifiedBy = _currentUserService.UserId;
                    //    entry.Entity.LastModified = _dateTime.Now;
                    //    break;
                }
            }
            foreach (var entry in ChangeTracker.Entries<AuditedEntity>())
            {
                switch (entry.State)
                {
                    //case EntityState.Added:
                    //    entry.Entity.CreatorUserId = _currentUserService.UserId;
                    //    entry.Entity.CreationTime = _dateTime.Now;
                    //    break;
                    case EntityState.Modified:
                        entry.Entity.LastModifierUserId = _currentUserService.UserId;
                        entry.Entity.LastModificationTime = _dateTime.Now;
                        break;
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
