using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class AboutKhaiController : Controller
    {
        // GET: AboutKhai
        KhachSanDBContextDataContext kh = new KhachSanDBContextDataContext();
        public ActionResult Index()
        {
            return View();
        }
    }
}