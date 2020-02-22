using NanoCell.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace NanoCell.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<CRMDMTuyenDoc> CRMDMTuyenDocs { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
