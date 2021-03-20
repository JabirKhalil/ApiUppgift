using System;
using System.Collections.Generic;

#nullable disable

namespace ApiUppgift.Models
{
    public partial class SessionToken
    {
        public long Id { get; set; }
        public byte[] AccessToken { get; set; }
    }
}
