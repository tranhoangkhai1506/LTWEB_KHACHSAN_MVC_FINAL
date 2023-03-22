using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class TaiKhoanHuyController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // GET: TaiKhoanHuy
        public ActionResult ListTaiKhoan()
        {
            var all_taikhoan = context.TAIKHOANs.ToList();
            return View(all_taikhoan);
        }
        public ActionResult CreateTaiKhoan()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateTaiKhoan(FormCollection collection, TAIKHOAN tk)
        {
            var E_tendangnhap = collection["TENDANGNHAP"];
            var E_matkhau = collection["MATKHAU"];
            var E_manhanvien = collection["MANHANVIEN"];
            var E_maphanquyen = collection["MAPHANQUYEN"];
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tk.TENDANGNHAP = E_tendangnhap;
                tk.MATKHAU = E_matkhau;
                tk.MANHANVIEN = int.Parse(E_manhanvien);
                tk.MAPHANQUYEN = int.Parse(E_maphanquyen);
                context.TAIKHOANs.InsertOnSubmit(tk);
                context.SubmitChanges();
                return RedirectToAction("ListTaiKhoan");
            }
            return this.CreateTaiKhoan();
        }

        public ActionResult EditTaiKhoan(String tenDangNhap)
        {
            var db = context.TAIKHOANs.First(m => m.TENDANGNHAP == tenDangNhap);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditTaiKhoan(String tenDangNhap, FormCollection collection)
        {
            TAIKHOAN db = context.TAIKHOANs.FirstOrDefault(p => p.TENDANGNHAP == tenDangNhap);
            var E_tendangnhap = collection["TENDANGNHAP"];
            var E_matkhau = collection["MATKHAU"];
            var E_manhanvien = collection["MANHANVIEN"];
            var E_maphanquyen = collection["MAPHANQUYEN"];
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENDANGNHAP = E_tendangnhap;
                db.MATKHAU = E_matkhau;
                db.MANHANVIEN = int.Parse(E_manhanvien);
                db.MAPHANQUYEN = int.Parse(E_maphanquyen);
                context.TAIKHOANs.InsertOnSubmit(db);
                context.SubmitChanges();
                return RedirectToAction("ListTaiKhoan");
            }
            return this.EditTaiKhoan(tenDangNhap);
        }

        public ActionResult DeleteTaiKhoan(String tenDangNhap)
        {
            var dbDelete = context.TAIKHOANs.First(m => m.TENDANGNHAP == tenDangNhap);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeleteTaiKhoan(String tenDangNhap, FormCollection collection)
        {
            var dbDelete = context.TAIKHOANs.Where(m => m.TENDANGNHAP == tenDangNhap).First();
            context.TAIKHOANs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListTaiKhoan");
        }

    }
}