using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUppgift.Models
{
    public class SignInResponseModel
    {
        public bool Succeded { get; set; }
        public SignInResponseResut Result { get; set; }
    }

    public class SignInResponseResut
    {
        public long Id { get; set; }
        public string Email { get; set; }
        public string AccessToken { get; set; }
    }
}
