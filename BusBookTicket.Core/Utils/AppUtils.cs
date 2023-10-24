using AutoMapper;
using Mapper = AutoMapper.Mapper;

namespace BusBookTicket.Core.Utils;

public class AppUtils
{
    /// <summary>
    /// Mapping from T1 to T2
    /// </summary>
    /// <param name="source">List T1</param>
    /// <typeparam name="T1">Object Source</typeparam>
    /// <typeparam name="T2">Object Dest</typeparam>
    /// <returns></returns>
    public static Task<List<T2>> MappObject<T1, T2>(List<T1> source, IMapper _mapper)
    {
        List<T2> listDest = new List<T2>();
        foreach (var item in source)
        {
            listDest.Add(_mapper.Map<T2>(item));
        }

        return Task.FromResult(listDest);
    }
}