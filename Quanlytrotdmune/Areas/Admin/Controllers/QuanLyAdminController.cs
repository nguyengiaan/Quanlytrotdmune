using PagedList;
using Quanlytrotdmune.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.Mvc;

namespace Quanlytrotdmune.Areas.Admin.Controllers
{
    public class QuanLyAdminController : Controller
    {
        QUANLYTROEntities1 ql = new QUANLYTROEntities1();
        // GET: Admin/QuanLyAdmin
        public ActionResult qluser(int? page, int? pagesize, string timkiem)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 6;
            }

            if (!String.IsNullOrEmpty(timkiem))
            {

                var sql = from n in ql.USERTROes where n.full_name.Contains(timkiem) select n;
                var sql1 = sql.ToList();
                return View(sql1.ToPagedList((int)page, (int)pagesize));
            }
            else
            {
                var sql = ql.USERTROes.ToList();
                return View(sql.ToPagedList((int)page, (int)pagesize));
            }

        }
        public ActionResult Removeuser(int id)
        {
            var sql0 = from n in ql.COMMENTs where n.user_id == id select n;
            foreach (var a in sql0)
            {
                var sql3 = ql.COMMENTs.Find(a.comment_id);
                ql.COMMENTs.Remove(sql3);
            }
            var sql1 = from n in ql.ROOMs where n.userid == id select n;
            foreach (var a in sql1)
            {
                var sql4 = ql.ROOMs.Find(a.room_id);
                ql.ROOMs.Remove(sql4);
            }
            var sql2 = from n in ql.FAVORITEs where n.user_id == id select n;
            foreach (var a in sql2)
            {
                var sql4 = ql.FAVORITEs.Find(a.favorite_id);
                if (sql4 != null)
                {
                    ql.FAVORITEs.Remove(sql4);
                }
            }
            var sql = ql.USERTROes.Find(id);
            ql.USERTROes.Remove(sql);
            ql.SaveChanges();
            return RedirectToAction("qluser", "QuanLyAdmin");
        }
        public ActionResult Qladmin(int? page, int? pagesize, string timkiem)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 6;
            }

            if (!String.IsNullOrEmpty(timkiem))
            {

                var sql = from n in ql.ADMINs where n.fullname.Contains(timkiem) select n;
                var sql1 = sql.ToList();
                return View(sql1.ToPagedList((int)page, (int)pagesize));
            }
            else
            {
                var sql = ql.ADMINs.ToList();
                return View(sql.ToPagedList((int)page, (int)pagesize));
            }
        }
        [HttpGet]
        public ActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAdmin(string fullname, string taikhoan, string password)
        {
            try
            {
                var sql = from n in ql.ADMINs where n.username == taikhoan select n;
                if (sql.Count() == 0)
                {

                    if (!String.IsNullOrEmpty(fullname) && !String.IsNullOrEmpty(taikhoan) && !String.IsNullOrEmpty(password))
                    {
                        ADMIN AD = new ADMIN();
                        AD.fullname = fullname;
                        AD.password = password;
                        AD.username = taikhoan;
                        ql.ADMINs.Add(AD);
                        ql.SaveChanges();
                        return RedirectToAction("Qladmin", "QuanLyAdmin");
                    }
                    else
                    {
                        ViewBag.error = "Vui lòng nhập đầy đủ thông tin";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "Tài khoản đã tồn tại";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = "Tạo tài khoản thất bại";
                return View();
            }
        }
        public ActionResult RemoveAD(int id)
        {
            var sql = ql.ADMINs.Find(id);
            ql.ADMINs.Remove(sql);
            ql.SaveChanges();
            return RedirectToAction("Qladmin", "QuanLyAdmin");
        }
        public ActionResult Location(int? page, int? pagesize, string timkiem)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 6;
            }

            if (!String.IsNullOrEmpty(timkiem))
            {

                var sql = from n in ql.LOCATIONs where n.name.Contains(timkiem) select n;
                var sql1 = sql.ToList();
                return View(sql1.ToPagedList((int)page, (int)pagesize));
            }
            else
            {
                var sql = ql.LOCATIONs.ToList();
                return View(sql.ToPagedList((int)page, (int)pagesize));
            }


        }
        
        [HttpGet]
        public ActionResult AddLocation()
        {
            return View();  
        }
        [HttpPost]
        public ActionResult AddLocation(string sokm, string phuong)
        {
            try
            {
                var sql = from n in ql.LOCATIONs where n.name == phuong select n;
                if (sql.Count() == 0)
                {

                    if (!String.IsNullOrEmpty(sokm) && !String.IsNullOrEmpty(phuong))
                    {
                        LOCATION LC = new LOCATION();
                        LC.name = phuong;
                        LC.km = Convert.ToInt32(sokm);
                        ql.LOCATIONs.Add(LC);
                        ql.SaveChanges();
                        return RedirectToAction("Location", "QuanLyAdmin");
                    }
                    else
                    {
                        ViewBag.error = "Vui lòng nhập đầy đủ thông tin";
                        return View();
                    }
                }
                else
                {
                    ViewBag.error = "địa chỉ đã tồn tại";
                    return View();
                }

            }
            catch (Exception ex)
            {
                ViewBag.error = "Tạo tài khoản thất bại";
                return View();
            }
        }
        public ActionResult RemoveLC(int id)
        {
            var sql = ql.LOCATIONs.Find(id);
            ql.LOCATIONs.Remove(sql);
            ql.SaveChanges();
            return RedirectToAction("Location","QuanLyAdmin");
        }
        public ActionResult CommentAD(int? page, int? pagesize, string timkiem)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pagesize == null)
            {
                pagesize = 6;
            }

            if (!String.IsNullOrEmpty(timkiem))
            {
                var sql = from n in ql.COMMENTs
                          join b in ql.USERTROes on n.user_id equals b.userid
                          where b.full_name.Contains(timkiem)
                          select n;
                var comm = sql.ToList();
                return View(comm.ToPagedList((int)page, (int)(pagesize)));
       
            }
            else
            {
                var sql = ql.COMMENTs.ToList();
                return View(sql.ToPagedList((int)page, (int)pagesize));
            }
  
        }
        public ActionResult RemoveCM(int id) 
        {
            var sql=ql.COMMENTs.Find(id);
            ql.COMMENTs.Remove(sql);
            ql.SaveChanges();
            return RedirectToAction("CommentAD", "QuanLyAdmin");
        }

    }
}