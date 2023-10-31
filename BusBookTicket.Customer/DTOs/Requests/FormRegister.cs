using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace BusBookTicket.CustomerManage.DTOs.Requests
{
    [ValidateNever]
    public class FormRegister
    {
        #region --Customer -- 
        [Required(ErrorMessage = "Requiry fullname")]
        [MinLength(length: 10, ErrorMessage = "Min lenght is 10"), MaxLength(50, ErrorMessage = "Max lenght is 50")]
        public string? fullName { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string? address { get; set; }
        public string? email { get; set; }
        public string? phoneNumber { get; set; }
        public string? gender { get; set; }
        public IFormFile avatar { get; set; }
        #endregion -- Customer --

        #region -- Account --
        public string username { get; set; }
        public string password { get; set; }
        public string roleName { get; set; }
        #endregion -- Account --
    }
}
