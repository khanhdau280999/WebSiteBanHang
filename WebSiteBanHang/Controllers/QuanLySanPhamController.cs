using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        QuanLyBanHangEntities db = new QuanLyBanHangEntities();
        public ActionResult Index()
        {
            return View(db.SanPhams.Where(n=>n.DaXoa==false).OrderByDescending(n=>n.MaSP));
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            //load dropdown list nhà cung cấp và loại sp, mã nhà sản xuất
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(SanPham sp, HttpPostedFileBase[] HinhAnh)
        {
            //load dropdown list nhà cung cấp và loại sp, mã nhà sản xuất
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX");
            int loi = 0;
            for(int i=0; i<HinhAnh.Count(); i++)
            {
                if (HinhAnh[i] != null)
                {
                    //Kiểm tra nội dung hình ảnh
                    if (HinhAnh[i].ContentLength > 0)
                    {
                        //kiểm tra định dạng hình ảnh
                        if(HinhAnh[i].ContentType != "image/jpeg" && HinhAnh[i].ContentType != "image/png" && HinhAnh[i].ContentType != "image/jpg" && HinhAnh[i].ContentType != "image/gif")
                        {
                            ViewBag.upload += "Hình ảnh" + i + "Không hợp lệ <br />";
                            loi++;
                        }
                        else
                        {
                            //kiểm tra hình ảnh tồn tại
                            //lấy tên hình ảnh
                            var fileName = Path.GetFileName(HinhAnh[0].FileName);
                            //lấy hình ảnh chuyển vào file thử mục hình ảnh
                            var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
                            //nếu thư mục chứa hình ảnh r thì xuất ra thông báo
                            if (System.IO.File.Exists(path))
                            {
                                ViewBag.upload1 = "Hình" + i + "Đã tồn tại <br/>";
                                loi++;
                            }
                        }
                    }
                }
            }
            if (loi > 0)
            {
                return View(sp);
            }
            sp.HinhAnh = HinhAnh[0].FileName;
            sp.HinhAnh = HinhAnh[1].FileName;
            sp.HinhAnh = HinhAnh[2].FileName;
            sp.HinhAnh = HinhAnh[3].FileName;
            sp.HinhAnh = HinhAnh[4].FileName;
            ////kiểm tra hình ảnh tồn tại trong csdl chưa
            //if (HinhAnh[0].ContentLength > 0)
            //{
            //    //lấy tên hình ảnh
            //    var fileName = Path.GetFileName(HinhAnh[0].FileName);
            //    //lấy hình ảnh chuyển vào file thử mục hình ảnh
            //    var path = Path.Combine(Server.MapPath("~/Content/HinhAnhSP"), fileName);
            //    //nếu thư mục chứa hình ảnh r thì xuất ra thông báo
            //    if (System.IO.File.Exists(path))
            //    {
            //        ViewBag.upload = "Hình ảnh đã tồn tại";
            //        return View();
            //    }
            //    else
            //    {
            //        //save hình ảnh vào thư mục
            //        HinhAnh[0].SaveAs(path);
            //        sp.HinhAnh = fileName;
            //    }

            //}
            db.SanPhams.Add(sp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(int? id)
        {
            //lấy  sản phẩm cần chỉnh sửa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load dropdown list nhà cung cấp và loại sp, mã nhà sản xuất
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(SanPham model)
        {
            
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", model.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai", model.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", model.MaNSX);
            //nếu dữ liệu đầu vào chắc chắn là ok
            db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
           
        }
        [HttpGet]
        public ActionResult Xoa(int? id)
        {
            
            //lấy  sản phẩm cần chỉnh sửa vào id
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();
            }

            //load dropdown list nhà cung cấp và loại sp, mã nhà sản xuất
            ViewBag.MaNCC = new SelectList(db.NhaCungCaps.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(db.LoaiSanPhams.OrderBy(n => n.MaLoaiSP), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(db.NhaSanXuats.OrderBy(n => n.MaNSX), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham model = db.SanPhams.SingleOrDefault(n => n.MaSP == id);
            if (model == null)
            {
                return HttpNotFound();
            }
            db.SanPhams.Remove(model);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
    }
}