using NanoCell.Domain.Common;
using NanoCell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace NanoCell.Application.CRMTuyenDoc.Queries
{
    [AutoMapper.AutoMap(typeof(CRMDMTuyenDoc))]
    public class CRMTuyenDocOutputDto : AuditableEntity
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
