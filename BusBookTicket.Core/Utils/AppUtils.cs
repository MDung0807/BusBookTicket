using AutoMapper;

namespace BusBookTicket.Core.Utils;

public class AppUtils
{
    /// <summary>
    /// Mapping from T1 to T2
    /// </summary>
    /// <param name="source">List T1</param>
    /// <param name="_mapper">Mapper object</param>
    /// <typeparam name="T1">Object Source</typeparam>
    /// <typeparam name="T2">Object Dest</typeparam>
    /// <returns></returns>
    public static Task<List<T2>> MapObject<T1, T2>(List<T1> source, IMapper _mapper)
    {
        List<T2> listDest = new List<T2>();
        try
        {
            foreach (var item in source)
            {
                listDest.Add(_mapper.Map<T2>(item));
            }

            return Task.FromResult(listDest ?? null);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }
    
    /// <summary>
    /// Response Page Object
    /// </summary>
    /// <param name="index">page index</param>
    /// <param name="size">page size</param>
    /// <param name="count">all item in db</param>
    /// <param name="items">tem in page</param>
    /// <typeparam name="T1">Page Result</typeparam>
    /// <typeparam name="T2">Item object</typeparam>
    /// <returns></returns>
    public static T1 ResultPaging<T1,T2>(int index, int size, int count, List<T2> items) where T1 : new()
    {
        T1 result = new T1();

        typeof(T1).GetProperty("PageIndex")?.SetValue(result, index);
        typeof(T1).GetProperty("PageSize")?.SetValue(result, size);
        typeof(T1).GetProperty("PageTotal")?.SetValue(result, (int)Math.Ceiling((decimal)count / size));
        typeof(T1).GetProperty("Items")?.SetValue(result, items);

        return result;
    }
}