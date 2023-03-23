using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Employee
{
    public class LoaiDichVuEmployKhaiController : Controller
    {
        // GET: LoaiDichVuEmployKhai
        
            KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
            // GET: LoaiDichVuHuy
            public ActionResult ListLoaiDichVu()
            {
                var all_loaidichvu = context.LOAIDICHVUs.ToList();
                return View(all_loaidichvu);
            }

            public ActionResult CreateLoaiDichVu()
            {
                return View();
            }
            [HttpPost]
            public ActionResult CreateLoaiDichVu(FormCollection collection, LOAIDICHVU ldv)
            {
                var E_tenloaidichvu = collection["TENLOAIDICHVU"];
                if (string.IsNullOrEmpty(E_tenloaidichvu))
                {
                    ViewData["Error"] = "Don't empty!";
                }
                else
                {
                    ldv.TENLOAIDICHVU = E_tenloaidichvu;

                    context.LOAIDICHVUs.InsertOnSubmit(ldv);
                    context.SubmitChanges();
                    return RedirectToAction("ListLoaiDichVu");
                }
                return this.CreateLoaiDichVu();
            }

            public ActionResult EditLoaiDichVu(int maLoaiDichVu)
            {
                var db = context.LOAIDICHVUs.First(m => m.MALOAIDICHVU == maLoaiDichVu);
                return View(db);
            }
            [HttpPost]
            public ActionResult EditLoaiDichVu(int maLoaiDichVu, FormCollection collection)
            {
                LOAIDICHVU db = context.LOAIDICHVUs.FirstOrDefault(p => p.MALOAIDICHVU == maLoaiDichVu);
                var E_tenloaidichvu = collection["TENLOAIDICHVU"];
                if (string.IsNullOrEmpty(E_tenloaidichvu))
                {
                    ViewData["Error"] = "Don't empty!";
                }
                else
                {
                    db.TENLOAIDICHVU = E_tenloaidichvu;
                    context.SubmitChanges();
                    return RedirectToAction("ListLoaiDichVu");
                }
                return this.EditLoaiDichVu(maLoaiDichVu);
            }

            public ActionResult DeleteLoaiDichVu(int maLoaiDichVu)
            {
                var dbDelete = context.LOAIDICHVUs.First(m => m.MALOAIDICHVU == maLoaiDichVu);
                return View(dbDelete);
            }
            [HttpPost]
            public ActionResult DeleteLoaiDichVu(int maLoaiDichVu, FormCollection collection)
            {
                var dbDelete = context.LOAIDICHVUs.Where(m => m.MALOAIDICHVU == maLoaiDichVu).First();
                context.LOAIDICHVUs.DeleteOnSubmit(dbDelete);
                context.SubmitChanges();
                return RedirectToAction("ListLoaiDichVu");
            }
        }
}