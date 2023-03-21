using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        KhachSanDBContextDataContext kh = new KhachSanDBContextDataContext();

        public ActionResult Index()
        {
            var list_KhachHang = from all in kh.KHACHHANGs select all;
            return View(list_KhachHang);
        }
    }
}