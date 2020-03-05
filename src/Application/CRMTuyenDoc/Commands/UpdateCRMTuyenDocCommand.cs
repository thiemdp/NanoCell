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
using System.Linq;
using NanoCell.Application.Common.Exceptions;

namespace NanoCell.Application.CRMTuyenDoc.Commands
{
    [AutoMap(typeof(CRMDMTuyenDoc))]
    public class UpdateCRMTuyenDocCommand:IRequest
    {
        public long Id { get; set; }
        [MaxLength(10)]
        public String Code { get; set; }
        public string Name { get; set; }
        public class UpdateCRMTuyenDocCommandHandler : IRequestHandler<UpdateCRMTuyenDocCommand>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            public UpdateCRMTuyenDocCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<Unit> Handle(UpdateCRMTuyenDocCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var td =   _applicationDbContext.CRMDMTuyenDocs.Where(x => x.Id == request.Id).FirstOrDefault();
                    if(td== null)
                    {
                        throw new NotFoundException(nameof(CRMDMTuyenDoc), request.Id);
                    }
                    td.Name = request.Name;
                    td.Code = request.Code;
                    await _applicationDbContext.SaveChangesAsync(cancellationToken);
                    return Unit.Value;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
             
            }
        }

    }
    public class UpdateCRMTuyenDocCommandValidator : AbstractValidator<UpdateCRMTuyenDocCommand>
    {
        public UpdateCRMTuyenDocCommandValidator()
        {
            RuleFor(v => v.Code)
                .MaximumLength(10)
                .NotEmpty();
        }
    }
}
