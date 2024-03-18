using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Models
{
    public class Login
    {
        [Required(ErrorMessage = "nhập Gmail")]
        [RegularExpression(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", ErrorMessage = "Định dạng gmail sai")]
        public string email { get; set; }

        [Required(ErrorMessage = "nhập password đăng nhập")]
        public string password { get; set; }
    }
}