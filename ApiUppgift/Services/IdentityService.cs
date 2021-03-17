using ApiUppgift.Models;
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

namespace ApiUppgift.Services
{
    public class IdentityService : IIdentityService
    {
        private readonly CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext _context;
        private IConfiguration _configuration { get; }
        public IdentityService(CUSERSJRMAGGISOURCEREPOSAPIUPPGIFTAPIUPPGIFTDATABASESQLDBMDFContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        public async Task<bool> CreateAdminAsync(SignUp model)
        {
            if (!_context.Administrators.Any(x => x.Email == model.Email))
            {
                try
                {
                    var admin = new Administrator()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        Email = model.Email
                    };
                    admin.CreatePasswordWithHash(model.Password);
                    _context.Administrators.Add(admin);
                    await _context.SaveChangesAsync();

                    return true;
                }
                catch
                {

                }
            }

            return false;
        }

        public async Task<SignInResponseModel> SignInAdminAsync(string email, string password)
        {

            try
            {
                var admin = await _context.Administrators.FirstOrDefaultAsync(admin => admin.Email == email);

                if (admin != null)
                {
                    try
                    {
                        if (admin.ValidatePasswordHash(password))
                        {
                            var tokenHandler = new JwtSecurityTokenHandler();
                            var _secretKey = Encoding.UTF8.GetBytes(_configuration.GetSection("SecretKey").Value);

                            var tokenDescriptor = new SecurityTokenDescriptor
                            {

                                Subject = new ClaimsIdentity(new Claim[] { new Claim("AdminId", admin.Id.ToString()) }),
                                Expires = DateTime.Now.AddHours(1),
                                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_secretKey), SecurityAlgorithms.HmacSha512Signature)

                            };

                            var _accessToken = tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));

                            return new SignInResponseModel
                            {
                                Succeded = true, Result = new SignInResponseResut {
                                    Id = admin.Id,
                                    Email = admin.Email,
                                    AccessToken =_accessToken
                                }
                            };
                        }

                        
                    }

                    catch { }
                }
            }
            catch { }
         
            return new SignInResponseModel
            {
                Succeded = false
            };
            




           
        }
    }
}
