using NanoCell.Domain.Common.Auditing;
using NanoCell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NanoCell.Application.CRMTuyenDoc.Queries
{
    [AutoMapper.AutoMap(typeof(CRMDMTuyenDoc))]
    public class CRMTuyenDocOutputDto : AuditedEntity<long>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
