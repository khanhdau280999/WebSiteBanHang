﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebSiteBanHang.Models;

namespace WebSiteBanHang.Controllers
{
    public class SubmitListModelController : Controller
    {
        // GET: SubmitListModel
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(PhieuNhap Model, IEnumerable<ChiTietPhieuNhap> ModeList)
        {
            return View();
        }
    }
}