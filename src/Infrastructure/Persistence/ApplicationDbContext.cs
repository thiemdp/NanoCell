﻿using NanoCell.Application.Common.Interfaces;
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
using NanoCell.Domain.Common;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq;
using System;
using Newtonsoft.Json;
using NanoCell.Domain.Enums;

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


        public virtual DbSet<CRMDMTuyenDoc> CRMDMTuyenDocs { get; set; }
        public virtual DbSet<NCEntityChange> NCEntityChanges { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
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
                    case EntityState.Added:
                        entry.Entity.CreatorUserId = _currentUserService.UserId;
                        entry.Entity.CreationTime = _dateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifierUserId = _currentUserService.UserId;
                        entry.Entity.LastModificationTime = _dateTime.Now;
                        break;
                }
            }
            var auditEntries = OnBeforeSaveChanges();
            int result = await base.SaveChangesAsync(cancellationToken);
            await OnAfterSaveChanges(auditEntries);
            return result;
          
        }

        public async Task SetModified<TEntity>(TEntity entity)
        {
            Entry(entity).State = EntityState.Modified;
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }

        private List<NCEntityChangeEntry> OnBeforeSaveChanges()
        {
            ChangeTracker.DetectChanges();
            var auditEntries = new List<NCEntityChangeEntry>();
            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is NCEntityChange || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                    continue;

                var auditEntry = new NCEntityChangeEntry(entry);
                auditEntry.userId= _currentUserService.UserId;
                auditEntry.TableName = entry.Metadata.GetTableName();//.Relational().TableName;
                if (string.IsNullOrEmpty(auditEntry.TableName))
                    continue;
                auditEntries.Add(auditEntry);
                foreach (var property in entry.Properties)
                {
                    if (property.IsTemporary)
                    {
                        // value will be generated by the database, get the value after saving
                        auditEntry.TemporaryProperties.Add(property);
                        continue;
                    }

                    string propertyName = property.Metadata.Name;
                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            auditEntry.ChangeType = NCEntityChangeType.Created;
                            break;

                        case EntityState.Deleted:
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            auditEntry.ChangeType = NCEntityChangeType.Deleted;
                            break;

                        case EntityState.Modified:
                            if (property.IsModified)
                            {
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                                auditEntry.ChangeType = NCEntityChangeType.Updated;
                            }
                            break;
                    }
                }
            }

            // Save audit entities that have all the modifications
            foreach (var auditEntry in auditEntries.Where(_ => !_.HasTemporaryProperties))
            {
                NCEntityChanges.Add(auditEntry.ToAudit());
            }

            // keep a list of entries where the value of some properties are unknown at this step
            return auditEntries.Where(_ => _.HasTemporaryProperties).ToList();
        }

        private Task OnAfterSaveChanges(List<NCEntityChangeEntry> auditEntries)
        {
            if (auditEntries == null || auditEntries.Count == 0)
                return Task.CompletedTask;
    
            foreach (var auditEntry in auditEntries)
            {
                // Get the final value of the temporary properties
                foreach (var prop in auditEntry.TemporaryProperties)
                {
                    if (prop.Metadata.IsPrimaryKey())
                        auditEntry.KeyValues[prop.Metadata.Name] = prop.CurrentValue;
                    else
                        auditEntry.NewValues[prop.Metadata.Name] = prop.CurrentValue;
                }

                // Save the Audit entry
                NCEntityChanges.Add(auditEntry.ToAudit());
            }

            return SaveChangesAsync();
        }
    }

    public class NCEntityChangeEntry
    {
        public NCEntityChangeEntry(EntityEntry entry)
        {
            Entry = entry;
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string userId { get; set; }
        public string KeyName { get; set; }
        public NCEntityChangeType ChangeType { get; set; }
        public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
        public List<PropertyEntry> TemporaryProperties { get; } = new List<PropertyEntry>();

        public bool HasTemporaryProperties => TemporaryProperties.Any();

        public NCEntityChange ToAudit()
        {
            var audit = new NCEntityChange();
            audit.ChangeType = ChangeType;
            audit.TableName = TableName;
            audit.CreationTime = DateTime.UtcNow;
            audit.CreatorUserId = userId;
            if(KeyValues.Any())
            {
                audit.keyName = KeyValues.First().Key;
                audit.EntityId = KeyValues.First().Value +"";
            }
            audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
            audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
            return audit;
        }
    }
}
