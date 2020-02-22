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
                var tuyendoc = _mapper.Map<CRMDMTuyenDoc>(request);
                _applicationDbContext.CRMDMTuyenDocs.Update(tuyendoc);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return Unit.Value;
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
