using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Employee
{
    public class QuanLyDichVuEmployeeKhaiController : Controller
    {
        // GET: QuanLyDichVuEmployeeKhai
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // GET: DichVuHuy
        public ActionResult ListDichVu()
        {
            var all_listdichvu = context.DICHVUs.ToList();
            return View(all_listdichvu);
        }

        public ActionResult CreateDichVu()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateDichVu(FormCollection collection, DICHVU dv)
        {
            var E_tendichvu = collection["TENDICHVU"];
            var E_donvitinh = collection["DONVITINH"];
            var E_giathue = collection["GIATHUEDICHVU"];
            var E_maloaidichvu = collection["MALOAIDICHVU"];
            if (string.IsNullOrEmpty(E_tendichvu))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                dv.TENDICHVU = E_tendichvu;
                dv.DONVITINH = E_donvitinh;
                dv.GIATHUEDICHVU = decimal.Parse(E_giathue);
                dv.MALOAIDICHVU = int.Parse(E_maloaidichvu);
                context.DICHVUs.InsertOnSubmit(dv);
                context.SubmitChanges();
                return RedirectToAction("ListDichVu");
            }
            return this.CreateDichVu();
        }

        public ActionResult EditDichVu(int maDichVu)
        {
            var db = context.DICHVUs.First(m => m.MADICHVU == maDichVu);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditDichVu(int maDichVu, FormCollection collection)
        {
            DICHVU db = context.DICHVUs.FirstOrDefault(p => p.MADICHVU == maDichVu);
            var E_tendichvu = collection["TENDICHVU"];
            var E_donvitinh = collection["DONVITINH"];
            var E_giathue = collection["GIATHUEDICHVU"];
            var E_maloaidichvu = collection["MALOAIDICHVU"];
            if (string.IsNullOrEmpty(E_tendichvu))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENDICHVU = E_tendichvu;
                db.DONVITINH = E_donvitinh;
                db.GIATHUEDICHVU = decimal.Parse(E_giathue);
                db.MALOAIDICHVU = int.Parse(E_maloaidichvu);
                context.SubmitChanges();
                return RedirectToAction("ListDichVu");
            }
            return this.EditDichVu(maDichVu);
        }

        public ActionResult DeleteDichVu(int maDichVu)
        {
            var dbDelete = context.DICHVUs.First(m => m.MADICHVU == maDichVu);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeleteDichVu(int maDichVu, FormCollection collection)
        {
            var dbDelete = context.DICHVUs.Where(m => m.MADICHVU == maDichVu).First();
            context.DICHVUs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListDichVu");
        }
        public ActionResult DetailDichVu(int maDichVu)
        {
            var D_dichvu = context.DICHVUs.Where(m => m.MADICHVU == maDichVu).First();
            return View(D_dichvu);
        }
    }
}