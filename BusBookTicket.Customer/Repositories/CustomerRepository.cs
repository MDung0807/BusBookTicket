using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Models.EntityFW;
using BusBookTicket.CustomerManage.Exceptions;
using BusBookTicket.CustomerManage.Utilitis;
using Microsoft.EntityFrameworkCore;

namespace BusBookTicket.CustomerManage.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        #region -- Properties --
        private readonly AppDBContext _context;
        private bool _status = false;
        #endregion -- Properties --
        #region -- Contructor --
        public CustomerRepository(AppDBContext context)
        {
            _context = context;
        }
        #endregion -- Contructor --

        /// <summary>
        /// Insert Customer to database
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public int create(Customer entity)
        {
            int id;
            try
            {
                id =_context.Add<Customer>(entity).Entity.customerID;
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.ToString());
            }
            return id;
        }

        /// <summary>
        /// Delete Customer in Database (update status)
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int delete(Customer customer)
        {
            int id;
            try
            {
                id = _context.Customers.Update(customer).Entity.customerID;
                _context.SaveChanges();
            }
            catch
            {
                throw new Exception(CusConstants.FAIL);
            }

            return id;
        }
        public List<Customer> getAll()
        {
            List<Customer> customers = new List<Customer>();

            try
            {
                return customers = _context.Set<Customer>()
                    .Where(x => x.account.role.roleName == "CUSTOMER")
                    .Include(x => x.account)
                    .Include(x => x.rank).ToList();
            }
            catch
            {
                throw new Exception("Error In GetAll Customer");
            }
        }

        /// <summary>
        /// Find Customer from id to database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CustomerException"></exception>
        /// <exception cref="Exception"></exception>
        public Customer getByID(int id)
        {
            try
            {
                return _context.Customers.Where(x => x.customerID == id)
                    .Include(x => x.rank)
                    .Include(x => x.account)
                    .Include(x => x.account.role) 
                    .First()?? throw new CustomerException(CusConstants.NOT_FOUND);
            }
            catch 
            {
                throw new Exception("Error");
            }
        }

        /// <summary>
        /// update customer information
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public int update(Customer entity)
        {
            int id;
            try
            {
                id = _context.Customers.Update(entity).Entity.customerID;
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw new Exception($"Fail in update profile: {e.Message}");
            }
            finally
            {
            }

            return id;
        }
    }
}
