using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Migrations;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;
using Quanlytrotdmune.Models;
namespace Quanlytrotdmune.Controllers
{
    public class UserController : Controller
    {
        private const string cartsession = "Favorite";
        private const string TK = "Iduser";
      
        QUANLYTROEntities1 ql=new QUANLYTROEntities1();
        // GET: User
    
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login user)
        {
            //var sql = from n in ql.USERTROes
            //          where n.email == user.email /*&& n.password == user.password*/
            //          select n;
            string sqlQuery = "SELECT * FROM USERTRO WHERE email = '" + user.email + "' AND password ='"+user.password+"'";
            var sql=ql.USERTROes.SqlQuery(sqlQuery).ToList();
            if (sql.Count() > 0)
            {
                Session["Iduser"] = sql.FirstOrDefault();
                Session["Iduser1"] = sql.FirstOrDefault().userid;
                Session["log"] = sql.ToList();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.log = sql.ToList();
                ViewBag.error = "Sai tài khoản mật khẩu";
            }

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            try {
                
                if(!ModelState.IsValid)
                {
                    USERTRO usertro=new USERTRO();
                    usertro.full_name= user.full_name;
                    usertro.phone= user.phone;
                    usertro.address= user.address;
                    usertro.email = user.email;
                    usertro.password =GetMD5(user.password);
                    ql.USERTROes.Add(usertro);   
                    ql.SaveChanges();
                    return RedirectToAction("Login","User");
                }            
            }
            catch(Exception)
            {
                ModelState.AddModelError("fullname", "Đăng ký không thành công");
            }
            return View();
        }
        public static string GetMD5(string str)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] fromData = Encoding.UTF8.GetBytes(str);
            byte[] targetData = md5.ComputeHash(fromData);
            string byte2String = null;

            for (int i = 0; i < targetData.Length; i++)
            {
                byte2String += targetData[i].ToString("x2");

            }
            return byte2String;
        }
        public ActionResult Loginlogout()
        {
            ViewBag.id = Convert.ToInt64(Session["Iduser1"]);
            return PartialView();
       }
        public ActionResult Additem(int idroom,int iduser)
        {
     
            if (Session["Iduser"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var favorite = ql.FAVORITEs.Select(x => x.room_id == idroom && x.user_id == iduser);

                if (favorite != null)
                {
                    var favorite2 = ql.FAVORITEs.Count(x => x.room_id == idroom);
                    if (favorite2 > 0)
                    {
                        TempData["Thongbao"] = "Đã tồn tại";
                    }
                    else
                    {
                        FAVORITE item = new FAVORITE();
                        item.room_id = idroom;
                        item.user_id = iduser;
                        ql.FAVORITEs.Add(item);
                        ql.SaveChanges();
                    }
                }
                else
                {
                    FAVORITE item = new FAVORITE();
                    item.room_id = idroom;
                    item.user_id = iduser;
                    ql.FAVORITEs.Add(item);
                    ql.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Favorite(int iduser)
        {
            if (Session["Iduser"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var sql = from n in ql.USERTROes
                          join a in ql.ROOMs on n.userid equals a.userid
                          join b in ql.FAVORITEs on a.room_id equals b.room_id
                          join c in ql.LOCATIONs on a.location_id equals c.location_id
                          where b.user_id == iduser
                          select new Rooms
                          {
                              full_name = n.full_name,
                              description = a.description,
                              street = c.name,
                              name = a.name,
                              phone = n.phone,
                              price = a.price,
                              address = n.address,
                              id = (int)b.favorite_id,
                          };
                return View(sql);
            }
        }
        public ActionResult profile()
        {
            if (Session["Iduser"] == null)
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                var id = Convert.ToInt32(Session["Iduser1"]);
                var sqlQuery = "SELECT * FROM USERTRO WHERE userid = '" + id + "'";
                var sql = ql.USERTROes.SqlQuery(sqlQuery).ToList();
                return View(sql);
            }
        }
        public ActionResult show()
        {
            var id = Convert.ToInt32(Session["Iduser1"]);
            var sql=from n in ql.ROOMs where n.userid == id select n;
            return PartialView(sql);
        }
        [HttpGet]
        public ActionResult CreateRoom()
        {
            var sql = ql.LOCATIONs.ToList();
            ViewBag.Location = new SelectList(sql, "location_id", "name");
            return View();
        }
        [HttpPost] 
        public ActionResult CreateRoom(FormCollection form,HttpPostedFileBase uploadhinh)
        {
            var sql = ql.LOCATIONs.ToList();
            ViewBag.Location = new SelectList(sql, "location_id", "name");
            var id1 = Convert.ToInt32(Session["Iduser1"]);
            var tentro = form["tentro"];
            var date = form["date"];
            var phone = form["phone"];
            var price = form["price"];
            var location = form["location"];
            var diachi = form["diachi"];
            var chitiet = form["desprcription"];
            if(tentro== "")
            {
                ViewBag.erro1 = "hãy nhập tên trọ";
                return View();

            }
            else if (date == "")
            {
                ViewBag.erro2 = "hãy nhập ngày";
                return View();

            }
            else if (phone == "" && phone.Length<0)
            {
                ViewBag.erro3 = "số điện thoại không đúng định dạng";
                return View();

            }
            else if(price == "")
            {
                ViewBag.erro4 = "hãy nhập tiền ";
                return View();
            }    
            else if (diachi == "")
            {
                ViewBag.error5 = "hãy nhập địa chỉ";
                return View();

            }

           else if (uploadhinh != null && uploadhinh.ContentLength>0)
            {
                try
                {
                    int id = int.Parse(ql.ROOMs.ToList().Last().room_id.ToString());

                    string _Filename = "";
                    int index = uploadhinh.FileName.IndexOf(".");
                    _Filename = "room" + id.ToString() + "." + uploadhinh.FileName.Substring(index + 1);
                    string _path = Path.Combine(Server.MapPath("~/Content/Anhtro"), _Filename);

                    // Kiểm tra dung lượng tệp tải lên, nếu quá lớn thì hiển thị thông báo lỗi.
                    if (uploadhinh.ContentLength > 10485760) // 10MB
                    {
                        ViewBag.error = "Dung lượng tệp quá lớn. Vui lòng chọn tệp có dung lượng nhỏ hơn 10MB.";
                        return View();
                    }

                    // Kiểm tra định dạng tệp hình ảnh, nếu không đúng định dạng thì hiển thị thông báo lỗi.
                    if (!Path.GetExtension(uploadhinh.FileName).Equals(".jpg", StringComparison.OrdinalIgnoreCase)
                        && !Path.GetExtension(uploadhinh.FileName).Equals(".jpeg", StringComparison.OrdinalIgnoreCase)
                        && !Path.GetExtension(uploadhinh.FileName).Equals(".png", StringComparison.OrdinalIgnoreCase))
                    {
                        ViewBag.error = "Định dạng tệp không hợp lệ. Vui lòng chọn tệp hình ảnh có định dạng .jpg, .jpeg, hoặc .png.";
                        return View();
                    }

                    // Tạo mới đường dẫn lưu trữ tệp hình ảnh nếu chưa tồn tại.
                    if (!Directory.Exists(Server.MapPath("~/Content/Anhtro")))
                    {
                        Directory.CreateDirectory(Server.MapPath("~/Content/Anhtro"));
                    }

                    // Lưu trữ tệp hình ảnh vào đường dẫn vừa tạo.
                    uploadhinh.SaveAs(_path);

                    // Thêm thông tin phòng vào cơ sở dữ liệu.
                    ROOM room = new ROOM();
                    room.Avt = _Filename;
                    room.userid = id1;
                    room.Address = diachi;
                    room.price = double.Parse(price);
                    room.name = tentro;
                    room.date = DateTime.Parse(date);
                    room.location_id = int.Parse(location);
                    room.description = diachi;
                    ql.ROOMs.Add(room);
                    ql.SaveChanges();

                    return RedirectToAction("profile", "user");
                }
                catch (Exception e)
                {
                    // Ghi lại thông tin lỗi vào file log hoặc trình quản lý lỗi.
                    // Hiển thị thông báo lỗi cho người dùng.
                  
                    ViewBag.error = "vui lòng thêm hình ảnh .";
                    return View();
                }
              
            }
            else
            {
                // Hiển thị thông báo lỗi cho người dùng.
                ViewBag.error = "Vui lòng chọn một tệp hình ảnh.";
                return View();
            }

        }
        public ActionResult Deleteroom(int id)
        {
            var a = Convert.ToInt32(Session["Iduser1"]);
            ROOM f = ql.ROOMs.Find(id);
            ql.ROOMs.Remove(f);
            ql.SaveChanges();
            return RedirectToAction("profile", "User", new { iduser = a });
        }
        public ActionResult Updateroom(int id)
        {
            var a = Convert.ToInt32(Session["Iduser1"]);
            var sql=ql.ROOMs.Find(id);
            if(sql.status=="Còn Trọ")
            {
                sql.status = "Hết Trọ";
                ql.SaveChanges();
                return RedirectToAction("profile", "User", new { iduser = a });
            }
            else
            {
                sql.status = "Còn Trọ";
                ql.SaveChanges();
                return RedirectToAction("profile", "User", new { iduser = a });
            }
           
        }
        public ActionResult Delete(int id)
        {
            var a = Convert.ToInt32(Session["Iduser1"]);
            FAVORITE f = ql.FAVORITEs.Find(id);
            ql.FAVORITEs.Remove(f);
            ql.SaveChanges();
            return RedirectToAction("Favorite", "User", new { iduser=a});
        }


    }
    
}