using ApiUppgift.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUppgift.Services
{
    public interface IIdentityService
    {
        Task<bool> CreateAdminAsync(SignUp model);
        Task<SignInResponseModel> SignInAdminAsync(string email, string password);
    }
}
