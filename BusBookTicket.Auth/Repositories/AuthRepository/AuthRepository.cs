using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Exceptions;
using BusBookTicket.Auth.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Models.EntityFW;
using BusBookTicket.Core.Utils;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.Auth.Repositories.AuthRepository
{
    public class AuthRepository : IAuthRepository
    {
        #region -- PropMerties --
        private readonly AppDBContext _context;
        private bool _status;
        #endregion -- Properties --

        public AuthRepository(AppDBContext context)
        {
            _context = context;
        }

        #region -- Public Method --

        public async Task<int> resetPass(FormResetPass request)
        {
            try
            {
                Account account = await _context.Accounts.Where(x => x.username == request.username)
                    .Include(x => x.customer)
                    .FirstAsync() ?? throw new NotFoundException(AuthConstants.NOT_FOUND);

                if (account.customer.email == request.email && account.customer.phoneNumber == request.phoneNumber)
                {
                    account.password = request.password;
                    return await update(account);
                }
                throw new NotFoundException(AuthConstants.NOT_FOUND);
            }
            catch
            {
                throw new Exception(AuthConstants.ERROR);
            }
        }

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
            try
            {
                if (roleName == "COMPANY")
                {
                    return await _context.Accounts.Where(x => x.username == username)
                        .Include(x => x.role)
                        .Include(x => x.company)
                        .FirstOrDefaultAsync() ?? throw new NotFoundException(AuthConstants.NOT_FOUND);
                }

                return await _context.Accounts.Where(x => x.username == username)
                    .Include(x => x.role)
                    .Include(x => x.customer)
                    .FirstOrDefaultAsync() ?? throw new NotFoundException(AuthConstants.NOT_FOUND);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                throw new Exception(AuthConstants.ERROR);
            }

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
            try
            {
                Account account = await getAccByUsername(acc.username, acc.role.roleName);
                                  
                return PassEncrypt.VerifyPassword(acc.password, account.password);
            }
            catch
            {
                throw new AuthException(AuthConstants.LOGIN_FAIL);
            }
            
        }

        public async Task<int> update(Account entity)
        {
            _context.Update<Account>(entity);
            return await _context.SaveChangesAsync();
        }
        #endregion -- Public Method -- 
    }
}
