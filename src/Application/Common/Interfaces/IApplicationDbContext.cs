using NanoCell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NanoCell.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {

        DbSet<CRMDMTuyenDoc> CRMDMTuyenDocs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
