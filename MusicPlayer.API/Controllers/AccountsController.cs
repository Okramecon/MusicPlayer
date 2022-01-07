using Microsoft.AspNetCore.Mvc;
using MusicPlayer.BLL.Services;
using MusicPlayer.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountsController(AccountService service)
        {
            _service = service;
        }

        [HttpGet("")]
        public async Task<List<AccountResponse>> GetAllAccounts()
        {
            return await _service.GetAllAccounts();
        }
    }
}
