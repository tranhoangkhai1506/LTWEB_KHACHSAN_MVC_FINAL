using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class PhieuDatPhongKhaiController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        // GET: PhieuDatPhongKhai
        public ActionResult ViewPhieuDatPhong()
        {
            var all_PDP = context.PHIEUDATPHONGs.ToList();
            return View(all_PDP);
        }
        public ActionResult CreatePhieuDatPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePhieuDatPhong(FormCollection collection, PHIEUDATPHONG phieuDat)
        {
            var E_SoNguoi = collection["SONGUOI"];
            var E_NgayNhanPhong = collection["NGAYNHANPHONG"];
            var E_NgayTraDuKien = collection["NGAYTRADUKIEN"];
            var E_TienCoc = collection["TIENCOC"];
            var E_MaPhong = collection["MAPHONG"];
            var E_MaKH = collection["MAKH"];
            var E_MaNV = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(E_SoNguoi))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                phieuDat.SONGUOI = int.Parse(E_SoNguoi);
                phieuDat.NGAYNHANPHONG = DateTime.Parse(E_NgayNhanPhong);
                phieuDat.NGAYTRADUKIEN = DateTime.Parse(E_NgayTraDuKien);
                phieuDat.TIENCOC = int.Parse(E_TienCoc);
                phieuDat.MAPHONG = int.Parse(E_MaPhong);
                phieuDat.MAKH = int.Parse(E_MaKH);
                phieuDat.MANHANVIEN = int.Parse(E_MaNV);


                context.PHIEUDATPHONGs.InsertOnSubmit(phieuDat);
                context.SubmitChanges();
                return RedirectToAction("ViewPhieuDatPhong");
            }
            return this.CreatePhieuDatPhong();
        }

        public ActionResult EditPhieuDatPhong(int maPhieuDatPhong)
        {
            var phieuDatPhong = context.PHIEUDATPHONGs.FirstOrDefault(m => m.MAPHIEUDATPHONG == (int)maPhieuDatPhong);
            return View(phieuDatPhong);
        }
        [HttpPost]
        public ActionResult EditPhieuDatPhong(int maPhieuDatPhong, FormCollection collection)
        {
            PHIEUDATPHONG phieuDat = context.PHIEUDATPHONGs.FirstOrDefault(p => p.MAPHIEUDATPHONG == maPhieuDatPhong);
            var E_SoNguoi = collection["SONGUOI"];
            var E_NgayNhanPhong = collection["NGAYNHANPHONG"];
            var E_NgayTraDuKien = collection["NGAYTRADUKIEN"];
            var E_TienCoc = collection["TIENCOC"];
            var E_MaPhong = collection["MAPHONG"];
            var E_MaKH = collection["MAKH"];
            var E_MaNV = collection["MANHANVIEN"];
            if (string.IsNullOrEmpty(E_SoNguoi))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                phieuDat.SONGUOI = int.Parse(E_SoNguoi);
                phieuDat.NGAYNHANPHONG = DateTime.Parse(E_NgayNhanPhong);
                phieuDat.NGAYTRADUKIEN = DateTime.Parse(E_NgayTraDuKien);
                phieuDat.TIENCOC = int.Parse(E_TienCoc);
                phieuDat.MAPHONG = int.Parse(E_MaPhong);
                phieuDat.MAKH = int.Parse(E_MaKH);
                phieuDat.MANHANVIEN = int.Parse(E_MaNV);
                context.SubmitChanges();
                return RedirectToAction("ViewPhieuDatPhong");
            }
            return this.EditPhieuDatPhong(maPhieuDatPhong);
        }

        public ActionResult DeletePhieuDatPhong(int maPhieuDatPhong)
        {
            var phieuDatPhong = context.PHIEUDATPHONGs.FirstOrDefault(m => m.MAPHIEUDATPHONG == maPhieuDatPhong);
            return View(phieuDatPhong);
        }
        [HttpPost]
        public ActionResult DeletePhieuDatPhong(int maPhieuDatPhong, FormCollection collection)
        {
            var phieuDP_Delete = context.PHIEUDATPHONGs.Where(m => m.MAPHIEUDATPHONG == maPhieuDatPhong).FirstOrDefault();
            context.PHIEUDATPHONGs.DeleteOnSubmit(phieuDP_Delete);
            context.SubmitChanges();
            return RedirectToAction("ViewPhieuDatPhong");
        }
    }
}