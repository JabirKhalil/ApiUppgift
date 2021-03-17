using ApiUppgift.Models;
using ApiUppgift.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiUppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministratorController : ControllerBase
    {
        //private readonly CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext _context;
        private readonly IIdentityService _identity;
        public AdministratorController( IIdentityService identity)
        {
           // _context = context;
            _identity = identity;
        }
        [AllowAnonymous]
        [HttpPost("signup")]

        public async Task<IActionResult> SignUpAsync([FromBody] SignUp model)
        {
            if (await _identity.CreateAdminAsync(model))
            {
                return new OkResult();
            }

            return new BadRequestResult();
        }

        //public async Task<IActionResult> SignUpAsync([FromBody] SignUpModel model)
        //{
        //    if (!_context.Administrators.Any(x => x.Email == model.Email))
        //    {
        //        try
        //        {
        //            var admin = new Administrator()
        //            {
        //                FirstName = model.FirstName,
        //                LastName = model.LastName,
        //                Email = model.Email
        //            };
        //            admin.CreatePasswordWithHash(model.Password);
        //            _context.Administrators.Add(admin);
        //            await _context.SaveChangesAsync();

        //            return new OkResult();
        //        }
        //        catch
        //        {

        //        }

        //    }

        //    return new BadRequestResult();
        //}

        [AllowAnonymous]
        [HttpPost("signip")]
        public async Task<IActionResult> SignInAsync([FromBody] SignIn model)
        {
          var response =  await _identity.SignInAdminAsync(model.Email, model.Password);
            if (response.Succeded)
                return new OkObjectResult(response.Result);

            return new BadRequestResult();
        }
    }
}
