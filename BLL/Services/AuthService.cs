using Microsoft.EntityFrameworkCore;
using MusicPlayer.DAL;
using MusicPlayer.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.Logics
{
    public class AuthService
    {
        private AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public Account AuthenticateAccount(string username, string password)
        {
            var existAccount = _context.Accounts.Include(x => x.Role).FirstOrDefault(a => a.UserName == username && a.Password == password);
            return existAccount;
        }

        public List<Account> GetAllAccounts()
        {
            var accounts = _context.Accounts.ToList();
            return accounts;
        }
    }
}
