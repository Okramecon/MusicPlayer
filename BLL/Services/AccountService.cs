using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using MusicPlayer.Models;
using MusicPlayer.Models.ResponseModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.BLL.Services
{
    public class AccountService : BaseContextService
    {
        public AccountService(AppDbContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<List<AccountResponse>> GetAllAccounts()
        {
            var accounts = await _context.Accounts.Include(x => x.Role).ToListAsync();
            return _mapper.Map<List<AccountResponse>>(accounts);
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
