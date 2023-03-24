﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class BookingQuangController : Controller
    {
        // GET: BookingQuang
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Booking(DateTime ngayNhanPhong, DateTime ngayTraDuKien, int soNguoi)
        {
            var listPhongTrong_BinhThuong = context.PHONGs.Where(p => p.TINHTRANG.Contains("Bình thường") && p.SONGUOITOIDA >= soNguoi).ToList();
            var listPhongDangDuocThue = context.PHONGs.Where(p => p.TINHTRANG.Contains("Đang được thuê") && p.SONGUOITOIDA >= soNguoi).ToList();
            List<PHIEUDATPHONG> listPhieuDatPhong = context.PHIEUDATPHONGs.ToList();
            List<PHONG> listPhongSapDuocTra = new List<PHONG>();
            foreach (var phongDangDuocThue in listPhongDangDuocThue)
            {
                PHIEUDATPHONG PhieuDatPhong_Cuoi = listPhieuDatPhong.LastOrDefault(p => p.MAPHONG == phongDangDuocThue.MAPHONG);

                if (PhieuDatPhong_Cuoi != null)
                {
                    if (Convert.ToDateTime(PhieuDatPhong_Cuoi.NGAYTRADUKIEN) <= ngayNhanPhong)
                    {
                        listPhongSapDuocTra.Add(phongDangDuocThue);
                    } 
                }
            }
            if (listPhongTrong_BinhThuong.Count() != 0)
            {
                return View(listPhongTrong_BinhThuong);
            }
            else if (listPhongSapDuocTra.Count() != 0)
            {
                return View(listPhongSapDuocTra);
            }
            else
            {
                var all_PhongTrong = context.PHONGs.Where(p => p.TINHTRANG == "Bình thường").ToList();
                return Redirect(@Url.Action("ThongBaoKhongCoPhongTrong","BookingQuang", "all_PhongTrong"));
            }            
        }

        public ActionResult ThongBaoKhongCoPhongTrong()
        {
            var all_PhongTrong = context.PHONGs.Where(p => p.TINHTRANG == "Bình thường").ToList();
            return View(all_PhongTrong);
        }

        public ActionResult TaoPhieuDatPhong()
        {
            return View();
        }
        [HttpPost]
        public ActionResult TaoPhieuDatPhong(FormCollection collection, PHIEUDATPHONG pd)
        {
            var C_songuoi = collection["SONGUOI"];
            var C_ngaynhanphong = collection["NGAYNHANPHONG"];
            var C_ngaytradukien = collection["NGAYTRADUKIEN"];
            var E_tiencoc = collection["TIENCOC"];
            if (string.IsNullOrEmpty(E_tiencoc))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                KHACHHANG khachhang = context.KHACHHANGs.Where(p => p.CCCD == long.Parse(cccdKhachHang_Booking)).FirstOrDefault();
                PHONG phongDuocThue = context.PHONGs.Where(p => p.MAPHONG == maPhongBooking).FirstOrDefault();
                if (phongDuocThue != null)
                {
                    if (khachhang != null)
                    {
                        pd.SONGUOI = int.Parse(C_songuoi);
                        pd.NGAYNHANPHONG = Convert.ToDateTime(C_ngaynhanphong);
                        pd.NGAYTRADUKIEN = Convert.ToDateTime(C_ngaytradukien);
                        pd.MAPHONG = maPhongBooking;
                        pd.MAKH = khachhang.MAKH;
                        pd.MANHANVIEN = 1;
                        pd.TIENCOC = decimal.Parse(E_tiencoc);
                        context.PHIEUDATPHONGs.InsertOnSubmit(pd);
                        phongDuocThue.TINHTRANG = "Đang được thuê";
                        context.SubmitChanges();
                        return RedirectToAction("DangKyThanhCong");
                    } 
                }
            }
            return this.TaoPhieuDatPhong();
        }
        public static int maPhongBooking;
        public ActionResult ThemKhachHang(int MaPhong)
        {
            maPhongBooking = MaPhong;
            return View();
        }
        public static string cccdKhachHang_Booking;
        [HttpPost]    
        public ActionResult ThemKhachHang(FormCollection collection, KHACHHANG kh)
        {
            var E_TenKhachHang = collection["TENKH"];
            var E_CCCD = collection["CCCD"];
            var E_email = collection["EMAIL"];
            var E_SDT = collection["DT"];
            if (string.IsNullOrEmpty(E_TenKhachHang))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                cccdKhachHang_Booking = E_CCCD;
                kh.TENKH = E_TenKhachHang;
                kh.CCCD = int.Parse(E_CCCD);
                kh.EMAIL = E_email;
                kh.DT = E_SDT;
                context.KHACHHANGs.InsertOnSubmit(kh);
                context.SubmitChanges();
                return RedirectToAction("TaoPhieuDatPhong");
            }
            return this.ThemKhachHang(maPhongBooking);
        }

        public ActionResult DangKyThanhCong()
        {
           
            return View();
        }
    }
}