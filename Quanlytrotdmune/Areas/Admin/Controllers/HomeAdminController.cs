using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quanlytrotdmune.Areas.Admin.Model;
using Quanlytrotdmune.Models;

namespace Quanlytrotdmune.Areas.Admin.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: Admin/HomeAdmin
        QUANLYTROEntities ql=new QUANLYTROEntities();
        public ActionResult Index()
        {
            if (Session["idAdmin"]==null)
            {
                return RedirectToAction("Login", "HomeAdmin");
            }    
            return View();
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
        public ActionResult information()
        {
            var a = Convert.ToInt32(Session["idAdmin"]);
            var tk = ql.ADMINs.Where(x=>x.id==a).ToList();
            return PartialView(tk);
        }
    }
}