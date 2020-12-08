using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;
using PagedList;

namespace WebSiteBanHang.Controllers
{
    public class TimKiemController : Controller
    {
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        [HttpGet]
        // GET: TimKiem
        public ActionResult KQTimKiem(string sTuKhoa, int? page)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            //thực hiện phân trang
            //Tạo biến số sản phẩm trên trang

            int PageSize = 2;
            //tạo biến thứ 2 : số trang hiện tại
            int PageNumber = (page ?? 1);
            //tìm kiếm  theo tên sp
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return View(lstSP.OrderBy(n=>n.TenSP).ToPagedList(PageNumber,PageSize));
        }

        [HttpPost]
        // GET: TimKiem
        public ActionResult LayTuKhoaTimKiem(string sTuKhoa)
        {
          // gọi về hàm get tìm kiếm
            return RedirectToAction("KQTimKiem", new {@sTuKhoa=sTuKhoa });
        }
        public ActionResult KQTimKiemPartial(string sTuKhoa)
        {
            //tìm kiếm  theo tên sp
            var lstSP = db.SanPhams.Where(n => n.TenSP.Contains(sTuKhoa));
            ViewBag.TuKhoa = sTuKhoa;
            return PartialView(lstSP.OrderBy(n=>n.DonGia));
        }

    }
}