using Microsoft.AspNetCore.Mvc;
using Models.RequestModels;
using MusicPlayer.Logics;
using MusicPlayer.Models;
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

        [HttpPost("login/")]
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

        [HttpPost("registration/")]
        public async Task Register(AddAccountModel model)
        {
            try
            {
                await _service.Register(model);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
