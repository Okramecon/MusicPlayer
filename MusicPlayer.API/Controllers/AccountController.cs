using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.API.Services;
using MusicPlayer.DAL;
using MusicPlayer.Models;
using MusicPlayer.Models.ResponseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicPlayer.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _service;
        public AccountController(AccountService service)
        {
            _service = service;
        }

        [HttpGet("/accounts/")]
        public async Task<List<AccountResponse>> GetAllAccounts()
        {
            return await _service.GetAllAccounts();
        }
    }
}
