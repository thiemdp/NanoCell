using NanoCell.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace NanoCell.Domain.Entities
{
  [Table("CRMTuyenDocs")]
  public  class CRMDMTuyenDoc: AuditableEntity
    {
        public long Id { get; set; }

        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Code { get; set; }
    }
}
