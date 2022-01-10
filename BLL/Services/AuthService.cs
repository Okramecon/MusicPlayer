using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MusicPlayer.Authtificate;
using MusicPlayer.BLL.Services;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models;
using MusicPlayer.Models.Models.AuthModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MusicPlayer.Logics
{
    public class AuthService : BaseContextService
    {
        private readonly AuthOptions _authOptions;

        public AuthService(AppDbContext context, IOptions<AuthOptions> options, IMapper mapper) : base(context, mapper)
        {
            _authOptions = options.Value;
        }

        public Account AuthenticateAccount(string username, string password)
        {
            var existAccount = _context.Accounts.Include(x => x.Role).FirstOrDefault(a => a.UserName == username && a.Password == password);
            return existAccount;
        }

        // very simple auth
        public async Task<ResponseAuthModel> Authenticate(string username, string password)
        {
            var user = await _context.Accounts.Include(a => a.Role).FirstOrDefaultAsync(a => a.UserName == username);
            var passwordIsMatch = password == user.Password;
            if (!passwordIsMatch)
            {
                throw new Exception("Incorrect password!");
            }

            var now = DateTime.UtcNow;
            var expireDate = now.Add(TimeSpan.FromSeconds(_authOptions.TokenLifeTime));

            var claims = new List<Claim>
            {
                new(ClaimsIdentity.DefaultNameClaimType, user.UserName),
                new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
            };

            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: now,
                    claims: claims,
                    expires: now.Add(TimeSpan.FromMinutes(_authOptions.TokenLifeTime)),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new ResponseAuthModel()
            {
                AccessToken = encodedJwt,
                ExpireDate = expireDate,
                RoleName = user.Role.Name,
                UserName = user.UserName
            };
        }

        public async Task Register(AddAccountModel model)
        {
            var userNameAlreadyExist = await _context.Accounts.AnyAsync(a => a.UserName == model.UserName);
            if (userNameAlreadyExist)
                throw new System.Exception("Account with same username already exist.");

            var account = _mapper.Map<Account>(model);
            account.RoleId = 2;
            await _context.Accounts.AddAsync(account);
            await _context.SaveChangesAsync();
        }
    }
}
