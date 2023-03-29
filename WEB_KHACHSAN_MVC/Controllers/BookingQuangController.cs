using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
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
            if(ngayNhanPhong < DateTime.Now)
            {
                ViewBag.Thongbao_Checkin = "The check-out date must be later than today.";
                return View("Index");
            }
            if (ngayTraDuKien <= ngayNhanPhong)
            {
                ViewBag.Thongbao_Checkout = "The check-out date must be later than the check-in date.";
                return View("Index");
            }

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
            if (int.Parse(C_songuoi) == 0)
            {
                ViewData["SoNguoi"] = "Số người không hợp lệ";
            }
            else if (Convert.ToDateTime(C_ngaynhanphong) < DateTime.Now)
            {
                ViewData["NgayNhanPhong"] = "Ngày nhận phòng phải lớn hơn ngày hiện tại!!";
            }
            else if (Convert.ToDateTime(C_ngaytradukien) < DateTime.Now && Convert.ToDateTime(C_ngaytradukien) < Convert.ToDateTime(C_ngaynhanphong))
            {
                ViewData["NgayTraPhong"] = "Ngày trả phòng không hợp lệ!!";
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
                        ngayNhanPhong = Convert.ToDateTime(pd.NGAYNHANPHONG);
                        pd.NGAYTRADUKIEN = Convert.ToDateTime(C_ngaytradukien);
                        pd.MAPHONG = maPhongBooking;
                        pd.MAKH = khachhang.MAKH;
                        pd.MANHANVIEN = 1;
                        pd.TIENCOC = 100000;
                        context.PHIEUDATPHONGs.InsertOnSubmit(pd);
                        phongDuocThue.TINHTRANG = "Đang được thuê";
                        context.SubmitChanges();
                        return RedirectToAction("QuestionUseOrtherService", "BookingQuang");
                    } 
                }
            }
            return this.TaoPhieuDatPhong();
        }
        public static int maPhongBooking;
        public static DateTime ngayNhanPhong;
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
            KHACHHANG khachhang = context.KHACHHANGs.Where(p => p.CCCD == long.Parse(cccdKhachHang_Booking)).FirstOrDefault();
            // lay phieu thue => lastordefautl


            if (khachhang != null && khachhang.EMAIL != "")
            {
                PHIEUDATPHONG phieuthue_KH = context.PHIEUDATPHONGs.Where(p => p.MAKH == khachhang.MAKH).ToArray().Last();
                EmailViewModel emailVm = new EmailViewModel();

                emailVm.EmailBody = @"<h2>Hello " + khachhang.TENKH + "! </h2> <br />"+
                                "<h3>THÔNG TIN PHIẾU ĐẶT PHÒNG</h3>"+ 
                                "Mã Phiếu đặt Phòng: " + phieuthue_KH.MAPHIEUDATPHONG + "<br/>" +
                                "Ngày Nhận Phòng: " + phieuthue_KH.NGAYNHANPHONG + "<br/>" +
                                "Ngày Trả Phòng Dự Kiến: " + phieuthue_KH.NGAYTRADUKIEN + "<br/>" +
                                "Ngày gửi: " + DateTime.Now.ToString() + "<br/>" +
                                "<br/>Thanks for using our services!";

                emailVm.SenderEmailAddress = System.Configuration.ConfigurationManager.AppSettings["SenderEmail"];
                emailVm.SenderPassword = System.Configuration.ConfigurationManager.AppSettings["SenderPassword"];
                emailVm.SmtpHostServer = System.Configuration.ConfigurationManager.AppSettings["smtpHostServer"];
                emailVm.SmtpPort = Convert.ToInt32(System.Configuration.ConfigurationManager.AppSettings["smtpPort"]);
                emailVm.ReceiverEmailAddress = khachhang.EMAIL;
                emailVm.EmailSubject = "LOTUS - COMFIRM";
                try
                {
                    var client = new SmtpClient(emailVm.SmtpHostServer, emailVm.SmtpPort)
                    {
                        EnableSsl = true,
                        Timeout = 100000,
                        DeliveryMethod = SmtpDeliveryMethod.Network,
                        UseDefaultCredentials = false,
                        Credentials = new NetworkCredential(emailVm.SenderEmailAddress, emailVm.SenderPassword)
                    };
                    var mailMessage = new MailMessage
                    {
                        IsBodyHtml = true,
                        BodyEncoding = Encoding.UTF8,
                        From = new MailAddress(emailVm.SenderEmailAddress)
                    };
                    mailMessage.To.Add(emailVm.ReceiverEmailAddress);
                    mailMessage.Subject = emailVm.EmailSubject;
                    mailMessage.Body = emailVm.EmailBody;
                    client.Send(mailMessage);
                    return View();
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View();
        }

        public ActionResult QuestionUseOrtherService()
        {
            return View();
        }

        public ActionResult DanhSachDichVu()
        {
            var listDichVu = from all in context.DICHVUs select all;
            return View(listDichVu);
        }

        public static int maDichVuBooking;

        public ActionResult CreateSuDungDichVu(int maDichVu)
        {
            maDichVuBooking = maDichVu;
            return View();
        }
        [HttpPost]
        public ActionResult CreateSuDungDichVu(FormCollection collection, PHIEUSDDV psd)
        {
            KHACHHANG khachhang = context.KHACHHANGs.Where(p => p.CCCD == long.Parse(cccdKhachHang_Booking)).FirstOrDefault();
            var C_ngaysudung = collection["NGAYSUDUNG"];
            var C_madichvu = collection["MADICHVU"];
            var C_manhanvien = collection["MANHANVIEN"];
            PHIEUDATPHONG maPhieuDatPhong = new PHIEUDATPHONG();
             if (khachhang != null)
             {
                maPhieuDatPhong = context.PHIEUDATPHONGs.Where(
                p => p.MAKH == khachhang.MAKH && p.NGAYNHANPHONG == ngayNhanPhong && p.MAPHONG == maPhongBooking).FirstOrDefault();
             }

            if (string.IsNullOrEmpty(C_madichvu))
            {
                ViewData["Error"] = "Service ID don't empty!";
            }
            else if (maPhieuDatPhong != null)
            {
                psd.NGAYSUDUNG = Convert.ToDateTime(C_ngaysudung);
                psd.MADICHVU = int.Parse(C_madichvu);
                psd.MAPHIEUDATPHONG = maPhieuDatPhong.MAPHIEUDATPHONG;
                psd.MANHANVIEN = 1;
                context.PHIEUSDDVs.InsertOnSubmit(psd);
                context.SubmitChanges();
                return RedirectToAction("Payment", "VnPayQuang");
            }
            return this.CreateSuDungDichVu(maDichVuBooking);
        }
    }
}