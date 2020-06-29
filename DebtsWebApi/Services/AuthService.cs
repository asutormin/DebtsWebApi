using DebtsWebApi.DAL;
using DebtsWebApi.Entities;
using DebtsWebApi.Helpers;
using DebtsWebApi.Interfaces;
using DebtsWebApi.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DebtsWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppSettings _appSettings;
        private readonly DebtStatisticContext _context;

        public AuthService(IOptions<AppSettings> appSettings, DebtStatisticContext context)
        {
            _appSettings = appSettings.Value;
            _context = context;
        }

        public AuthResult Authenticate(string login, string password)
        {
            var user = GetUser(login, password).Result.Value;

            // return null if user not found
            if (user == null)
                return null;

            var authUser = user.ToAuthUser();

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, authUser.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            authUser.Token = tokenHandler.WriteToken(token);

            return authUser;
        }

        private async Task<ActionResult<User>> GetUser(string login, string password)
        {
            var passwordEncryptor = new Md5BasedEncryptor();

            var user = TryActiveDirectoryAuthentificate(login, password)
               ? await AuthenticateByLdapLogin(login)
               : await AuthenticateByUserLogin(login, passwordEncryptor.Encrypt(password));
            
            return user;
        }

        private bool TryActiveDirectoryAuthentificate(string login, string password)
        {
            using (var directoryEntry = new DirectoryEntry("LDAP://local", login, password, AuthenticationTypes.Secure))
            {
                try
                {
#if DEBUG
                    return true;
#else
                    return !string.IsNullOrEmpty(directoryEntry.Guid.ToString());
#endif
                }
                catch (DirectoryServicesCOMException)
                {
                    return false;
                }
            }
        }

        private async Task<ActionResult<User>> AuthenticateByLdapLogin(string login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.LdapLogin == login);

            return user;
        }

        private async Task<ActionResult<User>> AuthenticateByUserLogin(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserLogin == login && u.UserPassword == password);

            return user;
        }
    }
}
