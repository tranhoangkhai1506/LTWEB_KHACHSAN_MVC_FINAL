using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Employee
{
    public class QuanLyPhongEmployeeKhaiController : Controller
    {
        // GET: QuanLyPhongKhai
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // list phòng
        public ActionResult ListPhong()
        {
            var all_Phong = context.PHONGs.ToList();
            return View(all_Phong);
        }
        // Tạo phòng
        public ActionResult CreatePhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePhong(FormCollection collection, PHONG p)
        {
            var E_tenphong = collection["TENPHONG"];
            var E_songuoitoida = collection["SONGUOITOIDA"];
            var E_tinhtrang = collection["TINHTRANG"];
            var E_hinh = collection["HINH"];
            var E_maloaiphong = collection["MALOAIPHONG"];
            if (string.IsNullOrEmpty(E_tenphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                p.TENPHONG = E_tenphong;
                p.SONGUOITOIDA = int.Parse(E_songuoitoida);
                p.TINHTRANG = E_tinhtrang;
                p.HINH = E_hinh;
                p.MALOAIPHONG = int.Parse(E_maloaiphong);


                context.PHONGs.InsertOnSubmit(p);
                context.SubmitChanges();
                return RedirectToAction("ListPhong");
            }
            return this.CreatePhong();
        }
        // Sửa phòng
        public ActionResult EditPhong(int maPhong)
        {
            var db = context.PHONGs.First(m => m.MAPHONG == maPhong);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditPhong(int maPhong, FormCollection collection)
        {
            PHONG db = context.PHONGs.FirstOrDefault(p => p.MAPHONG == maPhong);
            var E_tenphong = collection["TENPHONG"];
            var E_songuoitoida = collection["SONGUOITOIDA"];
            var E_tinhtrang = collection["TINHTRANG"];
            var E_hinh = collection["HINH"];
            var E_maloaiphong = collection["MALOAIPHONG"];
            if (string.IsNullOrEmpty(E_tenphong))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENPHONG = E_tenphong;
                db.SONGUOITOIDA = int.Parse(E_songuoitoida);
                db.TINHTRANG = E_tinhtrang;
                db.HINH = E_hinh;
                db.MALOAIPHONG = int.Parse(E_maloaiphong);
                context.SubmitChanges();
                return RedirectToAction("ListPhong");
            }
            return this.EditPhong(maPhong);
        }
        // Xóa phòng
        public ActionResult DeletePhong(int maPhong)
        {
            var dbDelete = context.PHONGs.First(m => m.MAPHONG == maPhong);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeletePhong(int maPhong, FormCollection collection)
        {
            var dbDelete = context.PHONGs.Where(m => m.MAPHONG == maPhong).First();
            context.PHONGs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListPhong");
        }

        public ActionResult Details(int id)
        {
            var D_sach = context.PHONGs.Where(m => m.MAPHONG == id).FirstOrDefault();
            return View(D_sach);
        }
    }
}