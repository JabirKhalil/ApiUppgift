using System;
using System.Collections.Generic;

#nullable disable

namespace ApiUppgift.Models
{
    public partial class Issue
    {
        public long Id { get; set; }
        public long AdministratorId { get; set; }
        public long CustomerId { get; set; }
        public DateTime IssueStart { get; set; }
        public DateTime? IssueChange { get; set; }

        public virtual Administrator Administrator { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
