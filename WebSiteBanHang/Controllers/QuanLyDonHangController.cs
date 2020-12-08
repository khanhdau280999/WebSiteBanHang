using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using System.Net.Mail; //send mail
using System.Net;

namespace WebSiteBanHang.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult ChuaThanhToan()
        {
            //Lấy danh sách các đơn hàng chưa duyệt
            var lst = db.DonDatHangs.Where(n => n.DaThanhToan == false).OrderBy(n => n.NgayDat);
            return View(lst);
        }
        public ActionResult ChuaGiao()
        {
            //lấy danh sách đơn hàng chưa giao
            var lstDSDHCG = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == false && n.DaThanhToan == true).OrderBy(n => n.NgayGiao);
            return View(lstDSDHCG);
        }
        public ActionResult DaGiaoDaThanhToan()
        {
            //Lấy danh sách đơn hàng chưa giao
            var lstDSDHCG = db.DonDatHangs.Where(n => n.TinhTrangGiaoHang == true && n.DaThanhToan == true);
            return View(lstDSDHCG);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang model = db.DonDatHangs.SingleOrDefault(n => n.MaDDH == id);
            //kiểm tra đơn hàng có tồn tại hay không
            if (model == null)
            {
                return HttpNotFound();
            }
            //Lấy danh sách chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == id);
            ViewBag.ListChiTietDH = lstChiTietDH;
            return View(model);

        }
        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            //truy vấn lấy ra dữ liệu của đơn hàng đó
            DonDatHang ddhUpdate = db.DonDatHangs.Single(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.TinhTrangGiaoHang = ddh.TinhTrangGiaoHang;
            db.SaveChanges();
            //Lấy danh sách chi tiết đơn hàng để hiển thị cho người dùng thấy
            var lstChiTietDH = db.ChiTietDonDatHangs.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.ListChiTietDH = lstChiTietDH;
            // gửi khách hàng 1 mail để xác nhận việc thanh toán
            GuiMail("xác nhận đơn hàng của hệ thống", "phuquockhanh10d5@gmail.com","1711060949@hunre.edu.vn","28/09/1999","Đơn hàng của bạn đã được đặt thành công!");
            return View(ddhUpdate);
        }
        public void GuiMail(string Title, string toEmail, string FromEmail, string Password, string Content)
        {
            //goi email
            MailMessage mail = new MailMessage();
            mail.To.Add(toEmail); // địa chỉ nhận
            mail.From = new MailAddress(toEmail); //địa chỉ gửi
            mail.Subject = Title; // tiêu đề gửi
            mail.Body = Content; // nội dung
            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com"; // host gửi của gmail
            smtp.Port = 587; //port của Gmail
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(FromEmail, Password); //tài khoản passwword người gửi
            smtp.EnableSsl = true; //kích hoạt giao tiếp an toàn SSL
            smtp.Send(mail); //gửi mail đi
        }
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