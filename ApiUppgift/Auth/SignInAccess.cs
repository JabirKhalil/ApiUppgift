using ApiUppgift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUppgift.Auth
{
    public class SignInAccess
    {
        public static bool LogIn(string email, string password)
        {
            using (CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext _context = new CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext())
            {
                return _context.Administrators.Any(admin => admin.Email.Equals(email, StringComparison.OrdinalIgnoreCase) &&
                admin.ValidatePasswordHash(password));
            }
        }

        //public bool LogIn(SignIn signIn)
        //{
        //    using (CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext _context = new CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext())
        //    {
        //        return _context.Administrators.Any(admin => admin.Email.Equals(signIn.Email , StringComparison.OrdinalIgnoreCase) &&
        //        admin.ValidatePasswordHash(signIn.Password));
        //    }
        //}
    }
}
