using ApiUppgift.Models;
using ApiUppgift.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ApiUppgift.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AdministratorController : ControllerBase
    {
        private readonly CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext _context;
        
        private IConfiguration _configuration { get; }
        public AdministratorController(CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext context, IConfiguration  configuration)
        {
            _context = context;
            _configuration = configuration;


        }
        [AllowAnonymous]
        [HttpPost("signup")]
        public async Task<IActionResult> SignUp([FromBody] SignUp model)
        {
            try
            {
                var admin= new Administrator
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email
                };

                admin.CreatePasswordWithHash(model.Password);
                _context.Administrators.Add(admin);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) { return new BadRequestObjectResult(ex.Message); }

            return new OkResult();
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
        [HttpPost("signin")]
        public async Task<IActionResult> SignInAdminAsync([FromBody] SignIn signIn)
        {

            try
            {
                var user = await _context.Administrators.FirstOrDefaultAsync(u => u.Email == u.Email);

                if (user != null)
                {
                    if (user.ValidatePasswordHash(signIn.Password))
                    {
                        var th = new JwtSecurityTokenHandler();
                        var expiresDate = DateTime.Now.AddDays(1);

                        var td = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[]
                            {
                                    new Claim("UserId", user.Id.ToString()),
                                    new Claim("Expires", expiresDate.ToString())
                            }),
                            Expires = expiresDate,
                            SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(
                                    Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value)),
                                    SecurityAlgorithms.HmacSha512Signature
                                )
                        };

                        var _accessToken = th.WriteToken(th.CreateToken(td));

                        return new OkObjectResult(_accessToken);
                    }
                }
            }
            catch (Exception ex) { return new BadRequestObjectResult(ex.Message); }

            return new BadRequestResult();


        }
    }

        //    try
        //    {
        //        var user = await _context.Administrators.FirstOrDefaultAsync(u => u.Email == u.Email);

            //        if (user != null)
            //        {
            //            if (user.ValidatePasswordHash(signUp.Password))
            //            {
            //                var th = new JwtSecurityTokenHandler();
            //                var expiresDate = DateTime.Now.AddDays(1);

            //                var td = new SecurityTokenDescriptor
            //                {
            //                    Subject = new ClaimsIdentity(new Claim[]
            //                    {
            //                        new Claim("UserId", user.Id.ToString()),
            //                        new Claim("Expires", expiresDate.ToString())
            //                    }),
            //                    Expires = expiresDate,
            //                    SigningCredentials = new SigningCredentials(
            //                        new SymmetricSecurityKey(
            //                            Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value)),
            //                            SecurityAlgorithms.HmacSha512Signature
            //                        )
            //                };

            //                var _accessToken = th.WriteToken(th.CreateToken(td));

            //                return new OkObjectResult(_accessToken);
            //            }
            //        }
            //    }
            //    catch (Exception ex) { return new BadRequestObjectResult(ex.Message); }

            //    return new BadRequestResult();


            //}




            //}

            //[HttpGet]
            //public async Task<IActionResult> GetAdmin()
            //{
            //    return await _identity.GetAdminAsync();
            //}

        
}
