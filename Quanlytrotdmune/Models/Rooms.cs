using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Models
{
    [Serializable]
    public class Rooms
    {
        public string name { get; set; }
        public int id{ get; set; }
        public string description { get; set; }
        public Nullable<double> price { get; set; }
        public string url { get; set; }
        public string full_name { get; set; }
        public string phone { get; set; }
        public string street { get; set; }
        public string address { get; set; }
        public Nullable<int> rate { get; set; }

        public string status { get; set; }

    }
}