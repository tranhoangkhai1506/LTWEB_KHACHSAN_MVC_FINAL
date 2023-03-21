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
        public ActionResult ViewPhong()
        {
            var all_Phong = from ss in data.PHONGs select ss;
            return View(all_Phong);
        }
    }
}