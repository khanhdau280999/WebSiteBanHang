using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString(); // lấy số lượng người truy cập từ application đã được tạo
            ViewBag.SoNguoiDangOnline = HttpContext.Application["SoNguoiDangOnline"].ToString(); // lấy số lượng người đang truy cập
            ViewBag.TongDoanhThu = ThongKeTongDoanhThu(); // thống kê tổng doanh thu
            ViewBag.TongDDH = ThongKeDonHang(); // thống kê đơn hàng
            ViewBag.TongThanhVien = ThongKeThanhVien(); // thống kê thành viên


            return View();
        }
        public decimal ThongKeTongDoanhThu()
        {
            //Thống kê theo tất cả doanh thu từ khi website thành lập
            decimal TongDoanhThu = db.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value;
            return TongDoanhThu;
        }
        public double ThongKeDonHang()
        {
            //đếm đơn đặt hàng
            double slDDH = db.DonDatHangs.Count();
            return slDDH;
        }
        public double ThongKeThanhVien()
        {
            //đếm đơn đặt hàng
            double slTV = db.DonDatHangs.Count();
            return slTV;
        }

        //public decimal ThongKeTongDoanhThuThang(int Thang, int Nam)
        //{
        //    //list ra những đơn hàng nào có tháng năm tương ứng
        //    var lstDDH = db.DonDatHangs.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year = Nam);
        //    decimal TongTien = 0;
        //    //Duyệt chi tiết đơn đặt hàng và lấy tổng tiền của tất cả các sp thuộc đơn hàng đó 
        //    foreach(var item in lstDDH)
        //    {
        //        TongTien += decimal.Parse( item.ChiTietDonDatHangs.Sum(n => n.SoLuong * n.DonGia).Value.ToString());
        //    }
        //    return TongTien;
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                    db.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}