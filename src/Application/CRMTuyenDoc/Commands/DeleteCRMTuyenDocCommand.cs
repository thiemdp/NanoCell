using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using NanoCell.Application.Common.Interfaces;
using NanoCell.Domain.Entities;
using AutoMapper.Configuration.Annotations;
using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace NanoCell.Application.CRMTuyenDoc.Commands
{
    [AutoMap(typeof(CRMDMTuyenDoc))]
    public class DeleteCRMTuyenDocCommand:IRequest
    {
        [Required]
        public long Id { get; set; }
        public class DeleteCRMTuyenDocCommandHandler : IRequestHandler<DeleteCRMTuyenDocCommand>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            public DeleteCRMTuyenDocCommandHandler(IApplicationDbContext applicationDbContext)
            {
                _applicationDbContext = applicationDbContext;
            }

            public async Task<Unit> Handle(DeleteCRMTuyenDocCommand request, CancellationToken cancellationToken)
            {
                var td = await _applicationDbContext.CRMDMTuyenDocs.FindAsync(request.Id);
                _applicationDbContext.CRMDMTuyenDocs.Remove(td);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
            }
        }

    }
   
}
