using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CustomerManage.DTOs.Requests
{
    [ValidateNever]
    public class FormRegister : IActionFilter
    {
        #region --Customer -- 
        [Required(ErrorMessage = "Require fullname")]
        [Range(10, 100, ErrorMessage = "Full name is from 10 to 100 character")]
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Gender { get; set; }
        public int WardId { get; set; }
        public IFormFile Avatar { get; set; }
        #endregion -- Customer --

        #region -- Account --
        public string Username { get; set; }
        public string Password { get; set; }
        public string RoleName { get; set; }
        #endregion -- Account --

        public void OnActionExecuting(ActionExecutingContext context)
        {
            new Exception("sfgfgg");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            new Exception("sfgfgg");

        }
    }
}
