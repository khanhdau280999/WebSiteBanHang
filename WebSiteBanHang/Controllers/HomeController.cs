using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;

namespace WebSiteBanHang.Controllers
{
    
    public class HomeController : Controller
    {
        
        // GET: Home
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        
        public ActionResult Index()
        {
            //Lần lượt tạo các viewbag để lấy list sản phẩm từ csdl
            //List Điện thoại 
            var lstDTM = db.SanPhams.Where(n => n.MaLoaiSP == 1 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListDTM = lstDTM;
            //List Laptop
            var lstLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListLTM = lstLTM;
            //List máy tính bảng
            var lstMTB = db.SanPhams.Where(n => n.MaLoaiSP == 3 && n.Moi == 1 && n.DaXoa == false);
            //Gán vào ViewBag
            ViewBag.ListMTB = lstMTB;


            return View();
        }
        public ActionResult MenuPartial()
        {
            //truy vấn lấy về 1 list các sản phẩm
            var lstSP = db.SanPhams;
            return PartialView(lstSP);
        }

        [HttpGet]
        public ActionResult DangKy()
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien tv, FormCollection f)
        {
            ViewBag.CauHoi = new SelectList(LoadCauHoi());
            //Kiểm tra captcha hợp lệ
            if (this.IsCaptchaValid("Captcha is not valid"))
            {
                if (ModelState.IsValid)
                {
                ViewBag.ThongBao = "Thêm thành công";
                //Thêm khách hàng vào csdl
                db.ThanhViens.Add(tv);
                db.SaveChanges();
                }
                else
                {
                    ViewBag.ThongBao = "Thêm không thành công";
                }
                return View();
            }
            ViewBag.ThongBao = "Sai mã captcha";
            return View();
        }
        //Load câu hỏi bí mật
        public List<string> LoadCauHoi()
        {
            List<string> lstCauHoi = new List<string>();
            lstCauHoi.Add("Con vật mà bạn yêu thích?");
            lstCauHoi.Add("Ca sĩ mà bạn yêu thích?");
            lstCauHoi.Add("Bạn ghét ai nhất?");
            return lstCauHoi;
        }

        //Xây dựng action đăng nhập
        [HttpPost]
        public ActionResult DangNhap(FormCollection f)
        {
            //string sTaiKhoan = f["txtTenDangNhap"].ToString();
            //string sMatKhau = f["txtMatKhau"].ToString();

            //ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == sTaiKhoan && n.MatKhau == sMatKhau);

            //if(tv != null)
            //{
            //    Session["TaiKhoan"] = tv;
            //    return Content("<script>window.location.reload();</script>");
            //}

            //return Content("Tài khoản hoặc mật khẩu không đúng!");
            string taikhoan = f["txtTenDangNhap"].ToString();
            string matkhau = f["txtMatKhau"].ToString();
            //truy vấn kiểm tra đăng nhập lấy thông tin tv
            ThanhVien tv = db.ThanhViens.SingleOrDefault(n => n.TaiKhoan == taikhoan && n.MatKhau == matkhau);

            if (tv != null)
            {
                //lấy ra list quyền của tv tương ứng với loại thành viên
                var lstQuyen = db.LoaiThanhVien_Quyen.Where(n => n.MaLoaiTV == tv.MaLoaiTV);
                //duyệt list quyền
                string Quyen = "";
                if (lstQuyen.Count() != 0) 
                {
                
                foreach(var item in lstQuyen)
                {
                    Quyen += item.Quyen.MaQuyen + ",";
                }
                Quyen = Quyen.Substring(0, Quyen.Length - 1); // cắt dấu ","
                PhanQuyen(tv.TaiKhoan.ToString(), Quyen);
                    Session["TaiKhoan"] = tv;
                return Content("<script>window.location.reload();</script>");
                }
            }

            return Content("Tài khoản hoặc mật khẩu không đúng!");
        }

        public void PhanQuyen(string TaiKhoan, string Quyen)
        {
            FormsAuthentication.Initialize();
            var ticket = new FormsAuthenticationTicket(1,
                TaiKhoan, // user
                DateTime.Now, //thời gian bắt đầu
                DateTime.Now.AddHours(3), //thời gian kết thúc
                false, //ghi nhớ
                Quyen, //"DangKy,QuanLyDonHang,QuanLySanPham"
                FormsAuthentication.FormsCookiePath);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(ticket));
            if (ticket.IsPersistent) cookie.Expires = ticket.Expiration;
            Response.Cookies.Add(cookie);
        }
        //Tạo trang ngăn chặn quyền truy cập
        public ActionResult LoiPhanQuyen()
        {
            return View();
        }

        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
    }
}