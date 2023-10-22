using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;
using BusBookTicket.CompanyManage.DTOs.Requests;
using BusBookTicket.CompanyManage.DTOs.Responses;
using BusBookTicket.CompanyManage.Repositories;

namespace BusBookTicket.CompanyManage.Services;

public class CompanyService : ICompanyServices
{
    #region -- Properties --
    private readonly ICompanyRepos _companyRepos;
    private readonly IMapper _mapper;
    private readonly IAuthService _authService;
    #endregion

    public CompanyService(ICompanyRepos companyRepos, IMapper mapper, IAuthService authService)
    {
        this._mapper = mapper;
        this._companyRepos = companyRepos;
        this._authService = authService;
    }

    #region -- Public Method --

    public async Task<ProfileCompany> getByID(int id)
    {
        Company company = await _companyRepos.getByID(id);
        ProfileCompany profile = _mapper.Map<ProfileCompany>(company);
        return profile;
    }

    public async Task<List<ProfileCompany>> getAll()
    {
        List<Company> companies = await _companyRepos.getAll();
        List<ProfileCompany> profileCompanies = await AppUtils.MappObject<Company, ProfileCompany>(companies, _mapper);
        return profileCompanies;
    }

    public async Task<bool> update(FormUpdateCompany entity, int id)
    {
        Company company = _mapper.Map<Company>(entity);
        company.companyID = id;
        await _companyRepos.update(company);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        Company company = await _companyRepos.getByID(id);
        company = changeStatus(company, (int)EnumsApp.Delete);
        await _companyRepos.delete(company);
        return true;
    }

    public async Task<bool> create(FormRegisterCompany entity)
    {
        Company company = _mapper.Map<Company>(entity);
        AuthRequest account = _mapper.Map<AuthRequest>(entity);

        // Set data
        company.status = 0;
        await _authService.create(account);
        company.account = await _authService.getAccountByUsername(entity.username, entity.roleName);
        await _companyRepos.create(company);
        return true;
    }

    #endregion

    #region -- Private Method --

    private Company changeStatus(Company entity, int status)
    {
        entity.status = status;
        foreach (Bus bus in entity.buses)
        {
            bus.status = status;
        }

        return entity;
    }
    

    #endregion
}