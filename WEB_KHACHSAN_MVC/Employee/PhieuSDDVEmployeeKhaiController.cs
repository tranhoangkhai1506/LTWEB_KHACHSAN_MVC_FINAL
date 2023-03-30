using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Employee
{
    public class PhieuSDDVEmployeeKhaiController : Controller
    {
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();

        // GET: PhieuSDDVEmployeeKhai
        public ActionResult Index()
        {
            var listPhieuSDDV = from all in data.PHIEUSDDVs select all;
            return View(listPhieuSDDV);
        }
        public ActionResult Details(int id)
        {
            var D_PhieuSDDV = data.PHIEUSDDVs.Where(m => Convert.ToInt32(m.MAPHIEUSDDV) == id).First();
            return View(D_PhieuSDDV);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, PHIEUSDDV psd)
        {
            var C_ngaysudung = collection["NGAYSUDUNG"];
            var C_madichvu = collection["MADICHVU"];
            var C_maphieudatphong = collection["MAPHIEUDATPHONG"];
            var C_manhanvien = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(C_maphieudatphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                psd.NGAYSUDUNG = Convert.ToDateTime(C_ngaysudung);
                psd.MADICHVU = int.Parse(C_madichvu);
                psd.MAPHIEUDATPHONG = int.Parse(C_maphieudatphong);
                psd.MANHANVIEN = 1;
                data.PHIEUSDDVs.InsertOnSubmit(psd);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var db = data.PHIEUSDDVs.First(m => m.MAPHIEUSDDV == id);
            return View(db);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var psd = data.PHIEUSDDVs.First(m => m.MAPHIEUSDDV == id);
            var C_ngaysudung = collection["NGAYSUDUNG"];
            var C_madichvu = collection["MADICHVU"];
            var C_maphieudatphong = collection["MAPHIEUDATPHONG"];
            var C_manhanvien = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(C_maphieudatphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                psd.NGAYSUDUNG = Convert.ToDateTime(C_ngaysudung);
                psd.MADICHVU = int.Parse(C_madichvu);
                psd.MAPHIEUDATPHONG = int.Parse(C_maphieudatphong);
                psd.MANHANVIEN = int.Parse(C_manhanvien);
                UpdateModel(psd);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = data.PHIEUSDDVs.First(m => m.MAPHIEUSDDV == id);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = data.PHIEUSDDVs.Where(m => m.MAPHIEUSDDV == id).First();
            data.PHIEUSDDVs.DeleteOnSubmit(dbDelete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}