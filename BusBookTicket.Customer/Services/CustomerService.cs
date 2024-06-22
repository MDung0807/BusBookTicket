using BusBookTicket.CustomerManage.DTOs.Requests;
using BusBookTicket.CustomerManage.DTOs.Responses;
using BusBookTicket.Core.Models.Entity;
using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Responses;
using BusBookTicket.AddressManagement.DTOs.Responses.Ward;
using BusBookTicket.AddressManagement.Services.WardService;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.AddressManagement.Utilities;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Application.OTP.Models;
using BusBookTicket.Application.OTP.Services;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Utils;
using BusBookTicket.CustomerManage.Paging;
using BusBookTicket.CustomerManage.Specification;
using BusBookTicket.CustomerManage.Utilities;

namespace BusBookTicket.CustomerManage.Services
{
    public class CustomerService : ICustomerService
    {
        #region -- Properties --
        private readonly IMapper _mapper;
        private readonly IAuthService _authService;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Customer> _repository;
        private readonly IWardService _wardService;
        private readonly IOtpService _otpService;
        private readonly IMailService _mailService;
        private readonly IGenericRepository<Ward> _wardRepo;

        #endregion --  Properties --

        #region -- Constructor --
        public CustomerService(IMapper mapper,
            IAuthService authService,
            IImageService imageService,
            IUnitOfWork unitOfWork,
            IWardService wardService,
            IOtpService opOtpService,
            IMailService mailService)
        {
            _mapper = mapper;
            _authService = authService;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
            _repository = _unitOfWork.GenericRepository<Customer>();
            _wardService = wardService;
            _otpService = opOtpService;
            _mailService = mailService;
            _wardRepo = unitOfWork.GenericRepository<Ward>();
        }
        #endregion -- Contructor --

        #region -- Public method --

        /// <summary>
        /// Create Customer and account genera
        /// Call Auth Service to create account and get account.
        /// After create customer
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<bool> Create(FormRegister entity, int userId)
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                CustomerSpecification specification = new CustomerSpecification(entity.Email, checkStatus:false, isDelete:true);
                Customer customer = new Customer();
                customer = await _repository.Get(specification);
                if (customer != null)
                {
                    await _repository.DeleteHard(customer);
                    await _authService.DeleteHard(customer.Account.Id);
                }
                customer = _mapper.Map<Customer>(entity);

                // Set Full data in form regisger
                customer.DateCreate = DateTime.Now;
                customer.DateCreate = DateTime.Now;

                // Get account
                AuthRequest authRequest = _mapper.Map<AuthRequest>(entity);
                await _authService.Create(authRequest, -1);
                customer.Account = await _authService.GetAccountByUsername(entity.Username, entity.RoleName, checkStatus:false);
                customer.Status = (int)EnumsApp.Waiting;
                await _repository.Create(customer, -1);

                // Save Image
                await _imageService.saveImage(entity.Avatar, typeof(Customer).ToString(), customer.Id);

                OtpResponse response = await _otpService.CreateOtp(new OtpRequest(email: customer.Email), userId: customer.Id);
                
                // Send mail with OTP code
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToMail = customer.Email;
                mailRequest.Content = "OTP code";
                mailRequest.FullName = customer.FullName;
                mailRequest.Subject = "Authentication OTP code";
                mailRequest.Message = response.Code;
                await _mailService.SendEmailAsync(mailRequest);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> ChangeIsActive(int id, int userId)
        {
            CustomerSpecification customerSpecification = new CustomerSpecification(id, false, getAll:false);
            Customer customer = await _repository.Get(customerSpecification, checkStatus: false);
            return await _repository.ChangeStatus(customer, userId: userId, (int)EnumsApp.Active);
        }

        public async Task<bool> ChangeIsLock(int id, int userId)
        {
            CustomerSpecification customerSpecification = new CustomerSpecification(id, false, getAll:false);
            Customer customer = await _repository.Get(customerSpecification, checkStatus: false);
            return await _repository.ChangeStatus(customer, userId: userId, (int)EnumsApp.Lock);
        }

        public Task<bool> ChangeToWaiting(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeToDisable(int id, int userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CheckToExistByParam(string param)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerPagingResult> GetAllByAdmin(CustomerPaging pagingRequest)
        {
            List<Customer> customers = new List<Customer>();
            List<CustomerResponse> responses = new List<CustomerResponse>();
            CustomerSpecification specification = new CustomerSpecification(paging:pagingRequest);
            customers = await _repository.ToList(specification);
            int count = await _repository.Count(new CustomerSpecification());
            foreach(Customer customer in customers)
            {
                responses.Add(_mapper.Map<CustomerResponse>(customer));
            }

            CustomerPagingResult result =
                AppUtils.ResultPaging<CustomerPagingResult, CustomerResponse>(pagingRequest.PageIndex,
                    pagingRequest.PageSize, count, responses);
            result.Items = responses;
            result.PageIndex = pagingRequest.PageIndex;
            result.PageSize = pagingRequest.PageSize;
            result.PageTotal = (int)Math.Round((decimal)count / pagingRequest.PageSize);
            return result;
        }

        public Task<CustomerPagingResult> GetAll(CustomerPaging pagingRequest)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerPagingResult> GetAll(CustomerPaging pagingRequest, int idMaster, bool checkStatus = false)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteHard(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CustomerPagingResult> GetAllCustomer(CustomerPaging paging)
        {
            CustomerSpecification specification = new CustomerSpecification(paging, false);
            List<Customer> customers = await _repository.ToList(specification);
            int count = await _repository.Count(new CustomerSpecification(checkStatus:false));
            CustomerPagingResult result = AppUtils.ResultPaging<CustomerPagingResult, CustomerResponse>(
                paging.PageIndex, paging.PageSize, count,
                await AppUtils.MapObject<Customer, CustomerResponse>(customers, _mapper));
            return result;
        }

        public async Task<bool> AuthOtp(OtpRequest request)
        {
            CustomerSpecification customerSpecification = new CustomerSpecification(request.Email, checkStatus:false);
            Customer customer = await _repository.Get(customerSpecification, checkStatus: false);
            if (! await _otpService.AuthenticationOtp(request, customer.Id))
            {
                throw new ExceptionDetail(CusConstants.OTP_NOT_AUTH);
            }
            return await ChangeIsActive(customer.Id, customer.Id);
        }

        public async Task<bool> CreateByGoogle(FormRegister register)
        {
            await _unitOfWork.BeginTransaction();
            try
            {
                CustomerSpecification specification = new CustomerSpecification(register.Email, checkStatus:false, isDelete:true);
                Customer customer = new Customer();
                customer = await _repository.Get(specification);
                if (customer != null)
                {
                    await _repository.DeleteHard(customer);
                    await _authService.DeleteHard(customer.Account.Id);
                }
                customer = _mapper.Map<Customer>(register);

                // Set Full data in form regisger
                customer.DateCreate = DateTime.Now;
                customer.DateCreate = DateTime.Now;

                // Get account
                AuthRequest authRequest = _mapper.Map<AuthRequest>(register);
                await _authService.Create(authRequest, -1);
                customer.Account = await _authService.GetAccountByUsername(register.Username, register.RoleName, checkStatus:false);
                customer.Status = (int)EnumsApp.Waiting;
                customer.Ward = null;
                await _repository.Create(customer, -1);

                // Save Image
                
                // Send mail with OTP code
                MailRequest mailRequest = new MailRequest();
                mailRequest.ToMail = customer.Email;
                mailRequest.Content = "Bây giờ bạn đã là thành viên của chúng tôi, yêu bạn nhiều lắm";
                mailRequest.FullName = customer.FullName;
                mailRequest.Subject = "Chào mừng quý khách";
                await _mailService.SendEmailAsync(mailRequest);
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                await _unitOfWork.RollbackTransactionAsync();
                return false;
            }
        }

        public async Task<bool> Delete(int id, int userId)
        {
            CustomerSpecification customerSpecification = new CustomerSpecification(id, false, getAll:false);
            Customer customer = await _repository.Get(customerSpecification, checkStatus: false);
            return await _repository.ChangeStatus(customer, userId: userId, (int)EnumsApp.Delete);
        }

        public async Task<ProfileResponse> GetById(int id)
        {
            CustomerSpecification specification = new CustomerSpecification(id);
            Customer customer = await _repository.Get(specification);
            ProfileResponse response = _mapper.Map<ProfileResponse>(customer);
            response.AddressResponse = _mapper.Map<AddressResponse>(await AddressUtils.GetFullAddressDb(customer.Ward.Id, _wardService));
            List<string> images = await _imageService.getImages(typeof(Customer).ToString(), id);
            if (images.Count > 0)
            {
                response.Avatar = images[0];
            }
            return response;
        }

        public Task<List<ProfileResponse>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Update(FormUpdate entity, int id, int userId)
        {
            Customer customer = new Customer();
            
            customer = _mapper.Map<Customer>(entity);
            customer.Id = userId;
            customer.Status = (int)EnumsApp.Active;
            WardSpecification wardSpecification = new WardSpecification(customer.Ward.Id);
            customer.Ward = await _wardRepo.Get(wardSpecification);
            await _repository.Update(customer, userId);
            return true;
        }
        #endregion -- Public method --

        #region -- Private Method --

        private Customer setStatus(Customer customer, int status)
        {
            customer.Status = customer.Status != null ? status : customer.Status;
            customer.Account.Status = customer.Account.Status != null ? status: customer.Account.Status;
            return customer;
        }
        #endregion
    }
}
