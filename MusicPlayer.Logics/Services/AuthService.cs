using Models.RequestModels;
using MusicPlayer.API;
using MusicPlayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicPlayer.Logics
{
    public class AuthService
    {
        private static DbBaseContext _context { get; set; }

        public AuthService(DbBaseContext context)
        {
            _context = context;
        }

        public Account AuthenticateAccount(string username, string password)
        {
            var existAccount = _context.Accounts.FirstOrDefault(a => a.UserName == username && a.Password == password);
            return existAccount;
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}
