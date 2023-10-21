using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.Common.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Auth.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        #region -- PropMerties --
        private readonly AppDBContext _context;
        private bool _status;
        private Account _account;
        #endregion -- Properties --

        public AuthRepository(AppDBContext context)
        {
            _context = context;
        }

        #region -- Public Method --

        public async Task<int> create(Account entity)
        {
            try
            {
                entity.password = PassEncrypt.hashPassword(entity.password);
                int id = _context.AddAsync(entity).Result.Entity.accountID;
                await _context.SaveChangesAsync();
                return  id;
            }
            catch (Exception)
            {
                throw new Exception("Errors when create account");
            }
        }

        public Task<int> delete(Account id)
        {
            throw new NotImplementedException();
        }

        public async Task<Account> getAccByUsername(string username, string roleName)
        {
            if (roleName == "COMPANY")
            {
                return await _context.Accounts.Where(x => x.username == username)
                .Include(x => x.role)
                .Include(x => x.company)
                .FirstAsync();
            }
            return await _context.Accounts.Where(x => x.username == username)
                .Include(x => x.role)
                .Include(x => x.customer)
                .FirstAsync();

        }

        public Task<List<Account>> getAll()
        {
            throw new NotImplementedException();
        }

        public Task<Account> getByID(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> login(Account acc)
        {
            _account = await getAccByUsername(acc.username, acc.role.roleName) ?? throw new AuthException(AuthConstants.LOGIN_FAIL) ;
            return PassEncrypt.VerifyPassword(acc.password, _account.password);
        }

        public Task<int> update(Account entity)
        {
            throw new NotImplementedException();
        }
        #endregion -- Public Method -- 
    }
}
