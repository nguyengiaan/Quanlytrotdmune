using Quanlytrotdmune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Quanlytrotdmune.Controllers
{
    public class HomeController : Controller
    {
        QUANLYTROEntities1 ql = new QUANLYTROEntities1();
       

        public ActionResult Index()
        {
            ViewBag.Title = Session["log"];
            ViewBag.id = Convert.ToInt64(Session["Iduser1"]);
            var sql = from n in ql.ROOMs
                      where n.price < 1000000
                      select n;
            return View(sql);
        }
        public ActionResult Room()
        {
            ViewBag.id = Convert.ToInt64(Session["Iduser1"]);
            var sql = from n in ql.ROOMs select n;
            return PartialView(sql);
        }
        [HttpGet]
      public ActionResult SumRoom(int ? page,int ? pagesize,FormCollection form,int ?price, int ?km)
        {
           //price =Convert.ToInt32(form["price"]);
            ViewBag.id = Convert.ToInt64(Session["Iduser1"]);
            var sql = from n in ql.ROOMs
                      join a in ql.LOCATIONs on n.location_id equals a.location_id
                      orderby n.date descending
                      select n;
            var room = sql.ToList();

            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 9;
            }
    
            if (price == 1 || km != null)
            {
                room = room.Where(x => x.price <= 700000 && x.price >= 500000 || x.LOCATION.km==km).ToList(); 
                return View(room.ToPagedList((int)page, (int)pagesize));
            }
            else if (price == 2 || km != null)
            {
                room = room.Where(x => x.price <= 1000000 && x.price >= 700000 || x.LOCATION.km == km).ToList();
                return View(room.ToPagedList((int)page, (int)pagesize));
            }
            else if (price == 3 || km != null)
            {
                room = room.Where(x => x.price <= 2000000 && x.price >= 1000000 || x.LOCATION.km == km).ToList();
                return View(room.ToPagedList((int)page, (int)pagesize));
            }

            return View(room.ToPagedList((int)page, (int)pagesize));
        }
      
    }
}
