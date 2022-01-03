using Microsoft.EntityFrameworkCore;
using MusicPlayer.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MusicPlayer.API.Services
{
    public class AccountService
    {
        private readonly DbBaseContext _context;

        public AccountService(DbBaseContext context)
        {
            _context = context;
        }

        public async Task<List<Account>> getAllAccounts()
        {
            return await _context.Accounts.Include(x => x.Role).ToListAsync();
        }
    }
}
