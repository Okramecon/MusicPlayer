using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Models.RequestModels;
using MusicPlayer.Authtificate;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Logics;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IOptions<AuthOptions> _authOptions;
        private AppDbContext _context;
        private AuthService _service;
        public AuthController(IOptions<AuthOptions> authOptions, AppDbContext context)
        {
            _authOptions = authOptions;
            _context = context;
            _service = new AuthService(context);
        }

        [HttpPost("/login/")]
        public IActionResult Login([FromBody] LoginRequest req)
        {
            var existAccount = _service.AuthenticateAccount(req.UserName, req.Password);

            if (existAccount != null)
            {
                var token = GenerateJWT(existAccount);

                return Ok(new
                {
                    token
                });
            }

            return BadRequest("User not found");
        }

        private string GenerateJWT(Account account)
        {
            var authParams = _authOptions.Value;

            var securityKey = authParams.GetSymmetricSecurityKey();
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Name, account.UserName),
                new Claim(JwtRegisteredClaimNames.Sub, account.Id.ToString()),
                new Claim("role", account.Role.Name)
            };

            var token = new JwtSecurityToken(
                authParams.Issuer,
                authParams.Audience,
                claims,
                expires: DateTime.Now.AddSeconds(authParams.TokenLifeTime),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
