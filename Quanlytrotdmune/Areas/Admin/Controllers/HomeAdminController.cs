using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlytrotdmune.Areas.Admin.Model;
using Quanlytrotdmune.Models;
using PagedList;
using System.Data.SqlTypes;

namespace Quanlytrotdmune.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        QUANLYTROEntities1 ql=new QUANLYTROEntities1();
        public ActionResult Index(int ?page,int ?pagesize,string timkiem)
        {
            if (page == null)
            {
                page = 1;
            } 
            if(pagesize == null)
            {
                pagesize = 6;
            }   
            if (Session["idAdmin"]==null)
            {
                return RedirectToAction("Login", "HomeAdmin");
            }
            else
            {
                if(!String.IsNullOrEmpty(timkiem))
                {

                    var sql=from n in ql.ROOMs join a in ql.USERTROes on n.userid equals a.userid where a.full_name.Contains(timkiem)   select n;
                    var sql1 = sql.ToList().OrderByDescending(x=>x.date);
                    return View(sql1.ToPagedList((int)page, (int)pagesize));
                }else
                {
                    var sql = ql.ROOMs.ToList().OrderByDescending((x=>x.date));
                    return View(sql.ToPagedList((int)page, (int)pagesize));
                }    
            }
        }
        public ActionResult Login()
        {
            return View();
        }
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Login(LoginAdmin lg)
        {
           if(ModelState.IsValid) 
            {
       
                var login = ql.ADMINs.Where(s=>s.username.Equals(lg.username)&& s.password.Equals(lg.password));
                if(login.Count()==0)
                {
                    
                  ModelState.AddModelError("username", "Sai tài khoản hoặc mật khẩu");

                }
                else
                {
                    Session["idAdmin"] = login.FirstOrDefault().id;
                    return RedirectToAction("Index", "HomeAdmin");
                } 
                    
            }
          return View();
        }
        public ActionResult Logout()
        {
            Session.Clear();//remove session
            return RedirectToAction("Login","HomeAdmin");
        }
        public ActionResult DeleteRA(int id)
        {

            var sql = ql.ROOMs.Find(id);
            var sql1 = from n in ql.COMMENTs where n.room_id == id select n;
            foreach (var cmt in sql1)
            {
                var sql2 = ql.COMMENTs.Find(cmt.comment_id);

                ql.COMMENTs.Remove(sql2);
            }
            ql.ROOMs.Remove(sql);
            ql.SaveChanges();
            return RedirectToAction("Index", "HomeAdmin");

        }
        public ActionResult information()
        {
            var a = Convert.ToInt32(Session["idAdmin"]);
            var tk = ql.ADMINs.Where(x=>x.id==a).ToList();
            return PartialView(tk);
        }
     }

}