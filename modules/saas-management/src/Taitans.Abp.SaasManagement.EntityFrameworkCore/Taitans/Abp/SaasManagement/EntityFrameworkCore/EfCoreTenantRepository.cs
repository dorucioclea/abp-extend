﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Taitans.Abp.SaasManagement.EntityFrameworkCore
{
    public class EfCoreTenantRepository : EfCoreRepository<ISaasManagementDbContext, Tenant, Guid>, ITenantRepository
    {
        public EfCoreTenantRepository(IDbContextProvider<ISaasManagementDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Tenant FindById(Guid id, bool includeDetails = true)
        {
            return DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Id == id);
        }

        public Tenant FindByName(string name, bool includeDetails = true)
        {
            return DbSet.IncludeDetails(includeDetails)
                .FirstOrDefault(t => t.Name == name);
        }

        public async Task<Tenant> FindByNameAsync(string name, bool includeDetails = true, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .FirstOrDefaultAsync(t => t.Name == name, GetCancellationToken(cancellationToken));
        }

        public async Task<long> GetCountAsync(string filter = null, CancellationToken cancellationToken = default)
        {
            return await this
                     .WhereIf(
                         !filter.IsNullOrWhiteSpace(),
                         u =>
                             u.Name.Contains(filter)
                     ).CountAsync(cancellationToken: cancellationToken);
        }

        public async Task<List<Tenant>> GetListAsync(string sorting = null, int maxResultCount = int.MaxValue, int skipCount = 0, string filter = null, bool includeDetails = false, CancellationToken cancellationToken = default)
        {
            return await DbSet
                .IncludeDetails(includeDetails)
                .WhereIf(
                    !filter.IsNullOrWhiteSpace(),
                    u =>
                        u.Name.Contains(filter)
            )
            .OrderBy(sorting ?? nameof(Tenant.Name))
            .PageBy(skipCount, maxResultCount)
            .ToListAsync(GetCancellationToken(cancellationToken));
        }

        public override IQueryable<Tenant> WithDetails()
        {
            return GetQueryable().IncludeDetails();
        }
    }
}
