using System;
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
                foreach (var phieuDatPhongDangDuocThue in listPhieuDatPhong)
                {
                    if (phongDangDuocThue.MAPHONG == Convert.ToInt32(phieuDatPhongDangDuocThue.MAPHONG) && Convert.ToDateTime(phieuDatPhongDangDuocThue.NGAYTRADUKIEN) <= ngayNhanPhong)
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
            return View(listPhongSapDuocTra);
        }
    }
}