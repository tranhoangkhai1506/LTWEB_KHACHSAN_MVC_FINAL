using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class LoginController : Controller
    {
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        // GET: Login
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangNhap(FormCollection collection)
        {
            var tendangnhap = collection["tendangnhap"];
            var matkhau = collection["matkhau"];
            TAIKHOAN taikhoandangnhap = data.TAIKHOANs.Where(n => n.TENDANGNHAP == tendangnhap && n.MATKHAU == matkhau).FirstOrDefault();
            if (taikhoandangnhap != null)
            {
                ViewBag.ThongBao = "Chúc mừng đăng nhập thành công";
                Session["TENDANGNHAP"] = taikhoandangnhap.TENDANGNHAP;
                var valueID = Session["TENDANGNHAP"];
                if (valueID != null)
                {
                    if (valueID.ToString() == "admin")
                    {

                    }
                }
            }
            else
            {
                ViewBag.ThongBao = "Tên đăng nhập hoặc mật khẩu không đúng";
                return this.DangNhap();
            }
            return RedirectToAction("Index", "HomeKhai");
        }
    }
}