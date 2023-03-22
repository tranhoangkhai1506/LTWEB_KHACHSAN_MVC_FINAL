using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class KhachHangQuangController : Controller
    {
        // GET: KhachHang
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();

        public ActionResult Index()
        {
            var list_KhachHang = from all in data.KHACHHANGs select all;
            return View(list_KhachHang);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, DICHVU dv)
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
                data.DICHVUs.InsertOnSubmit(dv);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var db = data.KHACHHANGs.First(m => m.MAKH == id);
            return View(db);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var kh = data.KHACHHANGs.First(m => m.MAKH == id);
            var E_tenkh = collection["TENKH"];
            var E_cccd = collection["CCCD"];
            var E_email = collection["EMAIL"];
            var E_dt = collection["DT"];
            if (string.IsNullOrEmpty(E_tenkh))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                kh.TENKH = E_tenkh;
                kh.CCCD = long.Parse(E_cccd);
                kh.EMAIL = E_email;
                kh.DT = E_dt;
                UpdateModel(kh);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = data.KHACHHANGs.First(m => m.MAKH == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = data.KHACHHANGs.Where(m => m.MAKH == id).First();
            data.KHACHHANGs.DeleteOnSubmit(dbDelete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}