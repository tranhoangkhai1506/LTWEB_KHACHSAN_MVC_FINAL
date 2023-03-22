using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class NhanVienHuyController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // GET: NhanVienHuy
        public ActionResult ListNhanVien()
        {
            var all_nhanvien = context.NHANVIENs.ToList();
            return View(all_nhanvien);
        }

        public ActionResult CreateNhanVien()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateNhanVien(FormCollection collection, NHANVIEN nv)
        {
            var E_tennhanvien = collection["TENNHANVIEN"];
            var E_namsinh = collection["NAMSINH"];
            var E_email = collection["EMAIL"];
            var E_cccd = collection["CCCD"];
            var E_chucvu = collection["CHUCVU"];
            var E_diachi = collection["DIACHI"];
            var E_dienthoai = collection["DT"];
            if (string.IsNullOrEmpty(E_tennhanvien))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                nv.TENNHANVIEN = E_tennhanvien;
                nv.NAMSINH = short.Parse(E_namsinh);
                nv.EMAIL = E_email;
                nv.CCCD = long.Parse(E_cccd);
                nv.CHUCVU = E_chucvu;
                nv.DIACHI = E_diachi;
                nv.DT = E_dienthoai;
                context.NHANVIENs.InsertOnSubmit(nv);
                context.SubmitChanges();
                return RedirectToAction("ListNhanVien");
            }
            return this.CreateNhanVien();
        }
        // Sửa phòng
        public ActionResult EditNhanVien(int maNhanVien)
        {
            var db = context.NHANVIENs.First(m => m.MANHANVIEN == maNhanVien);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditNhanVien(int maNhanVien, FormCollection collection)
        {
            NHANVIEN db = context.NHANVIENs.FirstOrDefault(p => p.MANHANVIEN == maNhanVien);
            var E_tennhanvien = collection["TENNHANVIEN"];
            var E_namsinh = collection["NAMSINH"];
            var E_email = collection["EMAIL"];
            var E_cccd = collection["CCCD"];
            var E_chucvu = collection["CHUCVU"];
            var E_diachi = collection["DIACHI"];
            var E_dienthoai = collection["DT"];
            if (string.IsNullOrEmpty(E_tennhanvien))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENNHANVIEN = E_tennhanvien;
                db.NAMSINH = short.Parse(E_namsinh);
                db.EMAIL = E_email;
                db.CCCD = long.Parse(E_cccd);
                db.CHUCVU = E_chucvu;
                db.DIACHI = E_diachi;
                db.DT = E_dienthoai;

                context.SubmitChanges();
                return RedirectToAction("ListNhanVien");
            }
            return this.EditNhanVien(maNhanVien);
        }
        // Xóa phòng
        public ActionResult DeleteNhanVien(int maNhanVien)
        {
            var dbDelete = context.NHANVIENs.First(m => m.MANHANVIEN == maNhanVien);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeleteNhanVien(int maNhanVien, FormCollection collection)
        {
            var dbDelete = context.NHANVIENs.Where(m => m.MANHANVIEN == maNhanVien).First();
            context.NHANVIENs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListNhanVien");
        }

    }
}