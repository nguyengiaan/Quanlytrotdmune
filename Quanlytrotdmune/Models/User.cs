using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Quanlytrotdmune.Models
{
    public class User
    {
    
            [Required(ErrorMessage = "Nhập Họ và tên")]
            [StringLength(20, MinimumLength = 6, ErrorMessage = "Tối thiểu là 6 ký tự và tối đa là 20 ký tự")]
            [Display(Name = "Họ và tên")]
            public string full_name { get; set; }
        [Required(ErrorMessage = "nhập Gmail")]
        [RegularExpression(@"([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)", ErrorMessage = "Định dạng gmail sai")]
            public string email { get; set; }

            [Required(ErrorMessage = "nhập password đăng nhập")]
            [StringLength(30, MinimumLength = 6, ErrorMessage = "Tối thiểu là 6 ký tự và tối đa là 20 ký tự")]
            [RegularExpression(@"^(?=.*[a-z]).{8,15}$", ErrorMessage = "phải có ký tự thường và chữ số")]
          
            public string password { get; set; }
            [Display(Name = "Hãy nhập lại mật khẩu")]
            [Required(ErrorMessage = "Hãy xác nhận password đăng nhập")]
            [System.ComponentModel.DataAnnotations.Compare("password", ErrorMessage = "Xác nhận mật khẩu không đúng")]
            [StringLength(30, MinimumLength = 6, ErrorMessage = "Tối thiểu là 6 ký tự và tối đa 20 ký tự")]
            public string confimpass { get; set; }
            [Phone]
            [MaxLength(10, ErrorMessage = "số điện thoại vượt quá 10 số")]
            [Required(ErrorMessage = "Hãy nhập số điện thoại")]
            public string phone { get; set; }
            [Required(ErrorMessage = "Hãy nhập địa chỉ")]
            [StringLength(50, MinimumLength = 6, ErrorMessage = "tối đa là 50 ký tự")]
            public string address { get; set; }
    }
}