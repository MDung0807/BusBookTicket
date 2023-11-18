﻿using AutoMapper;
using BusBookTicket.AddressManagement.DTOs.Requests.Region;
using BusBookTicket.AddressManagement.DTOs.Responses.Province;
using BusBookTicket.AddressManagement.DTOs.Responses.Region;
using BusBookTicket.AddressManagement.Specification;
using BusBookTicket.Core.Infrastructure.Interfaces;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.AddressManagement.Services;

public class RegionService : IRegionService
{
    #region -- Properties --

    private readonly IUnitOfWork _unitOfWork;
    private readonly IGenericRepository<AdministrativeRegion> _repository;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public RegionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _repository = unitOfWork.GenericRepository<AdministrativeRegion>();
    }

    #region -- Public Method --

    public async Task<RegionResponse> GetById(int id)
    {
        RegionSpecification regionSpecification = new RegionSpecification(id);
        AdministrativeRegion region = await _repository.Get(regionSpecification);
        RegionResponse response = new RegionResponse();
        response.Name = region.Name;
        response.Provinces = await AppUtils.MappObject<Province, ProvinceResponse>(region.Provinces.ToList(), _mapper);
        return response;
    }

    public async Task<List<RegionResponse>> GetAll()
    {
        RegionSpecification regionSpecification = new RegionSpecification();
        List<AdministrativeRegion> regions = await _repository.ToList(regionSpecification);
        return await AppUtils.MappObject<AdministrativeRegion, RegionResponse>(regions, _mapper);
    }

    public Task<bool> Update(RegionUpdate entity, int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Create(RegionCreate entity, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsActive(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsLock(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsWaiting(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> ChangeIsDisable(int id, int userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIsExistById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> CheckIsExistByParam(string param)
    {
        throw new NotImplementedException();
    }

    #endregion
    
}