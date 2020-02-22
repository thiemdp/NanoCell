using NanoCell.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using MediatR;
using NanoCell.Application.Common.Interfaces;
using AutoMapper;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
using Microsoft.EntityFrameworkCore;
//thiemdp
namespace NanoCell.Application.CRMTuyenDoc.Queries
{
    public class GetAllCRMTuyenDocQuery:IRequest<List<CRMTuyenDocOutputDto>>
    {
        public class GetAllCRMTuyenDocQueryHandler : IRequestHandler<GetAllCRMTuyenDocQuery, List<CRMTuyenDocOutputDto>>
        {
            private readonly IApplicationDbContext _applicationDbContext;
            private readonly IMapper _mapper;
            public GetAllCRMTuyenDocQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
            {
                _applicationDbContext = applicationDbContext;
                _mapper = mapper;
            }

            public async Task<List<CRMTuyenDocOutputDto>> Handle(GetAllCRMTuyenDocQuery request, CancellationToken cancellationToken)
            {
                var list = await _applicationDbContext.CRMDMTuyenDocs.ToListAsync();
                return _mapper.Map<List<CRMTuyenDocOutputDto>>(list);
            }
        }
    }
}
