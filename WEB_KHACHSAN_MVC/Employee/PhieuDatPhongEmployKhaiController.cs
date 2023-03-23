using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Employee
{
    public class PhieuDatPhongEmployKhaiController : Controller
    {
        // GET: PhieuDatPhongEmployKhai
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        // GET: PhieuDatPhongQuang
        public ActionResult Index()
        {
            var listPhieuDatPhong = from all in data.PHIEUDATPHONGs select all;
            return View(listPhieuDatPhong);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, PHIEUDATPHONG pd)
        {
            var C_songuoi = collection["SONGUOI"];
            var C_ngaynhanphong = collection["NGAYNHANPHONG"];
            var C_ngaytradukien = collection["NGAYTRADUKIEN"];
            var C_maphong = collection["MAPHONG"];
            var C_makh = collection["MAKH"];
            var C_manhanvien = collection["MANHANVIEN"];

            var E_tiencoc = collection["TIENCOC"];
            if (string.IsNullOrEmpty(C_maphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                pd.SONGUOI = int.Parse(C_songuoi);
                pd.NGAYNHANPHONG = Convert.ToDateTime(C_ngaynhanphong);
                pd.NGAYTRADUKIEN = Convert.ToDateTime(C_ngaytradukien);
                pd.MAPHONG = int.Parse(C_maphong);
                pd.MAKH = int.Parse(C_makh);
                pd.MANHANVIEN = int.Parse(C_manhanvien);
                pd.TIENCOC = decimal.Parse(E_tiencoc);
                data.PHIEUDATPHONGs.InsertOnSubmit(pd);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var db = data.PHIEUDATPHONGs.First(m => m.MAPHIEUDATPHONG == id);
            return View(db);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var pdp = data.PHIEUDATPHONGs.First(m => m.MAPHIEUDATPHONG == id);
            var E_songuoi = collection["SONGUOI"];
            var E_ngaynhanphong = collection["NGAYNHANPHONG"];
            var E_ngaytradukien = collection["NGAYTRADUKIEN"];
            var E_maphong = collection["MAPHONG"];
            var E_makh = collection["MAKH"];
            var E_manhanvien = collection["MANHANVIEN"];
            var E_tiencoc = collection["TIENCOC"];
            if (string.IsNullOrEmpty(E_maphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                pdp.SONGUOI = int.Parse(E_songuoi); ;
                pdp.NGAYNHANPHONG = Convert.ToDateTime(E_ngaynhanphong);
                pdp.NGAYTRADUKIEN = Convert.ToDateTime(E_ngaytradukien);
                pdp.MAPHONG = int.Parse(E_maphong);
                pdp.MAKH = int.Parse(E_makh);
                pdp.MANHANVIEN = int.Parse(E_manhanvien);
                pdp.TIENCOC = decimal.Parse(E_tiencoc);
                UpdateModel(pdp);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = data.PHIEUDATPHONGs.First(m => m.MAPHIEUDATPHONG == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = data.PHIEUDATPHONGs.Where(m => m.MAPHIEUDATPHONG == id).First();
            data.PHIEUDATPHONGs.DeleteOnSubmit(dbDelete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}