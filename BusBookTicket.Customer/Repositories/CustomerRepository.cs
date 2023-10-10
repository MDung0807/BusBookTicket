﻿using BusBookTicket.Common.Models.Entity;
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
        public bool create(Customer entity)
        {
            try
            {
                _context.Add<Customer>(entity);
                _status = true;
                _context.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.ToString());
                _status = false;
            }

            return _status;
        }

        /// <summary>
        /// Delete Customer to database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public bool delete(int id)
        {
            throw new NotImplementedException();
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
                    .Include<Customer, Account>(x => x.account)
                    .Include(x => x.account.role)
                    .Include(x => x.rank)
                    .FirstOrDefault() ?? throw new CustomerException(CusConstants.NOT_FOUND);
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
        public Customer update(Customer entity)
        {
            throw new NotImplementedException();
        }
    }
}
