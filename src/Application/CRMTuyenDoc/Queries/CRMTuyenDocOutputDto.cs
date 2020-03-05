using NanoCell.Domain.Common.Auditing;
using NanoCell.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
 

namespace NanoCell.Application.CRMTuyenDoc.Queries
{
    [AutoMapper.AutoMap(typeof(CRMDMTuyenDoc))]
    public class CRMTuyenDocOutputDto : AuditedEntity<long>
    {
        [Display(Name="Mã tuyến đọc")]
        [Required]
        public string Name { get; set; }
        [Display(Name = "Tên tuyến đọc")]
        public string Code { get; set; }
    }
}
