using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;

namespace BusBookTicket.CompanyManage.Services;

public class CompanyService : ICompanyServices
{
    #region -- Properties --
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<Company> _repository;
    #endregion

    public CompanyService(
        IMapper mapper, 
        IAuthService authService,
        IUnitOfWork unitOfWork
        )
    {
        this._mapper = mapper;
        this._authService = authService;
        this._unitOfWork = unitOfWork;
        _repository = unitOfWork.GenericRepository<Company>();
    }

    #region -- Public Method --

    public async Task<ProfileCompany> GetById(int id)
    {
        CompanySpecification companySpecification = new CompanySpecification(id);
        Company company = await _repository.Get(companySpecification);
        ProfileCompany profile = _mapper.Map<ProfileCompany>(company);
        return profile;
    }

    public async Task<List<ProfileCompany>> GetAll()
    {
        CompanySpecification companySpecification = new CompanySpecification();
        List<Company> companies = await _repository.ToList(companySpecification);
        List<ProfileCompany> profileCompanies = await AppUtils.MappObject<Company, ProfileCompany>(companies, _mapper);
        return profileCompanies;
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
        CompanySpecification companySpecification = new CompanySpecification(id);
        Company company = await _repository.Get(companySpecification);
        company = changeStatus(company, (int)EnumsApp.Delete);
        await _repository.Update(company, userId);
        return true;
    }

    public async Task<bool> changeStatus(int id, int status)
    {
        CompanySpecification companySpecification = new CompanySpecification(id);
        Company company = await _repository.Get(companySpecification);
        company = changeStatus(company, status);
        await _repository.Update(company, id);
        return true;
    }

    public async Task<bool> Create(FormRegisterCompany entity, int userID)
    {
        Company company = _mapper.Map<Company>(entity);
        AuthRequest account = _mapper.Map<AuthRequest>(entity);

        // Set data
        company.Status = 0;
        await _authService.Create(account, -1);
        company.Account = await _authService.GetAccountByUsername(entity.username, entity.roleName);
        await _repository.Create(company, -1);
        return true;
    }

    #endregion

    #region -- Private Method --

    private Company changeStatus(Company entity, int status)
    {
        entity.Status = status;
        foreach (Bus bus in entity.Buses)
        {
            bus.Status = status;
        }

        entity.Account.Status = status;

        return entity;
    }
    

    #endregion
}