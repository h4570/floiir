using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Services.Internal
{
    public class UserService
    {

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
        }

        public bool EmailExists(string email)
        {
            var userWithSameEmail = _context.Users.SingleOrDefault(c => c.Email.ToLower().Equals(email.ToLower()));
            return userWithSameEmail != null;
        }

        public bool LoginExists(string login)
        {
            var userWithSameLogin = _context.Users.SingleOrDefault(c => c.Login.ToLower().Equals(login.ToLower()));
            return userWithSameLogin != null;
        }

    }
}
