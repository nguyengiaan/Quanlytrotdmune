using Quanlytrotdmune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Quanlytrotdmune.Dao
{
    public class CommentDao
    {
        QUANLYTROEntities1 ql = null;
        public CommentDao() 
        {
            ql = new QUANLYTROEntities1();
        }
        public bool Insert(COMMENT cm)
        { 
            ql.COMMENTs.Add(cm);
            ql.SaveChangesAsync();
            return true;
        }
        public List<COMMENT> ListComment(long userid,long roomid)
        {
            return ql.COMMENTs.Where(x=>x.user_id==userid && x.room_id==roomid).ToList();
        }
        public List<CommentViewModel> ListCommentViewModel(long userid,long roomid)
        {
            var model = (from a in ql.COMMENTs
                         join b in ql.USERTROes on a.user_id equals b.userid
                         where a.user_id == userid && a.room_id == roomid
                         select new CommentViewModel
                         {
                             fullname = b.full_name,
                             content = a.content
                         }) ; 
            return model.ToList();
        }

    }
}