using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.Common.Models.Entity;
using AutoMapper;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.DTOs.Requests;

namespace BusBookTicket.CustomerManage.Services
{
    public class CustomerService : ICustomerService
    {
        #region -- Properties --
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private Customer _customer;
        #endregion --  Properties --

        #region -- Constructor --
        public CustomerService(IMapper mapper,
            IAuthService authService,
            ICustomerRepository customerRepository)
        {
            _mapper = mapper;
            _authService = authService;
            _customerRepository = customerRepository;
        }
        #endregion -- Contructor --

        #region -- Public method --

        /// <summary>
        /// Create Customer and account genera
        /// Call Auth Service to create account and get account.
        /// After create customer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool create(FormRegister entity)
        {
            _customer = new Customer();

            _customer = _mapper.Map<Customer>(entity);

            AuthRequest authRequest = _mapper.Map<AuthRequest>(entity);
            _authService.create(authRequest);
            _customer.account = _authService.getAccByUsername(entity.username);

            return _customerRepository.create(_customer);
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CustomerResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public CustomerResponse getByID(int id)
        {
            throw new NotImplementedException();
        }

        public CustomerResponse update(FormRegister entity)
        {
            throw new NotImplementedException();
        }

        public CustomerResponse update(FormUpdate entity)
        {
            throw new NotImplementedException();
        }
        #endregion -- Public method --
    }
}
