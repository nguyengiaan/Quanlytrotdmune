using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Models
{
    [Serializable]
    public class GioHang
    {
        public int favorite_id { get; set; }
        public Nullable<int> user_id { get; set; }
        public Nullable<int> room_id { get; set; }

        public virtual ROOM ROOM { get; set; }
        public virtual USERTRO USERTRO { get; set; }
    }
}