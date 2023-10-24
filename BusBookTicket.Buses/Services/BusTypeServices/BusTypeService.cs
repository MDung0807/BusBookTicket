using AutoMapper;
using BusBookTicket.Buses.DTOs.Requests;
using BusBookTicket.Buses.DTOs.Responses;
using BusBookTicket.Buses.Repositories.BusTypeRepositories;
using BusBookTicket.Core.Models.Entity;
using BusBookTicket.Core.Utils;

namespace BusBookTicket.Buses.Services.BusTypeServices;

public class BusTypeService : IBusTypeService
{
    #region -- Properties --

    private readonly IBusTypeRepos _busTypeRepos;
    private readonly IMapper _mapper;
    #endregion -- Properties --

    public BusTypeService(IBusTypeRepos busTypeRepos, IMapper mapper)
    {
        this._busTypeRepos = busTypeRepos;
        this._mapper = mapper;
    }
    public async Task<BusTypeResponse> getByID(int id)
    {
        BusType busType = await _busTypeRepos.getByID(id);
        return _mapper.Map<BusTypeResponse>(busType);
    }

    public async Task<List<BusTypeResponse>> getAll()
    {
        List<BusType> busTypes = await _busTypeRepos.getAll();
        List<BusTypeResponse> responses = await AppUtils.MappObject<BusType, BusTypeResponse>(busTypes, _mapper);
        return responses;
    }

    public async Task<bool> update(BusTypeFormUpdate entity, int id)
    {
        BusType busType = _mapper.Map<BusType>(entity);
        busType.busTypeID = id;
        await _busTypeRepos.update(busType);
        return true;
    }

    public async Task<bool> delete(int id)
    {
        BusType busType = await _busTypeRepos.getByID(id);
        busType.status = (int)EnumsApp.Delete;
        await _busTypeRepos.delete(busType);
        return true;
    }

    public async Task<bool> create(BusTypeForm entity)
    {
        BusType busType = _mapper.Map<BusType>(entity);
        await _busTypeRepos.create(busType);
        return true;
    }
}