using System;
using System.Collections.Generic;

#nullable disable

namespace ApiUppgift.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Issues = new HashSet<Issue>();
        }

        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public string DisplayName => $"{FirstName} {LastName}";

        public virtual ICollection<Issue> Issues { get; set; }
    }
}
