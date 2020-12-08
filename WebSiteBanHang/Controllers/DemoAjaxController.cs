using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class DemoAjaxController : Controller
    {
        // GET: DemoAjax
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult DemoAjax()
        {
            return View();
        }
        public ActionResult LoadAjaxActionLink()
        {
            return Content("Hello Ajax");
        }
        //Xử lý Ajax Jquery
        public ActionResult LoadAjaxJQuery(int a, int b)
        {
            System.Threading.Thread.Sleep(2000);
            return Content((a + b).ToString());
        }
        //Sử dụng load ajax kết quả trả về partialview
       // [ChildActionOnly]
        public ActionResult LoadSanPhamAjax()
        {
            //var lstSanPhamLTM = db.SanPhams.Where(n => n.MaLoaiSP == 2 && n.Moi == 1);
            var lstSanPham = db.SanPhams;
            return PartialView("LoadSanPhamAjax", lstSanPham);
        }
    }
}