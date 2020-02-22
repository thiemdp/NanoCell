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
    public class CreateCRMTuyenDocCommand:IRequest<long>
    {
        [MaxLength(10)]
        public String Code { get; set; }
        public string Name { get; set; }
        public class CreateCRMTuyenDocCommandHandler : IRequestHandler<CreateCRMTuyenDocCommand, long>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            public CreateCRMTuyenDocCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<long> Handle(CreateCRMTuyenDocCommand request, CancellationToken cancellationToken)
            {
                var tuyendoc = _mapper.Map<CRMDMTuyenDoc>(request);
                _applicationDbContext.CRMDMTuyenDocs.Add(tuyendoc);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return tuyendoc.Id;
            }
        }

    }
    public class CreateCRMTuyenDocCommandValidator : AbstractValidator<CreateCRMTuyenDocCommand>
    {
        public CreateCRMTuyenDocCommandValidator()
        {
            RuleFor(v => v.Code)
                .MaximumLength(10)
                .NotEmpty();
        }
    }
}
