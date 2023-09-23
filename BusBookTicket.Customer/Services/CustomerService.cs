using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.CustomerManage.Repositories;
using BusBookTicket.Auth.Services;
using BusBookTicket.Common.Models.Entity;
using AutoMapper;

namespace BusBookTicket.CustomerManage.Services
{
    public class CustomerService : ICustomerService
    {
        #region -- var --
        private readonly ICustomerRepository _customerRepository;
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private Customer _customer;
        #endregion --  varr --

        #region -- Constructor --
        public CustomerService(IMapper mapper,
            IAuthService authService,
            Customer customer,
            ICustomerRepository customerRepository)
        {
            _customer = customer;
            _mapper = mapper;
            _authService = authService;
            _customerRepository = customerRepository;
        }
        #endregion -- Contructor --
        public bool create(FormRegister entity)
        {
            _customer = new Customer();
            _customer = _mapper.Map<Customer>(entity);
            _customerRepository.create(_customer);
            throw new NotImplementedException();
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
    }
}
