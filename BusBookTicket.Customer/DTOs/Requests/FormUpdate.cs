using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CustomerManage.DTOs.Requests
{
    [ValidateNever]
    public class FormUpdate
    {
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
    }
}
