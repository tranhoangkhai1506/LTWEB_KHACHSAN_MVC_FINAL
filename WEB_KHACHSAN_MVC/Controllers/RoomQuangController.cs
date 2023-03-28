using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class RoomQuangController : Controller
    {
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        // GET: RoomQuang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewPhongLoai1()
        {
            var all_Phong = data.PHONGs.Where(p => p.MALOAIPHONG == 1);
            return View(all_Phong);
        }
        public ActionResult ViewPhongLoai2()
        {
            var all_Phong = data.PHONGs.Where(p => p.MALOAIPHONG == 2);
            return View(all_Phong);
        }
        public ActionResult ViewPhongLoai3()
        {
            var all_Phong = data.PHONGs.Where(p => p.MALOAIPHONG == 3);
            return View(all_Phong);
        }
    }
}