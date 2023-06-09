﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
            string ketquaMatKhau = "";
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else if (kiemTraMatKhauDauVao(E_matkhau, out ketquaMatKhau) == false)
            {
                ViewData["ChuoiMatKhauSai"] = ketquaMatKhau;
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

        public ActionResult EditTaiKhoan(string tenDangNhap)
        {
            var db = context.TAIKHOANs.First(m => m.TENDANGNHAP == tenDangNhap);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditTaiKhoan(string tenDangNhap, FormCollection collection)
        {
            TAIKHOAN db = context.TAIKHOANs.FirstOrDefault(p => p.TENDANGNHAP == tenDangNhap);
            var E_tendangnhap = collection["TENDANGNHAP"];
            var E_matkhau = collection["MATKHAU"];
            var E_manhanvien = collection["MANHANVIEN"];
            var E_maphanquyen = collection["MAPHANQUYEN"];
            string ketquaMatKhau = "";
            if (string.IsNullOrEmpty(E_tendangnhap))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else if (kiemTraMatKhauDauVao(E_matkhau, out ketquaMatKhau) == false)
            {
                ViewData["ChuoiMatKhauSai"] = ketquaMatKhau;
            }
            else
            {
                db.TENDANGNHAP = E_tendangnhap;
                db.MATKHAU = E_matkhau;
                db.MANHANVIEN = int.Parse(E_manhanvien);
                db.MAPHANQUYEN = int.Parse(E_maphanquyen);
                context.SubmitChanges();
                return RedirectToAction("ListTaiKhoan");
            }
            return this.EditTaiKhoan(tenDangNhap);
        }
        public ActionResult Details(string id)
        {
            var D_sach = context.TAIKHOANs.Where(m => m.TENDANGNHAP == id).FirstOrDefault();
            return View(D_sach);
        }

        public ActionResult DeleteTaiKhoan(string tenDangNhap)
        {
            var dbDelete = context.TAIKHOANs.First(m => m.TENDANGNHAP == tenDangNhap);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeleteTaiKhoan(string tenDangNhap, FormCollection collection)
        {
            var dbDelete = context.TAIKHOANs.Where(m => m.TENDANGNHAP == tenDangNhap).First();
            context.TAIKHOANs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListTaiKhoan");
        }

        private bool kiemTraMatKhauDauVao(string matKhau, out string ketQua)
        {
            var input = matKhau;
            ketQua = string.Empty;

            if (string.IsNullOrWhiteSpace(input))
            {
                throw new Exception("Password should not be empty");
            }

            var hasNumber = new Regex(@"[0-9]+");
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasMiniMaxChars = new Regex(@".{6,8}");
            var hasLowerChar = new Regex(@"[a-z]+");
            var hasSymbols = new Regex(@"[!@#$%^&*()_+=\[{\]};:<>|./?,-]");

            if (!hasLowerChar.IsMatch(input))
            {
                ketQua = "Mật khẩu phải có một kí tự chữ cái thường";
                return false;
            }
            else if (!hasUpperChar.IsMatch(input))
            {
                ketQua = "Mật khẩu phải có một kí tự chữ cái hoa";
                return false;
            }
            else if (!hasMiniMaxChars.IsMatch(input))
            {
                ketQua = "Mật khẩu phải có ít nhất 6 kí tự đến 8 kí tự";
                return false;
            }
            else if (!hasNumber.IsMatch(input))
            {
                ketQua = "Mật khẩu phải có một kí tự số";
                return false;
            }

            else if (!hasSymbols.IsMatch(input))
            {
                ketQua = "Mật khẩu phải có một kí tự đặc biệt";
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}