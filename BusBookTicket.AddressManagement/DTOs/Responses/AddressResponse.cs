namespace BusBookTicket.AddressManagement.DTOs.Responses;

public class AddressResponse
{
    public int WardId { get; set; }
    public string FullNameWard { get; set; }
    public int DistrictId { get; set; }
    public string FullNameDistrict { get; set; }
    public int ProvinceId { get; set; }
    public string FullNameProvince { get; set; }
}