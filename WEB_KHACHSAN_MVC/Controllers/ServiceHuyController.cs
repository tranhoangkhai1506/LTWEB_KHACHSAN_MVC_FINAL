using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class ServiceHuyController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // GET: ServiceHuy
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ViewService()
        {
            var all_service = context.DICHVUs.ToList();
            return View(all_service);
        }
    }
}