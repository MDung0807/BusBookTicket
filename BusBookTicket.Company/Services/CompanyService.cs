using AutoMapper;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.MailKet.DTO.Request;
using BusBookTicket.Application.MailKet.Service;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Paging;
using BusBookTicket.CompanyManage.Repository;
using BusBookTicket.CompanyManage.Specification;
using BusBookTicket.CompanyManage.Utils;
using BusBookTicket.Core.Common.Exceptions;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.CompanyManage.Services;

public class CompanyService : ICompanyServices
{
    #region -- Properties --
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Company> _repository;
    private readonly IImageService _imageService;
    private readonly INotificationService _notificationService;
    private readonly IMailService _mailService;
    private readonly ICompanyRepository _companyRepository;

    #endregion

    public CompanyService(
        IMapper mapper, 
        IAuthService authService,
        IUnitOfWork unitOfWork,
        IImageService imageService, INotificationService notificationService, IMailService mailService, ICompanyRepository companyRepository)
    {
        this._mapper = mapper;
        this._authService = authService;
        this._unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<Company>();
        _imageService = imageService;
        _notificationService = notificationService;
        _mailService = mailService;
        _companyRepository = companyRepository;
    }

    #region -- Public Method --

    public async Task<ProfileCompany> GetById(int id)
    {
        CompanySpecification companySpecification = new CompanySpecification(id);
        Company company = await _repository.Get(companySpecification);
        ProfileCompany profile = _mapper.Map<ProfileCompany>(company);
        List<string> images = await _imageService.getImages(typeof(Company).ToString(), company.Id);
        profile.Logo = (images.Count> 0 ? images[0]: null)!;
        return profile;
    }

    public async Task<List<ProfileCompany>> GetAll()
    {
        CompanySpecification companySpecification = new CompanySpecification();
        List<Company> companies = await _repository.ToList(companySpecification);
        List<ProfileCompany> profileCompanies = await AppUtils.MapObject<Company, ProfileCompany>(companies, _mapper);
        return profileCompanies;
    }
    
    public async Task<CompanyPagingResult> GetAllByAdmin(CompanyPaging paging)
    {
        CompanySpecification companySpecification = new CompanySpecification( checkStatus: false, paging: paging);
        List<Company> companies = await _repository.ToList(companySpecification);
        List<ProfileCompany> profileCompanies = await AppUtils.MapObject<Company, ProfileCompany>(companies, _mapper);
        int count = await _repository.Count(companySpecification);
        return AppUtils.ResultPaging<CompanyPagingResult, ProfileCompany>(
            index: paging.PageIndex, size: paging.PageSize, count: count, items: profileCompanies);
    }

    public Task<CompanyPagingResult> GetAll(CompanyPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyPagingResult> GetAll(CompanyPaging pagingRequest, int idMaster, bool checkStatus = false)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<CompanyPagingResult> FindByParam(string param, CompanyPaging pagingRequest = default, bool checkStatus = true)
    {
        CompanySpecification specification = new CompanySpecification(),
            specificationNotPaging = new CompanySpecification();
        
        specificationNotPaging.FindByParam(param, checkStatus: checkStatus);
        specification.FindByParam(param: param, paging: pagingRequest, checkStatus: checkStatus);

        var result = await _repository.ToList(specification);
        int count = await _repository.Count(specificationNotPaging);

        return AppUtils.ResultPaging<CompanyPagingResult, ProfileCompany>(
            pagingRequest.PageIndex, pagingRequest.PageSize, count,
            await AppUtils.MapObject<Company, ProfileCompany>(result, _mapper));
    }

    public async Task<object> StatisticalCompany(DateTime dateTime)
    {
        var totalCompany = await TotalCompany(dateTime);
        var rate = await RateCompany(dateTime);

        return new
        {
            totalCompany, rate
        };
    }


    public async Task<bool> Update(FormUpdateCompany entity, int id, int userId)
    {
        Company company = _mapper.Map<Company>(entity);
        company.Id = id;
        await _repository.Update(company, userId);
        return true;
    }

    public async Task<bool> Delete(int id, int userId)
    {
        CompanySpecification companySpecification = new CompanySpecification(id, checkStatus:false, getAll:false);
        Company company = await _repository.Get(companySpecification, checkStatus: false);
        company = ChangeStatus(company, (int)EnumsApp.Delete);
        await _repository.Update(company, userId);
        return true;
    }

    public async Task<bool> Create(FormRegisterCompany entity, int userID)
    {
        Company company = _mapper.Map<Company>(entity);
        AuthRequest account = _mapper.Map<AuthRequest>(entity);
        await _unitOfWork.BeginTransaction();
        try
        {
            // Set data
            company.Status = (int)EnumsApp.Lock;
            await _authService.Create(account, -1);
            company.Account = await _authService.GetAccountByUsername(entity.Username, entity.RoleName, false);
            company = await _repository.Create(company, -1);
            
            // Save Image
            await _imageService.saveImage(entity.Logo, typeof(Company).ToString(), company.Id);
            string content = $"{company.Name} đã tạo tài khoản";
            await SendNotification(content, $"{AppConstants.ADMIN}_1", company.Name, AppConstants.REGISTERTYPE, company.Id );
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e);
            throw new ExceptionDetail(CompanyConstants.ERROR_CREATE);
        }
    }

    public async Task<bool> ChangeIsActive(int id, int userId)
    {
        CompanySpecification companySpecification = new CompanySpecification(id, checkStatus: false, getAll:false);
        Company company = await _repository.Get(companySpecification, checkStatus: false);
        await _unitOfWork.BeginTransaction();
        try
        {
            await _repository.ChangeStatus(company, userId, (int)EnumsApp.Active);
            // Send mail with OTP code
            MailRequest mailRequest = new MailRequest();
            mailRequest.ToMail = company.Email;
            mailRequest.Content = "Your Account has been verified";
            mailRequest.FullName = company.Name;
            mailRequest.Subject = "Account has been verified";
            mailRequest.Message = "Your Account has been verified";
            await _mailService.SendEmailAsync(mailRequest);
            await _unitOfWork.SaveChangesAsync();
            return true;
        }
        catch (Exception e)
        {
            await _unitOfWork.RollbackTransactionAsync();
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> ChangeIsLock(int id, int userId)
    {
        CompanySpecification companySpecification = new CompanySpecification(id, checkStatus: false, getAll:false);
        Company company = await _repository.Get(companySpecification, checkStatus: false);

        return await _repository.ChangeStatus(company, userId, (int)EnumsApp.Lock);
    }

    public Task<bool> ChangeToWaiting(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeToWaiting(List<int> ids, int userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> ChangeStatus(List<int> ids, int userId)
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

    #endregion

    #region -- Private Method --

    private async Task<int> TotalCompany(DateTime dateTime)
    {
        return await _companyRepository.TotalCompany(dateTime);
    }

    private async Task<decimal> RateCompany(DateTime dateTime)
    {
        return await _companyRepository.Rate(dateTime);
    }

    private Company ChangeStatus(Company entity, int status)
    {
        entity.Status = status;
        foreach (Bus bus in entity.Buses)
        {
            bus.Status = status;
        }

        entity.Account.Status = status;

        return entity;
    }

    private async Task SendNotification(string content, string actor, string sender, string href, int userId)
    {
        AddNewNotification newNotification = new AddNewNotification
        {
            Content = content,
            Actor = actor,
            Href = href,
            Sender = sender
        };
        await _notificationService.InsertNotification(newNotification, userId);
    }

    #endregion
}