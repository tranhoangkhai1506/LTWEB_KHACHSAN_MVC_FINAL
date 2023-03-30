using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Controllers
{
    public class RoomQuangController : Controller
    {
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();
        // GET: RoomQuang
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ViewPhongLoai1()
        {
            var all_Phong = data.PHONGs.ToList();
            var all_LoaiPhong = data.LOAIPHONGs.ToList();
            List<PHONG> listPhongDon = new List<PHONG>();
            foreach (var phong in all_Phong)
            {
                foreach (var loai in all_LoaiPhong)
                {
                    if (phong.MALOAIPHONG == loai.MALOAIPHONG && String.Equals(loai.TENLOAIPHONG, "phòng đơn", StringComparison.OrdinalIgnoreCase))
                    {
                        listPhongDon.Add(phong);
                    }
                }
            }
            return View(listPhongDon);
        }
        public ActionResult ViewPhongLoai2()
        {
            var all_Phong = data.PHONGs.ToList();
            var all_LoaiPhong = data.LOAIPHONGs.ToList();
            List<PHONG> listPhongDoi = new List<PHONG>();
            foreach (var phong in all_Phong)
            {
                foreach (var loai in all_LoaiPhong)
                {
                    if (phong.MALOAIPHONG == loai.MALOAIPHONG && String.Equals(loai.TENLOAIPHONG, "phòng đôi", StringComparison.OrdinalIgnoreCase))
                    {
                        listPhongDoi.Add(phong);
                    }
                }
            }
            return View(listPhongDoi);
        }
        public ActionResult ViewPhongLoai3()
        {
            var all_Phong = data.PHONGs.ToList();
            var all_LoaiPhong = data.LOAIPHONGs.ToList();
            List<PHONG> listPhongVIP = new List<PHONG>();
            foreach (var phong in all_Phong)
            {
                foreach (var loai in all_LoaiPhong)
                {
                    if (phong.MALOAIPHONG == loai.MALOAIPHONG && String.Equals(loai.TENLOAIPHONG, "phòng vip", StringComparison.OrdinalIgnoreCase))
                    {
                        listPhongVIP.Add(phong);
                    }
                }
            }
            return View(listPhongVIP);
        }
    }
}