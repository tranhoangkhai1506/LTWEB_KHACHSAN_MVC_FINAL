﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class KhachHangQuangController : Controller
    {
        // GET: KhachHang
        KhachSanDBContextDataContext data = new KhachSanDBContextDataContext();

        public ActionResult Index()
        {
            var list_KhachHang = from all in data.KHACHHANGs select all;
            return View(list_KhachHang);
        }
        public ActionResult Detail(int id)
        {
            var D_khachhang = data.KHACHHANGs.Where(m => m.MAKH == id).First();
            return View(D_khachhang);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, KHACHHANG kh)
        {
            var E_TenKhachHang= collection["TENKH"];
            var E_CCCD = collection["CCCD"];
            var E_email = collection["EMAIL"];
            var E_SDT = collection["DT"];

            KHACHHANG kiemtraKH = data.KHACHHANGs.Where(p => p.CCCD == long.Parse(E_CCCD)).FirstOrDefault();
            if (string.IsNullOrEmpty(E_TenKhachHang))
            {
                ViewData["Error_Name"] = "Don't empty!";
            }
            else if(kiemtraKH != null)
            {
                ViewData["Error_Client"] = "Da co khach hang!";
            }
            else
            {
                kh.TENKH = E_TenKhachHang;
                kh.CCCD = int.Parse(E_CCCD);
                kh.EMAIL = E_email;
                kh.DT = E_SDT;
                data.KHACHHANGs.InsertOnSubmit(kh);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }

        public ActionResult Edit(int id)
        {
            var db = data.KHACHHANGs.First(m => m.MAKH == id);
            return View(db);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var kh = data.KHACHHANGs.First(m => m.MAKH == id);
            var E_tenkh = collection["TENKH"];
            var E_cccd = collection["CCCD"];
            var E_email = collection["EMAIL"];
            var E_dt = collection["DT"];
            if (string.IsNullOrEmpty(E_tenkh))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                kh.TENKH = E_tenkh;
                kh.CCCD = long.Parse(E_cccd);
                kh.EMAIL = E_email;
                kh.DT = E_dt;
                UpdateModel(kh);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }

        public ActionResult Delete(int id)
        {
            var dbDelete = data.KHACHHANGs.First(m => m.MAKH == id);
            if (dbDelete != null)
            {
                PHIEUDATPHONG phieuDatPhong = data.PHIEUDATPHONGs.Where(p => p.MAKH == dbDelete.MAKH).FirstOrDefault();
                if (phieuDatPhong != null)
                {
                    HOADON hoaDon = data.HOADONs.Where(p => p.MAPHIEUDATPHONG == phieuDatPhong.MAPHIEUDATPHONG).FirstOrDefault();
                    if (hoaDon != null)
                    {
                        ViewData["Khongduocxoa"] = "Can`t be deleted! Try again!";
                    }
                    ViewData["Khongduocxoa"] = "Can`t be deleted! Try again!";
                }
            }
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var dbDelete = data.KHACHHANGs.Where(m => m.MAKH == id).First();
            data.KHACHHANGs.DeleteOnSubmit(dbDelete);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
    }
}