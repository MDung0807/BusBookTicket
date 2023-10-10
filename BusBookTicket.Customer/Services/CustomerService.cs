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
        /// Get all customer by admin
        /// </summary>
        /// <returns></returns>
        public List<CustomerResponse> getAll()
        {
            List<Customer> customers = new List<Customer>();
            List<CustomerResponse> responses = new List<CustomerResponse>();
            customers = _customerRepository.getAll();
            foreach(Customer customer in customers)
            {
                responses.Add(_mapper.Map<CustomerResponse>(customer));
            }
            return responses;
        }

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
            _customer.account = _authService.getAccountByUsername(entity.username, entity.roleName);

            return _customerRepository.create(_customer);
        }

        public bool delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProfileResponse> GetAll()
        {
            throw new NotImplementedException();
        }

        public ProfileResponse getByID(int id)
        {
            _customer = _customerRepository.getByID(id);
            return _mapper.Map<ProfileResponse>(_customer);
        }

        public ProfileResponse update(FormRegister entity)
        {
            throw new NotImplementedException();
        }

        public ProfileResponse update(FormUpdate entity)
        {
            throw new NotImplementedException();
        }
        #endregion -- Public method --
    }
}
