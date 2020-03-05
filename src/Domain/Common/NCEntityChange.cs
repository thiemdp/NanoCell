using System;
using System.Collections.Generic;
using System.Text;
using NanoCell.Domain.Common.Auditing;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using NanoCell.Domain.Enums;

namespace NanoCell.Domain.Common
{
    [Table("NCEntityChanges")]
   public class NCEntityChange:CreationAuditedEntity<long>
    {
        public NCEntityChangeType ChangeType { get; set; }
        [MaxLength(255)]
        public string TableName { get; set; }
        [MaxLength(255)]
        public string EntityId { get; set; }
        [MaxLength(255)]
        public string keyName { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}
