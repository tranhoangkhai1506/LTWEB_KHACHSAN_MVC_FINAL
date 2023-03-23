using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class HomeAdminQuangController : Controller
    {
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        // GET: HomeAdminQuang
        public ActionResult Index()
        {
            return View();
        }
    }
}