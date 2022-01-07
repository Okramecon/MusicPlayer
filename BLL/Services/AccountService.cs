using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
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
    }
}
