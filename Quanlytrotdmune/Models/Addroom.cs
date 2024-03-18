using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Models
{
    public class Addroom
    {
        [Required(ErrorMessage ="Hãy nhập Tên trọ")]
        public string name { get; set; }
        public string description { get; set; }
        [Required(ErrorMessage = "Hãy nhập giá")]
        public Nullable<double> price { get; set; }

        [Required(ErrorMessage = "Hãy nhập đường")]
        public Nullable<int> location_id { get; set; }
        [Required(ErrorMessage = "Hãy nhập ngày tháng")]
        public Nullable<System.DateTime> date { get; set; }
        [Required(ErrorMessage = "Hãy nhập Avartar")]
        public string Avt { get; set; }
        [Required(ErrorMessage = "Hãy nhập địa chỉ")]
        public string Address { get; set; }
    }
}