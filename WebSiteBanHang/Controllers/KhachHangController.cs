using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    [Authorize(Roles = "QuanTri,QuanLySanPham")]
    public class KhachHangController : Controller
    {
        // GET: KhachHang
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [Authorize(Roles = "QuanLySanPham")]
        public ActionResult Index()
        {
            //truy vấn dữ liệu thông qua câu lệnh
            //Đối tượng lstKH sẽ lấy toàn bộ dữ liệu từ bảng Khách Hàng
            //Cách 1: lấy dữ liệu là 1 danh sách khách hàng
            //var lstKH = from KH in db.KhachHangs select KH;
            //Cách 2: Dùng phương thức hỗ trợ sẵn
            var lstKH = db.KhachHangs;
            return View(lstKH);
        }
        [Authorize(Roles = "QuanTri")]
        public ActionResult Index1()
        {
            var lstKH = from KH in db.KhachHangs select KH;
            return View(lstKH);
        }
        public ActionResult TruyVanMotDoiTuong()
        {
            //Cách 1: Truy vấn 1 đối tượng bằng câu lệnh truy vấn
            //Bước 1: Lấy ra danh sách khách hàng
            var lstKH = from kh in db.KhachHangs where kh.MaKH==2 select kh ;
            //Bước 2: 
            // KhachHang khang = lstKH.FirstOrDefault();
            //Lấy đối tượng Khách hàng dựa trên phương thức hỗ trợ
            KhachHang khang = db.KhachHangs.SingleOrDefault(n=>n.MaKH==1);
            return View(khang);
        }
        public ActionResult SortDuLieu()
        {
            //Phương thức sắp xếp dữ liệu
            List<KhachHang> lstKH = db.KhachHangs.OrderByDescending(n => n.TenKH).ToList();
            return View(lstKH);
        }
        public ActionResult GroupDuLieu()
        {
            //Group dữ liệu trên view
            List<ThanhVien> lstTV = db.ThanhViens.OrderByDescending(n => n.TaiKhoan).ToList();
            return View(lstTV);
        }
    }
}