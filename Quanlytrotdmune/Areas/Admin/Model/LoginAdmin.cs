using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Areas.Admin.Model
{
    public class LoginAdmin
    {
        public int id { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập tài khoản")]
        public string username { get; set; }
        public string fullname { get; set; }
        [Required(ErrorMessage ="Vui lòng nhập mật khẩu")]
        public string password { get; set; }
    }
}