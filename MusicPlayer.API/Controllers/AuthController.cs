using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using MusicPlayer.Logics;
using MusicPlayer.Models.Models.AuthModels;
using System;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _service;
        public AuthController(AuthService service)
        {
            _service = service;
        }

        [HttpPost("")]
        public async Task<ResponseAuthModel> Login(LoginRequest req)
        {
            try
            {
                return await _service.Authenticate(req.UserName, req.Password);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
