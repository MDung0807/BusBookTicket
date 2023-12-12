using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CustomerManage.DTOs.Requests
{
    [ValidateNever]
    public class FormRegister
    {
        #region --Customer -- 
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public int WardId { get; set; }
        public IFormFile Avatar { get; set; }
        #endregion -- Customer --

        #region -- Account --
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        #endregion -- Account --
    }
}
