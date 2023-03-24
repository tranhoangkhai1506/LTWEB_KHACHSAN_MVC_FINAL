using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_KHACHSAN_MVC.Models;

namespace WEB_KHACHSAN_MVC.Administrator
{
    public class PhanQuyenHuyController : Controller
    {
        KhachSanDBContextDataContext context = new KhachSanDBContextDataContext();      
        // GET: PhanQuyenHuy
        public ActionResult ListPhanQuyen()
        {
            var all_phanquyen = context.PHANQUYENs.ToList();
            return View(all_phanquyen);
        }
        public ActionResult Details(int maPhanQuyen)
        {
            var D_PhanQuyen = context.PHANQUYENs.Where(m => Convert.ToInt32(m.MAPHANQUYEN) == maPhanQuyen).First();
            return View(D_PhanQuyen);
        }
        public ActionResult CreatePhanQuyen()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreatePhanQuyen(FormCollection collection, PHANQUYEN pq)
        {
            var E_tenphanquyen = collection["TENPHANQUYEN"];
            var E_quyenhan = collection["QUYENHAN"];
            if (string.IsNullOrEmpty(E_tenphanquyen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                pq.TENPHANQUYEN = E_tenphanquyen;
                pq.QUYENHAN = E_quyenhan;
                context.PHANQUYENs.InsertOnSubmit(pq);
                context.SubmitChanges();
                return RedirectToAction("ListPhanQuyen");
            }
            return this.CreatePhanQuyen();
        }
        // Sửa phòng
        public ActionResult EditPhanQuyen(int maPhanQuyen)
        {
            var db = context.PHANQUYENs.First(m => m.MAPHANQUYEN == maPhanQuyen);
            return View(db);
        }
        [HttpPost]
        public ActionResult EditPhanQuyen(int maPhanQuyen, FormCollection collection)
        {
            PHANQUYEN db = context.PHANQUYENs.FirstOrDefault(p => p.MAPHANQUYEN == maPhanQuyen);
            var E_tenphanquyen = collection["TENPHANQUYEN"];
            var E_quyenhan = collection["QUYENHAN"];
            if (string.IsNullOrEmpty(E_tenphanquyen))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                db.TENPHANQUYEN = E_tenphanquyen;
                db.QUYENHAN = E_quyenhan;
                context.SubmitChanges();
                return RedirectToAction("ListPhanQuyen");
            }
            return this.EditPhanQuyen(maPhanQuyen);
        }
        // Xóa phòng
        public ActionResult DeletePhanQuyen(int maPhanQuyen)
        {
            var dbDelete = context.PHANQUYENs.First(m => m.MAPHANQUYEN == maPhanQuyen);
            return View(dbDelete);
        }
        [HttpPost]
        public ActionResult DeletePhanQuyen(int maPhanQuyen, FormCollection collection)
        {
            var dbDelete = context.PHANQUYENs.Where(m => m.MAPHANQUYEN == maPhanQuyen).First();
            context.PHANQUYENs.DeleteOnSubmit(dbDelete);
            context.SubmitChanges();
            return RedirectToAction("ListPhanQuyen");
        }
    }
}