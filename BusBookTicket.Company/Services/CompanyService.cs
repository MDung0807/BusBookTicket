using AutoMapper;
using BusBookTicket.Application.CloudImage.Services;
using BusBookTicket.Application.Notification.Modal;
using BusBookTicket.Application.Notification.Services;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Paging;
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
    #endregion

    public CompanyService(
        IMapper mapper, 
        IAuthService authService,
        IUnitOfWork unitOfWork,
        IImageService imageService, INotificationService notificationService)
    {
        this._mapper = mapper;
        this._authService = authService;
        this._unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<Company>();
        _imageService = imageService;
        _notificationService = notificationService;
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
        CompanySpecification companySpecification = new CompanySpecification(false);
        List<Company> companies = await _repository.ToList(companySpecification);
        List<ProfileCompany> profileCompanies = await AppUtils.MapObject<Company, ProfileCompany>(companies, _mapper);
        int count = await _repository.Count(companySpecification);
        CompanyPagingResult result = new CompanyPagingResult();
        result.PageIndex = paging.PageIndex;
        result.PageSize = paging.PageSize;
        result.PageTotal = (int)Math.Round((decimal)count / paging.PageSize);
        result.Items = profileCompanies;
        return result;
    }

    public Task<CompanyPagingResult> GetAll(CompanyPaging pagingRequest)
    {
        throw new NotImplementedException();
    }

    public Task<CompanyPagingResult> GetAll(CompanyPaging pagingRequest, int idMaster)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteHard(int id)
    {
        throw new NotImplementedException();
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
            await _unitOfWork.SaveChangesAsync();
            _unitOfWork.Dispose();
            await SendNotification(company);
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

        return await _repository.ChangeStatus(company, userId, (int)EnumsApp.Active);
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

    private async Task SendNotification(Company company)
    {
        AddNewNotification newNotification = new AddNewNotification
        {
            Content = $"{company.Name} Registered",
            Actor = "ADMIN_1",
            Href = "",
            Sender = $"COMPANY_{company.Id}"
        };
        await _notificationService.InsertNotification(newNotification, company.Id);
    }

    #endregion
}