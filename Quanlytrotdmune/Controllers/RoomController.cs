using PagedList;
using Quanlytrotdmune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quanlytrotdmune.Controllers
{
    public class RoomController : Controller
    {
     
       QUANLYTROEntities1 ql=new QUANLYTROEntities1();
        [HttpGet]
        public ActionResult Detail(int id)
        {
            ViewBag.id = Convert.ToInt32(Session["Iduser1"]);
            ViewBag.idroom = id;
            Session["idroom"] = id;
            var sql = from n in ql.USERTROes
                      join
                    a in ql.ROOMs on n.userid equals a.userid
                      join c in ql.LOCATIONs on a.location_id equals c.location_id
                      where a.room_id == id
                      select new Rooms { full_name = n.full_name, description = a.description, street = c.name, name = a.name, phone = n.phone, price = a.price, url = a.Avt, address = n.address,rate=n.rate,status=a.status };

            return View(sql);
        }
        public ActionResult Showcomment(int? page, int? pagesize)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 3;
            }
            ViewBag.idroom = Convert.ToInt32( Session["idroom"]);
            var idroom = Convert.ToInt32(Session["idroom"]);
            var sql = from n in ql.COMMENTs
                      join b in ql.USERTROes on n.user_id equals b.userid
                      where n.room_id == idroom 
                      select n;
           var comm=sql.ToList();
            return View  (comm.ToPagedList((int)page, (int)(pagesize)));
          
        }

        [HttpPost]
        public ActionResult Comment(FormCollection form)
        {
           var iduser=Convert.ToInt32(Session["Iduser1"]);
            var idroom = Convert.ToInt32(Session["idroom"]);
            var comment = form["comment"];
            var rate = form["rate"];
          
            COMMENT cm=new COMMENT();
            if(Session["Iduser1"]==null)
            {
                return RedirectToAction("Login", "User");
          
              }
            if (comment != null && rate !=null)
            {
                try
                {
                    var sql = from n in ql.ROOMs where n.room_id ==idroom select n;
                    var sql2 = sql.FirstOrDefault().userid;
                    var sql1 = from n in ql.COMMENTs where n.user_id_tro == sql2 select n;
                    cm.content = comment;
                    cm.room_id = idroom;
                    cm.user_id = iduser;
                    cm.rate = Convert.ToInt32(rate);
                    cm.user_id_tro = sql2;
                    ql.COMMENTs.Add(cm);
                    ql.SaveChanges();
                    var comm = sql1.Sum(x => x.rate);
                    var count = sql1.Count();
                    var rate1 = ql.USERTROes.Find(sql2);
                    rate1.rate = comm / count;
                    ql.SaveChanges();
                    return RedirectToAction("Detail", "Room", new { id = idroom });
                }
                catch(Exception e) 
                {
                    ViewBag.error = "Thêm bình luận thất bại";
                }
           
            }
            else
            {
                ViewBag.error = "hãy nhập bình luận và đánh giá  ";
                return RedirectToAction("Detail", "Room", new { id = idroom });
            }
            return RedirectToAction("Detail", "Room", new { id = idroom });

        }
        public ActionResult Dexuat()
        {
            var idroom = Convert.ToInt32(Session["idroom"]);
            var sql = from n in ql.ROOMs where n.room_id == idroom select n;
            var sql2 = sql.FirstOrDefault().userid;
            var sql3 = from n in ql.ROOMs where n.userid == sql2 select n;

            return View(sql3);
        }
    }

}