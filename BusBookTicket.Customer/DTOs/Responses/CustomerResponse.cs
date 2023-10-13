namespace BusBookTicket.CustomerManage.DTOs.Responses
{
    public class CustomerResponse
    {
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public string username { get; set; }
        public string rank { get; set; }
    }
}
