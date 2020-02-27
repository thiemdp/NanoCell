using NanoCell.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NanoCell.Domain.Common.Auditing;

namespace NanoCell.Domain.Entities
{
  [Table("CRMTuyenDocs")]
  public  class CRMDMTuyenDoc: AuditedEntity
    { 
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
    }
}
