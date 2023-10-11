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

        public bool create(Account entity)
        {
            _status = false;
            try
            {
                entity.password = PassEncrypt.hashPassword(entity.password);
                _context.Add(entity);
                _context.SaveChanges();
                _status = true;
            }
            catch (Exception)
            {
                throw new Exception("Errors when create account");
            }
            _status = true;
            return _status;
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public Account getAccByUsername(string username, string roleName)
        {
            if (roleName == "COMPANY")
            {
                return _account = _context.Accounts.Where(x => x.username == username)
                .Include(x => x.role)
                .Include(x => x.company)
                .FirstOrDefault();
            }
            return _account = _context.Accounts.Where(x => x.username == username)
                .Include(x => x.role)
                .Include(x => x.customer)
                .FirstOrDefault();

        }

        public List<Account> getAll()
        {
            throw new NotImplementedException();
        }

        public Account getByID(int id)
        {
            throw new NotImplementedException();
        }

        public bool login(Account acc)
        {
            _status = false;
            _account = getAccByUsername(acc.username, acc.role.roleName) ?? throw new AuthException(AuthConstants.LOGIN_FAIL) ;
            return PassEncrypt.verifyPassword(acc.password, _account.password);
        }

        public bool update(Account entity)
        {
            throw new NotImplementedException();
        }
        #endregion -- Public Method -- 
    }
}
