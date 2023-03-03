using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;
namespace Model.Dao
{
    public class UserAdmin
    {
        QUANLYTROEntities ql = null;
        public UserAdmin() { ql = new QUANLYTROEntities(); }

    }
}
