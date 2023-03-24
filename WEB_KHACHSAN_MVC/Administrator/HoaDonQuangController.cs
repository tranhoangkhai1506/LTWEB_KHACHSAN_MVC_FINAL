using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class HoaDonQuangController : Controller
    {
        // GET: HoaDon
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        public ActionResult Index()
        {
            var list_HoaDon = from all in data.HOADONs select all;
            return View(list_HoaDon);
        }
        public ActionResult Detail(int id)
        {
            var D_hoadon = data.HOADONs.Where(m => m.MAHOADON == id).First();
            return View(D_hoadon);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, HOADON hd)
        {
            var E_tongtien = collection["TONGTIEN"];
            var E_ngaythanhtoan = collection["NGAYTHANHTOAN"];
            var E_maphieudatphong= collection["MAPHIEUDATPHONG"];
            var E_manhanvien = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(E_manhanvien))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                hd.TONGTIEN = Convert.ToDecimal(E_tongtien);
                hd.NGAYTHANHTOAN = Convert.ToDateTime(E_ngaythanhtoan);
                hd.MAPHIEUDATPHONG = int.Parse(E_maphieudatphong);
                hd.MANHANVIEN = int.Parse(E_manhanvien);
                data.HOADONs.InsertOnSubmit(hd);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var db = data.HOADONs.First(m => m.MAHOADON == id);
            return View(db);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var hd = data.HOADONs.First(m => m.MAHOADON == id);
            var E_tongtien = collection["TONGTIEN"];
            var E_ngaythanhtoan = collection["NGAYTHANHTOAN"];
            var E_maphieudatphong = collection["MAPHIEUDATPHONG"];
            var E_manhanvien = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(E_manhanvien))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                hd.TONGTIEN = Convert.ToDecimal(E_tongtien);
                hd.NGAYTHANHTOAN = Convert.ToDateTime(E_ngaythanhtoan);
                hd.MAPHIEUDATPHONG = int.Parse(E_maphieudatphong);
                hd.MANHANVIEN = int.Parse(E_manhanvien);
                UpdateModel(hd);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = data.HOADONs.First(m => m.MAHOADON == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = data.HOADONs.Where(m => m.MAHOADON == id).First();
            data.HOADONs.DeleteOnSubmit(dbDelete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}