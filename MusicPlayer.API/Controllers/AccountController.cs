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
        public AccountController(AppDbContext context)
        {
            _service = new AccountService(context);
        }

        [HttpGet("/accounts/")]
        public async Task<List<AccountResponse>> GetAllAccounts()
        {
            var accounts = await _service.getAllAccounts();

            var config = new MapperConfiguration(cfg =>
                cfg.CreateMap<Account, AccountResponse>()
                .ForMember("RoleName", opt => opt.MapFrom(c => c.Role.Name)));

            var response = new Mapper(config).Map<List<AccountResponse>>(accounts);

            return response;
        }
    }
}
