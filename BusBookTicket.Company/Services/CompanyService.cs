using AutoMapper;
using BusBookTicket.Auth.DTOs.Requests;
using BusBookTicket.Auth.Services.AuthService;
using BusBookTicket.Common.Models.Entity;
using BusBookTicket.Common.Utils;
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

    public ProfileCompany getByID(int id)
    {
        Company company = _companyRepos.getByID(id);
        ProfileCompany profile = _mapper.Map<ProfileCompany>(company);
        return profile;
    }

    public List<ProfileCompany> getAll()
    {
        List<ProfileCompany> profileCompanies = new List<ProfileCompany>();
        List<Company> companies = new List<Company>();

        companies = _companyRepos.getAll();
        profileCompanies = AppUtils.MappObject<Company, ProfileCompany>(companies, _mapper);
        return profileCompanies;
    }

    public bool update(FormUpdateCompany entity, int id)
    {
        Company company = _mapper.Map<Company>(entity);
        company.companyID = id;
        return _companyRepos.update(company);
    }

    public bool delete(int id)
    {
        Company company = _companyRepos.getByID(id);
        company = changeStatus(company, (int)EnumsApp.Delete);
        return _companyRepos.delete(company);
    }

    public bool create(FormRegisterCompany entity)
    {
        Company company = _mapper.Map<Company>(entity);
        AuthRequest account = _mapper.Map<AuthRequest>(entity);

        // Set data
        company.status = 0;
        _authService.create(account);
        company.account = _authService.getAccountByUsername(entity.username, entity.roleName);
        return _companyRepos.create(company);
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