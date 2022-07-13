using System;
using System.Collections.Generic;
using System.Linq;
using DemoPET3.Repository.Models;

namespace DemoPET3.Repository.Repositories
{
    public class AccountUserRepository : IRepository<AccountUser>
    {
        private readonly DemoPEContext _context;

        public AccountUserRepository(DemoPEContext context)
        {
            _context = context;
        }
        public IEnumerable<AccountUser> GetAll()
        {
            throw new NotImplementedException();
        }

        public AccountUser GetById(string id)
        {
            throw new NotImplementedException();
        }

        public void Add(AccountUser entity)
        {
            throw new NotImplementedException();
        }

        public void Update(AccountUser entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }
        
        public AccountUser Login(string userId, string password)
        {
            return _context.AccountUsers
                .FirstOrDefault(a=>a.UserId == userId && a.UserPassword == password);
        }
    }
}