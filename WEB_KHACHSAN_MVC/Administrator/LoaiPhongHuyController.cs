using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class LoaiPhongHuyController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();  
        // GET: LoaiPhongHuy
        public ActionResult ListLoaiPhong()
        {
            var all_loaiPhong = context.LOAIPHONGs.ToList();
            return View(all_loaiPhong);
        }

        public ActionResult CreateLoaiPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateLoaiPhong(FormCollection collection, LOAIPHONG lp)
        {
            var E_tenloaiphong = collection["TENLOAIPHONG"];
            var E_giathue = collection["GIATHUE"];
            if (string.IsNullOrEmpty(E_tenloaiphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                lp.TENLOAIPHONG = E_tenloaiphong;
                lp.GIATHUE = decimal.Parse(E_giathue);

                context.LOAIPHONGs.InsertOnSubmit(lp); 
                context.SubmitChanges();
                return RedirectToAction("ListLoaiPhong");
            }
            return this.CreateLoaiPhong();
        }

        public ActionResult EditLoaiPhong(int maLoaiPhong)
        {
            var db = context.LOAIPHONGs.First(m => m.MALOAIPHONG == maLoaiPhong);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditLoaiPhong(int maLoaiPhong, FormCollection collection)
        {
            LOAIPHONG db = context.LOAIPHONGs.FirstOrDefault(p => p.MALOAIPHONG == maLoaiPhong);
            var E_tenloaiphong = collection["TENLOAIPHONG"];
            var E_giathue = collection["GIATHUE"];
            if (string.IsNullOrEmpty(E_tenloaiphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENLOAIPHONG = E_tenloaiphong;
                db.GIATHUE = decimal.Parse(E_giathue);
                context.SubmitChanges();
                return RedirectToAction("ListLoaiPhong");
            }
            return this.EditLoaiPhong(maLoaiPhong);
        }

        public ActionResult DeleteLoaiPhong(int maLoaiPhong)
        {
            var dbDelete = context.LOAIPHONGs.First(m => m.MALOAIPHONG == maLoaiPhong);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeleteLoaiPhong(int maLoaiPhong, FormCollection collection)
        {
            var dbDelete = context.LOAIPHONGs.Where(m => m.MALOAIPHONG == maLoaiPhong).First();
            context.LOAIPHONGs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListLoaiPhong");
        }
    }
}