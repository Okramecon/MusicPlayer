using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public class AccountService
    {
        private readonly AppDbContext _context;

        public AccountService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> getAllAccounts()
        {
            return await _context.Accounts.Include(x => x.Role).ToListAsync();
        }
    }
}
